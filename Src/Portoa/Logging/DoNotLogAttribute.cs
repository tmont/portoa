using System;

namespace Portoa.Logging {
	/// <summary>
	/// Signifies that this object should not be logged
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class DoNotLogAttribute : Attribute { }
}