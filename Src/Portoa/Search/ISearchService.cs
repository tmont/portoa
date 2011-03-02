using System.Collections.Generic;
using Portoa.Persistence;

namespace Portoa.Search {
	/// <summary>
	/// Exposes methods to perform full-text searching and indexing
	/// </summary>
	/// <typeparam name="T">The type of entity to search/index</typeparam>
	public interface ISearchService<out T, TId> where T : IIdentifiable<TId> {
		/// <summary>
		/// Gets all indexable entities that match the given set of <paramref name="ids"/>
		/// </summary>
		IEnumerable<T> FindByIds(IEnumerable<TId> ids);

		/// <summary>
		/// Retrieves all indexable records
		/// </summary>
		IEnumerable<T> GetAllIndexableRecords();

		/// <summary>
		/// Converts the string value of the identifier (as stored in the search index) to
		/// its proper type
		/// </summary>
		/// <param name="idValue">The string value of the identifier</param>
		TId ConvertIdFromStringValue(string idValue);
	}
}