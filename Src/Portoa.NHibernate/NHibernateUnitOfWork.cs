using NHibernate;
using Portoa.Logging;
using Portoa.Persistence;

namespace Portoa.NHibernate {
	/// <summary>
	/// Handles transactions in NHibernate, does not support nested transactions
	/// </summary>
	[DoNotLog]
	public class NHibernateUnitOfWork : IUnitOfWork {
		private readonly ISession session;
		private readonly ILogger logger;
		private ITransaction tx;

		public NHibernateUnitOfWork(ISession session, ILogger logger) {
			this.session = session;
			this.logger = logger;
		}

		public IUnitOfWork Start() {
			if (tx != null && tx.IsActive) {
				throw new PersistenceException("Cannot start another transaction while another is still active");
			}

			tx = session.BeginTransaction();
			logger.Debug("Starting transaction");
			return this;
		}

		public void Commit() {
			if (tx == null) {
				throw new PersistenceException("Transaction not started.");
			}

			logger.Debug("Committing transaction");
			tx.Commit();
		}

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