using NHibernate;
using Portoa.Persistence;

namespace Portoa.NHibernate {
	/// <summary>
	/// Handles transactions in NHibernate, does not support nested transactions
	/// </summary>
	public class NHibernateUnitOfWork : IUnitOfWork {
		private readonly ISession session;
		private ITransaction tx;

		public NHibernateUnitOfWork(ISession session) {
			this.session = session;
		}

		/// <summary>
		/// Starts a transaction
		/// </summary>
		public IUnitOfWork Start() {
			tx = session.BeginTransaction();
			return this;
		}

		/// <summary>
		/// Commits a transaction
		/// </summary>
		/// <exception cref="PersistenceException">If a transaction has not been started</exception>
		public void Commit() {
			if (tx == null) {
				throw new PersistenceException("Transaction not started.");
			}

			tx.Commit();
		}

		/// <summary>
		/// Rolls back a transaction
		/// </summary>
		/// <exception cref="PersistenceException">If a transaction has not been started</exception>
		public void Rollback() {
			if (tx == null) {
				throw new PersistenceException("Transaction not started.");
			}

			tx.Rollback();
		}

		public void Dispose() {
			if (tx != null) {
				tx.Dispose();
			}
		}
	}
}