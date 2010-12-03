using System;

namespace Portoa.Persistence {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class UnitOfWorkAttribute : Attribute { }
}