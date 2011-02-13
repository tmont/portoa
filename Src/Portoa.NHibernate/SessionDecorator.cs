using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Stat;
using NHibernate.Type;

namespace Portoa.NHibernate {
	/// <summary>
	/// Empty session decorator, for easier implementing of the <c>ISession</c> object
	/// </summary>
	/// <remarks>This is why you don't create interfaces with dozens of methods.</remarks>
	[DebuggerNonUserCode]
	public class SessionDecorator : ISession {
		/// <summary>
		/// The <c>ISession</c> being decorated
		/// </summary>
		protected ISession Session { get; private set; }

		public SessionDecorator(ISession session) {
			Session = session;
		}

		public virtual void Dispose() {
			Session.Dispose();
		}

		public virtual void Flush() {
			Session.Flush();
		}

		public virtual IDbConnection Disconnect() {
			return Session.Disconnect();
		}

		public virtual void Reconnect() {
			Session.Reconnect();
		}

		public virtual void Reconnect(IDbConnection connection) {
			Session.Reconnect(connection);
		}

		public virtual IDbConnection Close() {
			return Session.Close();
		}

		public virtual void CancelQuery() {
			Session.CancelQuery();
		}

		public virtual bool IsDirty() {
			return Session.IsDirty();
		}

		public virtual object GetIdentifier(object obj) {
			return Session.GetIdentifier(obj);
		}

		public virtual bool Contains(object obj) {
			return Session.Contains(obj);
		}

		public virtual void Evict(object obj) {
			Session.Evict(obj);
		}

		public virtual object Load(Type theType, object id, LockMode lockMode) {
			return Session.Load(theType, id, lockMode);
		}

		public virtual object Load(string entityName, object id, LockMode lockMode) {
			return Session.Load(entityName, id, lockMode);
		}

		public virtual object Load(Type theType, object id) {
			return Session.Load(theType, id);
		}

		public virtual T Load<T>(object id, LockMode lockMode) {
			return Session.Load<T>(id, lockMode);
		}

		public virtual T Load<T>(object id) {
			return Session.Load<T>(id);
		}

		public object Load(string entityName, object id) {
			return Session.Load(entityName, id);
		}

		public virtual void Load(object obj, object id) {
			Session.Load(obj, id);
		}

		public virtual void Replicate(object obj, ReplicationMode replicationMode) {
			Session.Replicate(obj, replicationMode);
		}

		public virtual void Replicate(string entityName, object obj, ReplicationMode replicationMode) {
			Session.Replicate(entityName, obj, replicationMode);
		}

		public virtual object Save(object obj) {
			return Session.Save(obj);
		}

		public virtual void Save(object obj, object id) {
			Session.Save(obj, id);
		}

		public virtual object Save(string entityName, object obj) {
			return Session.Save(entityName, obj);
		}

		public virtual void SaveOrUpdate(object obj) {
			Session.SaveOrUpdate(obj);
		}

		public virtual void SaveOrUpdate(string entityName, object obj) {
			Session.SaveOrUpdate(entityName, obj);
		}

		public virtual void Update(object obj) {
			Session.Update(obj);
		}

		public virtual void Update(object obj, object id) {
			Session.Update(obj, id);
		}

		public virtual void Update(string entityName, object obj) {
			Session.Update(entityName, obj);
		}

		public virtual object Merge(object obj) {
			return Session.Merge(obj);
		}

		public virtual object Merge(string entityName, object obj) {
			return Session.Merge(entityName, obj);
		}

		public virtual void Persist(object obj) {
			Session.Persist(obj);
		}

		public virtual void Persist(string entityName, object obj) {
			Session.Persist(entityName, obj);
		}

		public virtual object SaveOrUpdateCopy(object obj) {
			return Session.SaveOrUpdateCopy(obj);
		}

		public virtual object SaveOrUpdateCopy(object obj, object id) {
			return Session.SaveOrUpdateCopy(obj, id);
		}

		public virtual void Delete(object obj) {
			Session.Delete(obj);
		}

		public virtual void Delete(string entityName, object obj) {
			Session.Delete(entityName, obj);
		}

		public virtual IList Find(string query) {
			return Session.Find(query);
		}

		public virtual IList Find(string query, object value, IType type) {
			return Session.Find(query, value, type);
		}

		public virtual IList Find(string query, object[] values, IType[] types) {
			return Session.Find(query, values, types);
		}

		public virtual IEnumerable Enumerable(string query) {
			return Session.Enumerable(query);
		}

		public virtual IEnumerable Enumerable(string query, object value, IType type) {
			return Session.Enumerable(query, value, type);
		}

		public virtual IEnumerable Enumerable(string query, object[] values, IType[] types) {
			return Session.Enumerable(query, values, types);
		}

		public virtual ICollection Filter(object collection, string filter) {
			return Session.Filter(collection, filter);
		}

		public virtual ICollection Filter(object collection, string filter, object value, IType type) {
			return Session.Filter(collection, filter, value, type);
		}

		public virtual ICollection Filter(object collection, string filter, object[] values, IType[] types) {
			return Session.Filter(collection, filter, values, types);
		}

