using System;

namespace Portoa.Web {
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class NeedsBuildUpAttribute : Attribute { }
}