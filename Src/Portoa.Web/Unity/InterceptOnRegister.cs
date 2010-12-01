using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity {
	public class InterceptOnRegister : UnityContainerExtension {
		private IInstanceInterceptor interceptor;
		private readonly IList<IInterceptRule> rules;

		public InterceptOnRegister() {
			interceptor = new TransparentProxyInterceptor();
			rules = new List<IInterceptRule> {
				new NotUnityInterceptionAssembly(),
				new DoesNotHaveGenericMethods()
			};
		}

		public InterceptOnRegister AddMatchingRule(IInterceptRule rule) {
			rules.Add(rule);
			return this;
		}

		public InterceptOnRegister AddNewMatchingRule<T>() where T : IInterceptRule, new() {
			rules.Add(new T());
			return this;
		}

		public InterceptOnRegister SetDefaultInterceptor<T>() where T : IInstanceInterceptor, new() {
			interceptor = new T();
			return this;
		}

		protected override void Initialize() {
			Container.AddExtensionOnce<Interception>();
			Context.Registering += (sender, e) => SetInterceptorFor(e.TypeFrom, e.TypeTo ?? e.TypeFrom);
		}

		private void SetInterceptorFor(Type typeToIntercept, Type typeOfInstance) {
			if (!rules.All(rule => rule.Matches(typeToIntercept, typeOfInstance))) {
				return;
			}

			if (interceptor.CanIntercept(typeOfInstance)) {
				Container
					.Configure<Interception>()
					.SetDefaultInterceptorFor(typeOfInstance, interceptor);
			} else if (interceptor.CanIntercept(typeToIntercept)) {
				Container
					.Configure<Interception>()
					.SetDefaultInterceptorFor(typeToIntercept, interceptor);
			}
		}

	}
}