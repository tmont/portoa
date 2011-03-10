using System.Web.Mvc;
using JetBrains.Annotations;

namespace Portoa.Web.SmartCasing {
	/// <summary>
	/// <see cref="IActionInvoker"/> decorator that uses <see cref="SmartCaseConverter"/>
	/// to modify the action name before invoking the action
	/// </summary>
	public sealed class SmartCaseActionInvoker : IActionInvoker {
		private readonly IActionInvoker actionInvoker;

		/// <param name="actionInvoker">The action invoker to decorate</param>
		public SmartCaseActionInvoker([NotNull]IActionInvoker actionInvoker) {
			this.actionInvoker = actionInvoker;
		}

		public bool InvokeAction(ControllerContext controllerContext, string actionName) {
			var convertedActionName = SmartCaseConverter.ConvertFrom(actionName);
			//i'd rather just do this, and then the value passed into this method would already be correct, but
			//i couldn't figure out where that needs to happen... routing is not very... uh... intuitive in .NET
			controllerContext.RouteData.Values["action"] = convertedActionName;

			return actionInvoker.InvokeAction(controllerContext, convertedActionName);
		}
	}
}