using System;

namespace Portoa.NHibernate {
	/// <summary>
	/// Interface used to enable events before and after NHibernate constructs an entity
	/// </summary>
	/// <seealso cref="BuildWithProviderInterceptor"/>
	public interface IInstantiatable {
		/// <summary>
		/// Invoked before an entity is instantiated. Arguments are the entity name, its ID
		/// and a service provider
		/// </summary>
		event Action<string, object, IServiceProvider> BeforeInstatiation;

		/// <summary>
		/// Invoked after an entity is instantiated. Arguments are the entity
		/// and a service provider
		/// </summary>
		event Action<object, IServiceProvider> AfterInstantiation;
	}
}