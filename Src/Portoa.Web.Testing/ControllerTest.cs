using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FakeN.Web;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Portoa.Testing;
using MvcTestingHelpers;
using Portoa.Web.Filters;
using Portoa.Web.Unity;

namespace Portoa.Web.Testing {
	[TestFixture]
	public abstract class ControllerTest<T> : ContainerWiredTest where T : Controller {

		private T controller;

		protected ControllerTest() {
			ActionExecutor = new ControllerActionExecutor();
		}

		[SetUp]
		public override void SetUp() {
			base.SetUp();

			Container
				.RegisterInstance<HttpContextBase>(new FakeHttpContext())
				.RegisterInstance(new RouteData())
				.RegisterType<T>();

			FilterProviders.Providers.Clear();
			FilterProviders.Providers.Add(new AdjustableFilterProvider(new InjectionFilterAdjuster(Container)));
		}

		[TearDown]
		public override void TearDown() {
			controller = null;
			base.TearDown();
		}

		protected ControllerActionExecutor ActionExecutor { get; set; }

		/// <summary>
		/// The controller instance (resolved by the container) associated with this test
		/// </summary>
		protected T Controller {
			get {
				if (controller == null) {
					controller = Container.Resolve<T>();
					controller.ControllerContext = new ControllerContext(
						Container.Resolve<HttpContextBase>(),
						Container.Resolve<RouteData>(),
						controller
					);
				}

				return controller;
			}
		}

		/// <summary>
		/// Calls an action on the controller using ActionFilterInjector as
		/// the action invoker
		/// </summary>
		protected ActionResult ExecuteAction(Expression<Func<T, object>> actionMethod, HttpVerbs httpMethod = HttpVerbs.Get) {
			return ActionExecutor.ExecuteActionWithFilters(actionMethod, Controller, () => new ControllerActionInvoker(), httpMethod);
		}

		/// <summary>
		/// Calls an action on the controller using ActionFilterInjector as
		/// the action invoker
		/// </summary>
		protected ActionResult ExecuteAction<TInvoker>(Expression<Func<T, object>> actionMethod, Expression<Func<TInvoker>> newInvokerExpression, HttpVerbs httpMethod = HttpVerbs.Get) 
			where TInvoker : ControllerActionInvoker {
			return ActionExecutor.ExecuteActionWithFilters(actionMethod, Controller, newInvokerExpression, httpMethod);
		}
	}
}