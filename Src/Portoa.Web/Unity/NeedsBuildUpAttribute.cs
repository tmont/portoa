using System;
using Portoa.Web.Controllers;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Indicates that an object needs to be built up by the container
	/// </summary>
	/// <see cref="InjectableFilterActionInvoker"/>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class NeedsBuildUpAttribute : Attribute { }
}