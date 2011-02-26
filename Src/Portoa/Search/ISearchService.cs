using System.Collections.Generic;
using Portoa.Persistence;

namespace Portoa.Search {
	/// <summary>
	/// Exposes methods to perform full-text searching and indexing
	/// </summary>
	/// <typeparam name="T">The type of entity to search/index</typeparam>
	public interface ISearchService<T> where T : Entity<T, int> {
		/// <summary>
		/// Gets all indexable entities that match the given set of <paramref name="ids"/>
		/// </summary>
		IEnumerable<T> FindByIds(IEnumerable<int> ids);

		/// <summary>
		/// Retrieves all indexable records
		/// </summary>
		IEnumerable<T> GetAllIndexableRecords();
	}
}