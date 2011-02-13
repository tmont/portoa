using System;
using Portoa.Logging;

namespace Portoa.Persistence {
	/// <summary>
	/// Represents a transaction
	/// </summary>
	[DoNotLog]
	public interface IUnitOfWork : IDisposable {

		/// <summary>
		/// Starts a transaction
		/// </summary>
		/// <exception cref="PersistenceException">If a transaction has already been started</exception>
		IUnitOfWork Start();

		/// <summary>
		/// Commits a transaction
		/// </summary>
		/// <exception cref="PersistenceException">If a transaction has not been started</exception>
		void Commit();

		/// <summary>
		/// Rolls back a transaction
		/// </summary>
		/// <exception cref="PersistenceException">If a transaction has not been started</exception>
		void Rollback();
	}
}