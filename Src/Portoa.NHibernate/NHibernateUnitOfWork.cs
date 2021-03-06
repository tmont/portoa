﻿using NHibernate;
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

		public IUnitOfWork Start() {
			if (IsActive) {
				throw new PersistenceException("Cannot start another transaction while another is still active");
			}

			tx = session.BeginTransaction();
			return this;
		}

		public void Commit() {
			if (!IsActive) {
				throw new PersistenceException("Transaction not started.");
			}

			tx.Commit();
		}

		public void Rollback() {
			if (!IsActive) {
				throw new PersistenceException("Transaction not started.");
			}

			logger.Warn("Rolling back transaction");
			tx.Rollback();
		}

		public bool IsActive { get { return tx != null && tx.IsActive; } }

		public void Dispose() {
			if (tx != null) {
				tx.Dispose();
			}
		}
	}
}