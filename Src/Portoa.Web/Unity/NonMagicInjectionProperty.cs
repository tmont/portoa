using System;
using System.Linq.Expressions;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Unity {
	public class NonMagicInjectionProperty<T> : NonMagicInjectionProperty<T, object> {
		public NonMagicInjectionProperty(Expression<Func<T, object>> propertyAccessor) : base(propertyAccessor) { }
		public NonMagicInjectionProperty(Expression<Func<T, object>> propertyAccessor, object propertyValue) : base(propertyAccessor, propertyValue) { }
	}

	public class NonMagicInjectionProperty<T, TReturn> : InjectionMember {
		private readonly InjectionProperty injectionProperty;

		public NonMagicInjectionProperty(Expression<Func<T, TReturn>> propertyAccessor) {
			injectionProperty = new InjectionProperty(GetPropertyNameFromExpression(propertyAccessor));
		}

		public NonMagicInjectionProperty(Expression<Func<T, TReturn>> propertyAccessor, TReturn propertyValue) {
			injectionProperty = new InjectionProperty(GetPropertyNameFromExpression(propertyAccessor), propertyValue);
		}

		private static string GetPropertyNameFromExpression(Expression<Func<T, TReturn>> expression) {
			var parameterName = expression.Parameters[0].Name;
			var memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null) {
				throw new ArgumentException("Expected lambda expression like: foo => foo.Bar, where Bar is the name of the property to be injected");
			}

			var leftSide = memberExpression.Expression as ParameterExpression;
			if (leftSide == null || leftSide.Name != parameterName) {
				throw new ArgumentException("Expected lambda expression like: foo => foo.Bar, where Bar is the name of the property to be injected");
			}

			return memberExpression.Member.Name;
		}

		public override void AddPolicies(Type serviceType, Type implementationType, string name, IPolicyList policies) {
			injectionProperty.AddPolicies(serviceType, implementationType, name, policies);
		}
	}
}