using System;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using JetBrains.Annotations;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Stat;
using NHibernate.Type;

namespace Portoa.NHibernate.Testing {
	/// <summary>
	/// ISession decorator that automatically commits
	/// and evicts entities after persisting
	/// </summary>
	[DebuggerNonUserCode]
	public class AutoEvictSession : ISession {
		private readonly ISession session;

		public AutoEvictSession([NotNull]ISession session) {
			this.session = session;
		}

		private void CommitAndEvict(Action action, object entity) {
			using (var tx = session.BeginTransaction()) {
				try {
					action();
					tx.Commit();
					session.Evict(entity);
				}
				catch {
					tx.Rollback();
					throw;
				}
			}
		}

		private object CommitAndEvict(Func<object> action, object entity) {
			using (var tx = session.BeginTransaction()) {
				try {
					var result = action();
					tx.Commit();
					session.Evict(entity);
					return result;
				} catch {
					tx.Rollback();
					throw;
				}
			}
		}

		private T CommitAndEvict<T>(Func<T> action, T entity) where T : class {
			using (var tx = session.BeginTransaction()) {
				try {
					var result = action();
					tx.Commit();
					session.Evict(entity);
					return result;
				} catch {
					tx.Rollback();
					throw;
				}
			}
		}

		public void Dispose() {
			session.Dispose();
		}

		public void Flush() {
			session.Flush();
		}

		public IDbConnection Disconnect() {
			return session.Disconnect();
		}

		public void Reconnect() {
			session.Reconnect();
		}

		public void Reconnect(IDbConnection connection) {
			session.Reconnect(connection);
		}

		public IDbConnection Close() {
			return session.Close();
		}

		public bool IsReadOnly(object entityOrProxy) {
			return session.IsReadOnly(entityOrProxy);
		}

		public bool IsDirty() {
			return session.IsDirty();
		}

		public void CancelQuery() {
			session.CancelQuery();
		}

		public void SetReadOnly(object entityOrProxy, bool readOnly) {
			session.SetReadOnly(entityOrProxy, readOnly);
		}

		public object GetIdentifier(object obj) {
			return session.GetIdentifier(obj);
		}

		public bool Contains(object obj) {
			return session.Contains(obj);
		}

		public void Evict(object obj) {
			session.Evict(obj);
		}

		public object Load(string entityName, object id, LockMode lockMode) {
			return session.Load(entityName, id, lockMode);
		}

		public object Load(Type theType, object id, LockMode lockMode) {
			return session.Load(theType, id, lockMode);
		}

		public object Load(Type theType, object id) {
			return session.Load(theType, id);
		}

		public T Load<T>(object id, LockMode lockMode) {
			return session.Load<T>(id, lockMode);
		}

		public T Load<T>(object id) {
			return session.Load<T>(id);
		}

		public object Load(string entityName, object id) {
			return session.Load(entityName, id);
		}

		public void Load(object obj, object id) {
			session.Load(obj, id);
		}

		public void Replicate(object obj, ReplicationMode replicationMode) {
			session.Replicate(obj, replicationMode);
		}

		public void Replicate(string entityName, object obj, ReplicationMode replicationMode) {
			session.Replicate(entityName, obj, replicationMode);
		}

		public object Save(object obj) {
			return CommitAndEvict(() => session.Save(obj), obj);
		}

		public void Save(object obj, object id) {
			CommitAndEvict(() => session.Save(obj, id), obj);
		}

		public object Save(string entityName, object obj) {
			return CommitAndEvict(() => session.Save(entityName, obj), obj);
		}

		public void SaveOrUpdate(object obj) {
			CommitAndEvict(() => session.SaveOrUpdate(obj), obj);
		}

		public void SaveOrUpdate(string entityName, object obj) {
			CommitAndEvict(() => session.SaveOrUpdate(entityName, obj), obj);
		}

		public void Update(object obj) {
			CommitAndEvict(() => session.Update(obj), obj);
		}

		public void Update(object obj, object id) {
			CommitAndEvict(() => session.Update(obj, id), obj);
		}

		public void Update(string entityName, object obj) {
			CommitAndEvict(() => session.Update(entityName, obj), obj);
		}

		public object Merge(object obj) {
			return CommitAndEvict(() => session.Merge(obj), obj);
		}

		public object Merge(string entityName, object obj) {
			return CommitAndEvict(() => session.Merge(entityName, obj), obj);
		}

		public T Merge<T>(T entity) where T : class {
			return CommitAndEvict(() => session.Merge(entity), entity);
		}

		public T Merge<T>(string entityName, T entity) where T : class {
			return CommitAndEvict(() => session.Merge(entityName, entity), entity);
		}

		public void Persist(object obj) {
			session.Persist(obj);
		}

		public void Persist(string entityName, object obj) {
			session.Persist(entityName, obj);
		}

		public object SaveOrUpdateCopy(object obj) {
			return CommitAndEvict(() => session.SaveOrUpdateCopy(obj), obj);
		}

		public void Delete(object obj) {
			CommitAndEvict(() => session.Delete(obj), obj);
		}

		public object SaveOrUpdateCopy(object obj, object id) {
			return CommitAndEvict(() => session.SaveOrUpdateCopy(obj, id), obj);
		}

