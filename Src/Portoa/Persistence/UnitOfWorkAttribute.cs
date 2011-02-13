using System;

namespace Portoa.Persistence {
	/// <summary>
	/// Signifies that this method should be executed within a transaction
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class UnitOfWorkAttribute : Attribute { }
}