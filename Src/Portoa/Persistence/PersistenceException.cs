using System;
using Portoa.Util;

namespace Portoa.Persistence {
	public class PersistenceException : Exception {
		public PersistenceException(string message = null, Exception innerException = null) : base(message, innerException) { }
	}


	public class EntityNotFoundException : PersistenceException {
		public EntityNotFoundException(string message = null, Exception innerException = null) : base(message, innerException) { }
	}

	public class EntityNotFoundException<T, TId> : EntityNotFoundException where T : Entity<T, TId> {
		public EntityNotFoundException(string message = null, Exception innerException = null) : base(message, innerException) { }
		public EntityNotFoundException(TId id) : base(string.Format("Entity of type {0} not found with ID {1}", typeof(T).GetFriendlyName(fullyQualified: false), id)) {}
	}
}