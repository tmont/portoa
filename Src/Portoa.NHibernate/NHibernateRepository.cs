using System;
using System.Diagnostics;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Portoa.Persistence;

namespace Portoa.NHibernate {

	/// <summary>
	/// Default repository implementation for entities with integral identifiers
	/// </summary>
	/// <typeparam name="T">The entity type</typeparam>
	/// <seealso cref="NHibernateRepository{T, TId}"/>
	public class NHibernateRepository<T> : NHibernateRepository<T, int>, IRepository<T> where T : IIdentifiable<int> {
		public NHibernateRepository(ISession session) : base(session) { }
	}

	/// <summary>
	/// Default repository implementation for NHibernate
	/// </summary>
	/// <typeparam name="T">The entity type</typeparam>
	/// <typeparam name="TId">The identifier type</typeparam>
	/// <seealso cref="NHibernateRepository{T}"/>
	[DebuggerNonUserCode]
	public class NHibernateRepository<T, TId> : MarshalByRefObject, IRepository<T, TId> where T : IIdentifiable<TId> {
		/// <summary>
		/// The current session
		/// </summary>
		protected ISession Session { get; private set; }

		public NHibernateRepository(ISession session) {
			Session = session;
		}

		public virtual T Save(T entity) {
			if (Session.Contains(entity) || Equals(entity.Id, default(TId))) {
				Session.SaveOrUpdate(entity);
				return entity;
			}

			var entityWithId = (T)Session.SaveOrUpdateCopy(entity);
			entity.Id = entityWithId.Id;
			return entityWithId;
		}

		public T Reload(T entity) {
			Session.Evict(entity);
			return FindById(entity.Id);
		}

		public virtual void Delete(T entity) {
			Session.Delete(entity);
		}

		public void Delete(TId id) {
			Delete(Session.Get<T>(id));
		}

		public virtual T FindById(TId id) {
			var entity = Session.Get<T>(id);
			if (Equals(entity, default(T))) {
				throw new EntityNotFoundException<T, TId>(id);
			}

			return entity;
		}

		public virtual IQueryable<T> Records { get { return Session.Query<T>(); } }
	}
}