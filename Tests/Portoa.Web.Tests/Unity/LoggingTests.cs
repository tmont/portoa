using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using Portoa.Logging;
using Portoa.Web.Unity;

namespace Portoa.Web.Tests.Unity {
	[TestFixture]
	public class LoggingTests {
		private static readonly Regex elapsedRegex = new Regex(@"^(.+)\s\[\d+\sms\]$");
		private IUnityContainer container;
		private TestLoggingClass obj;

		[SetUp]
		public void SetUp() {
			container = new UnityContainer();
			container
				.AddNewExtension<Interception>()
				.AddNewExtension<LogAllMethodCalls>()
				.RegisterInstance<ILogger>(new InMemoryLogger(), new ContainerControlledLifetimeManager())
				.RegisterAndIntercept(new TestLoggingClass());

			obj = container.Resolve<TestLoggingClass>();
		}

		[TearDown]
		public void TearDown() {
			container.Dispose();
		}

		private string[] GetMessages() {
			return ((InMemoryLogger)container.Resolve<ILogger>()).Messages.ToArray();
		}

		private void VerifyMessages(string methodCall, string returnValue) {
			var messages = GetMessages();
			Assert.That(messages, Has.Property("Length").EqualTo(2));
			Assert.That(messages[0], Is.EqualTo(string.Format("call:   {0}.{1}", typeof(TestLoggingClass).Name, methodCall)));
			Assert.That(
				elapsedRegex.Replace(messages[1], "${1}"),
				Is.EqualTo(string.Format("return: {0}.{1} -> {2}", typeof(TestLoggingClass).Name, methodCall, returnValue))
			);
		}

		[Test]
		public void Should_shorten_long_string() {
			obj.GetReallyLongString();
			VerifyMessages("GetReallyLongString()", "\"" + new string('x', 40) + "... <snip> ..." + new string('x', 40) + "\"");
		}

		[Test]
		public void Should_format_null_return() {
			obj.GetNull();
			VerifyMessages("GetNull()", "<NULL>");
		}

		[Test]
		public void Should_format_void_call_as_null() {
			obj.Void();
			VerifyMessages("Void()", "<NULL>");
		}

		[Test]
		public void Should_format_char_return() {
			obj.GetChar();
			VerifyMessages("GetChar()", "'x'");
		}

		[Test]
		public void Should_format_type_return() {
			obj.GetType2();
			VerifyMessages("GetType2()", "Type[TestLoggingClass]");
		}

		[Test]
		public void Should_format_exception_return() {
			obj.GetException();
			VerifyMessages("GetException()", "Exception");
		}

		[Test]
		public void Should_format_methodinfo_return() {
			obj.GetMethodInfo();
			VerifyMessages("GetMethodInfo()", "MethodInfo[Object.ToString]");
		}

		[Test]
		public void Should_format_object_that_overrides_tostring_return() {
			obj.GetToStringable();
			VerifyMessages("GetToStringable()", "oh hai!");
		}

		[Test]
		public void Should_format_collection_return() {
			obj.GetCollection();
			VerifyMessages("GetCollection()", "String[](Count=2)");
		}

		[Test]
		public void Should_format_short_string_return() {
			obj.GetShortString();
			VerifyMessages("GetShortString()", "\"foo\"");
		}

		[Test]
		public void Should_format_integer_return() {
			obj.GetInteger();
			VerifyMessages("GetInteger()", "5");
		}

		[Test]
		public void Should_format_other_type_return() {
			obj.GetOtherType();
			VerifyMessages("GetOtherType()", "TestLoggingClass");
		}

		[Test]
		public void Should_log_uncaught_exceptions() {
			try {
				obj.ThrowException();
			} catch { }

			var message = GetMessages()[1];
			Assert.That(message, Is.StringMatching(@"return: TestLoggingClass\.ThrowException\(\) -> threw exception Exception: oh noes!!\s+(.+\s+)+"));
		}

		[Test]
		public void Should_log_method_signature_and_respect_DoNotLog() {
			obj.FancySignature("a string", 9, new ToStringable(), "hunter2");
			VerifyMessages("FancySignature(\"a string\", 9, oh hai!, secret:******)", "<NULL>");
		}

		public class ToStringable {
			public override string ToString() {
				return "oh hai!";
			}
		}

		public class TestLoggingClass : MarshalByRefObject {
			public void FancySignature(string foo, int bar, ToStringable stringable, [DoNotLog]string secret) { }

			public void ThrowException() {
				throw new Exception("oh noes!!");
			}

			public object GetOtherType() {
				return this;
			}

			public int GetInteger() {
				return 5;
			}

			public string GetShortString() {
				return "foo";
			}

			public string GetReallyLongString() {
				return new string('x', 300);
			}

			public object GetNull() {
				return null;
			}

			public void Void() { }

			public char GetChar() {
				return 'x';
			}

			public Type GetType2() {
				return GetType();
			}

			public Exception GetException() {
				return new Exception();
			}

			public MethodInfo GetMethodInfo() {
				return GetType().GetMethod("ToString");
			}

			public ToStringable GetToStringable() {
				return new ToStringable();
			}

			public ICollection GetCollection() {
				return new[] { "foo", "bar" };
			}
		}

		public class InMemoryLogger : AbstractLogger {
			private readonly List<string> messages = new List<string>();

			public IEnumerable<string> Messages { get { return messages; } }

			protected override void Log(object message) {
				messages.Add(message.ToString());
			}
		}
	}
}