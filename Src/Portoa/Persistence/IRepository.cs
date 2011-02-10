﻿using System.Linq;

namespace Portoa.Persistence {

	/// <summary>
	/// Provides a CRUD interface to a persistence medium for entities that have an
	/// integral identifier
	/// </summary>
	/// <typeparam name="T">The entity's type</typeparam>
	public interface IRepository<T> : IRepository<T, int> where T : Entity<T, int> { }

	/// <summary>
	/// Provides a CRUD interface to a persistence medium
	/// </summary>
	/// <typeparam name="T">The entity's type</typeparam>
	/// <typeparam name="TId">The entity identifier's type</typeparam>
	public interface IRepository<T, in TId> where T : Entity<T, TId> {
		/// <summary>
		/// Persists an entity (inserts or updates)
		/// </summary>
		/// <param name="entity">The entity to save</param>
		T Save(T entity);

		/// <summary>
		/// Reloads an entity from the persistence medium
		/// </summary>
		/// <param name="entity">The entity to reload</param>
		T Reload(T entity);

		/// <summary>
		/// Deletes an entity
		/// </summary>
		/// <param name="id">The unique identifier of the entity to delete</param>
		void Delete(TId id);

		/// <summary>
		/// Finds an entity by its unique identifier
		/// </summary>
		/// <param name="id">The unique identifier of the entity to locate</param>
		/// <exception cref="EntityNotFoundException{T,TId}"/>
		T FindById(TId id);

		/// <summary>
		/// Equivalent of "select *"
		/// </summary>
		IQueryable<T> Records { get; }
	}
}