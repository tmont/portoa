using System;

namespace Portoa.Persistence {
	/// <summary>
	/// Represents a transaction
	/// </summary>
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

		/// <summary>
		/// Gets whether or not the transaction is currently active
		/// </summary>
		bool IsActive { get; }
	}
}