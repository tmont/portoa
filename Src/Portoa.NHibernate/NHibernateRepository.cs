using System.Diagnostics;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Portoa.Persistence;

namespace Portoa.NHibernate {

	/// <summary>
	/// Default repository implementation for NHibernate
	/// </summary>
	/// <typeparam name="T">The entity type</typeparam>
	[DebuggerNonUserCode]
	public class NHibernateRepository<T> : IRepository<T> {
		/// <summary>
		/// The current session
		/// </summary>
		protected ISession Session { get; private set; }

		public NHibernateRepository(ISession session) {
			Session = session;
		}

		public virtual T Save(T entity) {
			if (Session.Contains(entity) || entity.IsTransient()) {
				Session.SaveOrUpdate(entity);
				return entity;
			}

			return (T)Session.Merge(entity);
		}

		public virtual T Reload(T entity) {
			Session.Refresh(entity);
			return entity;
		}

		public virtual void Delete(T entity) {
			Session.Delete(entity);
		}

		public void Delete(object id) {
			Delete(Session.Load<T>(id));
		}

		public virtual T FindById(object id) {
			var entity = Session.Get<T>(id);
			if (Equals(entity, default(T))) {
				throw new EntityNotFoundException<T>(id);
			}

			return entity;
		}

		public virtual IQueryable<T> Records { get { return Session.Query<T>(); } }
	}
}