		public virtual int Delete(string query) {
			return Session.Delete(query);
		}

		public virtual int Delete(string query, object value, IType type) {
			return Session.Delete(query, value, type);
		}

		public virtual int Delete(string query, object[] values, IType[] types) {
			return Session.Delete(query, values, types);
		}

		public virtual void Lock(object obj, LockMode lockMode) {
			Session.Lock(obj, lockMode);
		}

		public virtual void Lock(string entityName, object obj, LockMode lockMode) {
			Session.Lock(entityName, obj, lockMode);
		}

		public virtual void Refresh(object obj) {
			Session.Refresh(obj);
		}

		public virtual void Refresh(object obj, LockMode lockMode) {
			Session.Refresh(obj, lockMode);
		}

		public virtual LockMode GetCurrentLockMode(object obj) {
			return Session.GetCurrentLockMode(obj);
		}

		public virtual ITransaction BeginTransaction() {
			return Session.BeginTransaction();
		}

		public virtual ITransaction BeginTransaction(IsolationLevel isolationLevel) {
			return Session.BeginTransaction(isolationLevel);
		}

		public virtual ICriteria CreateCriteria<T>() where T : class {
			return Session.CreateCriteria<T>();
		}

		public virtual ICriteria CreateCriteria<T>(string alias) where T : class {
			return Session.CreateCriteria<T>(alias);
		}

		public virtual ICriteria CreateCriteria(Type persistentClass) {
			return Session.CreateCriteria(persistentClass);
		}

		public virtual ICriteria CreateCriteria(Type persistentClass, string alias) {
			return Session.CreateCriteria(persistentClass, alias);
		}

		public virtual ICriteria CreateCriteria(string entityName) {
			return Session.CreateCriteria(entityName);
		}

		public virtual ICriteria CreateCriteria(string entityName, string alias) {
			return Session.CreateCriteria(entityName, alias);
		}

		public virtual IQuery CreateQuery(string queryString) {
			return Session.CreateQuery(queryString);
		}

		public virtual IQuery CreateFilter(object collection, string queryString) {
			return Session.CreateFilter(collection, queryString);
		}

		public virtual IQuery GetNamedQuery(string queryName) {
			return Session.GetNamedQuery(queryName);
		}

		public virtual IQuery CreateSQLQuery(string sql, string returnAlias, Type returnClass) {
			return Session.CreateSQLQuery(sql, returnAlias, returnClass);
		}

		public virtual IQuery CreateSQLQuery(string sql, string[] returnAliases, Type[] returnClasses) {
			return Session.CreateSQLQuery(sql, returnAliases, returnClasses);
		}

		public virtual ISQLQuery CreateSQLQuery(string queryString) {
			return Session.CreateSQLQuery(queryString);
		}

		public virtual void Clear() {
			Session.Clear();
		}

		public virtual object Get(Type clazz, object id) {
			return Session.Get(clazz, id);
		}

		public virtual object Get(Type clazz, object id, LockMode lockMode) {
			return Session.Get(clazz, id, lockMode);
		}

		public virtual object Get(string entityName, object id) {
			return Session.Get(entityName, id);
		}

		public virtual T Get<T>(object id) {
			return Session.Get<T>(id);
		}

		public virtual T Get<T>(object id, LockMode lockMode) {
			return Session.Get<T>(id, lockMode);
		}

		public virtual string GetEntityName(object obj) {
			return Session.GetEntityName(obj);
		}

		public virtual IFilter EnableFilter(string filterName) {
			return Session.EnableFilter(filterName);
		}

		public virtual IFilter GetEnabledFilter(string filterName) {
			return Session.GetEnabledFilter(filterName);
		}

		public virtual void DisableFilter(string filterName) {
			Session.DisableFilter(filterName);
		}

		public virtual IMultiQuery CreateMultiQuery() {
			return Session.CreateMultiQuery();
		}

		public virtual ISession SetBatchSize(int batchSize) {
			return Session.SetBatchSize(batchSize);
		}

		public virtual ISessionImplementor GetSessionImplementation() {
			return Session.GetSessionImplementation();
		}

		public virtual IMultiCriteria CreateMultiCriteria() {
			return Session.CreateMultiCriteria();
		}

		public virtual ISession GetSession(EntityMode entityMode) {
			return Session.GetSession(entityMode);
		}

		public virtual EntityMode ActiveEntityMode {
			get { return Session.ActiveEntityMode; }
		}

		public virtual FlushMode FlushMode {
			get { return Session.FlushMode; }
			set { Session.FlushMode = value; }
		}

		public virtual CacheMode CacheMode {
			get { return Session.CacheMode; }
			set { Session.CacheMode = value; }
		}

		public virtual ISessionFactory SessionFactory {
			get { return Session.SessionFactory; }
		}

		public virtual IDbConnection Connection {
			get { return Session.Connection; }
		}

		public virtual bool IsOpen {
			get { return Session.IsOpen; }
		}

		public virtual bool IsConnected {
			get { return Session.IsConnected; }
		}

		public virtual ITransaction Transaction {
			get { return Session.Transaction; }
		}

		public virtual ISessionStatistics Statistics {
			get { return Session.Statistics; }
		}
	}
}