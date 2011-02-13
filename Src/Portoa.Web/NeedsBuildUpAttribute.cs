using System;

namespace Portoa.Web {
	/// <summary>
	/// Indicates that an object needs to be built up by the container
	/// </summary>
	/// <see cref="InjectableFilterActionInvoker"/>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class NeedsBuildUpAttribute : Attribute { }
}