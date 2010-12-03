using NHibernate;
using Portoa.Logging;
using Portoa.Persistence;

namespace Portoa.NHibernate {
	/// <summary>
	/// Handles transactions in NHibernate, does not support nested transactions
	/// </summary>
	public class NHibernateUnitOfWork : IUnitOfWork {
		private readonly ISession session;
		private readonly ILogger logger;
		private ITransaction tx;

		public NHibernateUnitOfWork(ISession session, ILogger logger) {
			this.session = session;
			this.logger = logger;
		}

		/// <summary>
		/// Starts a transaction
		/// </summary>
		public IUnitOfWork Start() {
			tx = session.BeginTransaction();
			logger.Debug("Starting transaction");
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

			logger.Debug("Committing transaction");
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

			logger.Warn("Rolling back transaction");
			tx.Rollback();
		}

		public void Dispose() {
			if (tx != null) {
				tx.Dispose();
			}
		}
	}
}