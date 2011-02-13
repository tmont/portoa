using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Logging;
using Portoa.Util;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Performs automatic logging on method calls (but respects <see cref="DoNotLogAttribute"/>)
	/// </summary>
	[DebuggerNonUserCode]
	public class LoggerCallHandler : ICallHandler {
		private readonly ILogger logger;
		private string formattedInvocation;

		public LoggerCallHandler(ILogger logger) {
			this.logger = logger;
		}

		public int Order { get; set; }

		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext) {
			if (ShouldLog(input)) {
				logger.Debug("call:   " + FormatMethodInvocation(input));
			}

			var stopwatch = Stopwatch.StartNew();
			var result = getNext()(input, getNext);
			stopwatch.Stop();

			if (ShouldLog(input)) {
				logger.Debug("return: " + CreateMethodReturnMessage(input, result, stopwatch));
			}

			return result;
		}

		private bool ShouldLog(IMethodInvocation invocation) {
			if (!logger.IsDebugEnabled) {
				return false;
			}

			var instanceIsLoggable = invocation.GetInstanceMethodInfo().IsLoggable();
			var targetIsLoggable = invocation.Target.GetType().IsLoggable();

			return instanceIsLoggable && targetIsLoggable;
		}

		private static string GetTypeName(Type type) {
			return type.GetFriendlyName(false);
		}

		private string CreateMethodReturnMessage(IMethodInvocation input, IMethodReturn result, Stopwatch stopwatch) {
			return string.Format(
				CultureInfo.InvariantCulture,
				"{0} -> {1} [{2} ms]",
				FormatMethodInvocation(input),
				FormatMethodReturn(result),
				stopwatch.ElapsedMilliseconds
			);
		}

		private string FormatMethodInvocation(IMethodInvocation invocation) {
			if (string.IsNullOrEmpty(formattedInvocation)) {
				var builder = new StringBuilder();
				builder.Append(GetTypeName(invocation.Target.GetType()));
				builder.Append("." + invocation.MethodBase.Name + "(");
				if (invocation.Arguments != null && invocation.Arguments.Count > 0) {
					builder.Append(
						invocation
							.Arguments
							.GetParameters()
							.Implode(param => param.IsLoggable() ? FormatForLog(invocation.Arguments[param.Name]) : param.Name + ":******", ", ")
					);
				}
				builder.Append(")");
				formattedInvocation = builder.ToString();
			}

			return formattedInvocation;
		}

		private static string FormatMethodReturn(IMethodReturn methodReturn) {
			if (methodReturn.Exception != null) {
				return "threw exception " + FormatForLog(methodReturn.Exception) + ": " + methodReturn.Exception.Message + Environment.NewLine + methodReturn.Exception.StackTrace;
			}

			return FormatForLog(methodReturn.ReturnValue);
		}

		private static string FormatForLog(object obj) {
			if (obj == null) {
				return "<NULL>";
			}
			if (obj is string) {
				return "\"" + FormatString(obj.ToString()) + "\"";
			}
			if (obj is char) {
				return "'" + obj + "'";
			}
			if (obj is Type) {
				return "Type[" + GetTypeName((Type)obj) + "]";
			}

			var type = obj.GetType();
			if (obj is Exception) {
				//exception overrides ToString() but we don't want to use it because it spits out a stack trace as well
				return GetTypeName(type);
			}
			if (obj is MethodInfo) {
				return "MethodInfo[" + GetTypeName(((MethodInfo)obj).DeclaringType) + "." + ((MethodInfo)obj).Name + "]";
			}

			var toString = type.GetMethod("ToString", new Type[0]);
			if (type.IsValueType || (toString != null && toString.DeclaringType != typeof(object))) {
				//value types (ints, chars, etc.) can be written as is.
				//if some subclass has overridden ToString(), use it.
				return FormatString(obj.ToString());
			}

			var formatted = GetTypeName(type);
			if (obj is ICollection) {
				return formatted + "(Count=" + ((ICollection)obj).Count + ")";
			}

			return formatted;
		}

		/// <summary>
		/// Truncates a string to 150 characters in a pretty way, if necessary
		/// </summary>
		private static string FormatString(string s) {
			const int maxLength = 150;
			if (s.Length < maxLength) {
				return s;
			}

			const int length = (maxLength / 2) - 10;
			return s.Substring(0, length) + "... <snip> ..." + s.Substring(s.Length - length - 1);
		}
	}
}