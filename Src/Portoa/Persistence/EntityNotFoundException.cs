using System;
using Portoa.Util;

namespace Portoa.Persistence {
	/// <summary>
	/// Raised when an entity was not found
	/// </summary>
	public class EntityNotFoundException : PersistenceException {
		public EntityNotFoundException(string message = null, Exception innerException = null) : base(message, innerException) { }
	}

	/// <summary>
	/// Raised when an entity was not found
	/// </summary>
	/// <typeparam name="T">The entity type</typeparam>
	/// <typeparam name="TId">The entity's identifier type</typeparam>
	public class EntityNotFoundException<T, TId> : EntityNotFoundException where T : Entity<T, TId> {
		public EntityNotFoundException(string message = null, Exception innerException = null) : base(message, innerException) { }
		public EntityNotFoundException(TId id) : base(string.Format("Entity of type {0} not found with ID {1}", typeof(T).GetFriendlyName(fullyQualified: false), id)) { }
	}
}