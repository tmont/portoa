using System;

namespace Portoa.Persistence {
	/// <summary>
	/// Represents a transaction
	/// </summary>
	public interface IUnitOfWork : IDisposable {

		/// <summary>
		/// Starts a transaction
		/// </summary>
		IUnitOfWork Start();

		/// <summary>
		/// Commits a transaction
		/// </summary>
		void Commit();

		/// <summary>
		/// Rolls back a transaction
		/// </summary>
		void Rollback();
	}
}