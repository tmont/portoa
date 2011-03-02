using Portoa.Persistence;

namespace Portoa.Search {
	/// <summary>
	/// Exposes an interface to build and update a search index
	/// </summary>
	public interface ISearchIndexBuilder<in T> {
		/// <summary>
		/// (Re)builds the search index
		/// </summary>
		void BuildIndex();

		/// <summary>
		/// Updates the index for the specified <paramref name="indexableObject"/>
		/// </summary>
		/// <param name="indexableObject">The object that needs its index updated</param>
		void UpdateIndex(T indexableObject);
	}

	/// <summary>
	/// Exposes an interface to build and update a search index for entities
	/// </summary>
	public interface ISearchIndexBuilder<in T, TId> where T : IIdentifiable<TId> {
		/// <summary>
		/// (Re)builds the search index
		/// </summary>
		void BuildIndex();

		/// <summary>
		/// Updates the index for the specified <paramref name="entity"/>
		/// </summary>
		/// <param name="entity">The entity that needs its index updated</param>
		void UpdateIndex(T entity);
	}
}