		public void Delete(string entityName, object obj) {
			CommitAndEvict(() => session.Delete(entityName, obj), obj);
		}

		public int Delete(string query) {
			return session.Delete(query);
		}

		public int Delete(string query, object value, IType type) {
			return session.Delete(query, value, type);
		}

		public int Delete(string query, object[] values, IType[] types) {
			return session.Delete(query, values, types);
		}

		public void Lock(object obj, LockMode lockMode) {
			session.Lock(obj, lockMode);
		}

		public void Lock(string entityName, object obj, LockMode lockMode) {
			session.Lock(entityName, obj, lockMode);
		}

		public void Refresh(object obj) {
			session.Refresh(obj);
		}

		public void Refresh(object obj, LockMode lockMode) {
			session.Refresh(obj, lockMode);
		}

		public LockMode GetCurrentLockMode(object obj) {
			return session.GetCurrentLockMode(obj);
		}

		public ITransaction BeginTransaction() {
			return session.BeginTransaction();
		}

		public ITransaction BeginTransaction(IsolationLevel isolationLevel) {
			return session.BeginTransaction(isolationLevel);
		}

		public ICriteria CreateCriteria<T>() where T : class {
			return session.CreateCriteria<T>();
		}

		public ICriteria CreateCriteria<T>(string alias) where T : class {
			return session.CreateCriteria<T>(alias);
		}

		public ICriteria CreateCriteria(Type persistentClass) {
			return session.CreateCriteria(persistentClass);
		}

		public ICriteria CreateCriteria(Type persistentClass, string alias) {
			return session.CreateCriteria(persistentClass, alias);
		}

		public ICriteria CreateCriteria(string entityName) {
			return session.CreateCriteria(entityName);
		}

		public ICriteria CreateCriteria(string entityName, string alias) {
			return session.CreateCriteria(entityName, alias);
		}

		public IQueryOver<T, T> QueryOver<T>() where T : class {
			return session.QueryOver<T>();
		}

		public IQueryOver<T, T> QueryOver<T>(string entityName) where T : class {
			return session.QueryOver<T>(entityName);
		}

		public IQueryOver<T, T> QueryOver<T>(Expression<Func<T>> alias) where T : class {
			return session.QueryOver(alias);
		}

		public IQueryOver<T, T> QueryOver<T>(string entityName, Expression<Func<T>> alias) where T : class {
			return session.QueryOver(entityName, alias);
		}

		public IQuery CreateQuery(string queryString) {
			return session.CreateQuery(queryString);
		}

		public IQuery CreateFilter(object collection, string queryString) {
			return session.CreateFilter(collection, queryString);
		}

		public IQuery GetNamedQuery(string queryName) {
			return session.GetNamedQuery(queryName);
		}

		public ISQLQuery CreateSQLQuery(string queryString) {
			return session.CreateSQLQuery(queryString);
		}

		public void Clear() {
			session.Clear();
		}

		public object Get(Type clazz, object id) {
			return session.Get(clazz, id);
		}

		public object Get(Type clazz, object id, LockMode lockMode) {
			return session.Get(clazz, id, lockMode);
		}

		public object Get(string entityName, object id) {
			return session.Get(entityName, id);
		}

		public T Get<T>(object id) {
			return session.Get<T>(id);
		}

		public T Get<T>(object id, LockMode lockMode) {
			return session.Get<T>(id, lockMode);
		}

		public string GetEntityName(object obj) {
			return session.GetEntityName(obj);
		}

		public IFilter EnableFilter(string filterName) {
			return session.EnableFilter(filterName);
		}

		public IFilter GetEnabledFilter(string filterName) {
			return session.GetEnabledFilter(filterName);
		}

		public void DisableFilter(string filterName) {
			session.DisableFilter(filterName);
		}

		public IMultiQuery CreateMultiQuery() {
			return session.CreateMultiQuery();
		}

		public ISession SetBatchSize(int batchSize) {
			return session.SetBatchSize(batchSize);
		}

		public ISessionImplementor GetSessionImplementation() {
			return session.GetSessionImplementation();
		}

		public IMultiCriteria CreateMultiCriteria() {
			return session.CreateMultiCriteria();
		}

		public ISession GetSession(EntityMode entityMode) {
			return session.GetSession(entityMode);
		}

		public EntityMode ActiveEntityMode {
			get { return session.ActiveEntityMode; } }

		public FlushMode FlushMode { get { return session.FlushMode; }
			set { session.FlushMode = value; } }

		public CacheMode CacheMode { get { return session.CacheMode; }
			set { session.CacheMode = value; } }

		public ISessionFactory SessionFactory {
			get { return session.SessionFactory; } }

		public IDbConnection Connection {
			get { return session.Connection; } }

		public bool IsOpen {
			get { return session.IsOpen; } }

		public bool IsConnected {
			get { return session.IsConnected; } }

		public bool DefaultReadOnly { get { return session.DefaultReadOnly; }
			set { session.DefaultReadOnly = value; } }

		public ITransaction Transaction {
			get { return session.Transaction; } }

		public ISessionStatistics Statistics {
			get { return session.Statistics; } }

		
	}
}