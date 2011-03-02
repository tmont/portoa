using System.Collections.Generic;
using Portoa.Persistence;

namespace Portoa.Search {
	/// <summary>
	/// Exposes an interface to perform a full-text search
	/// </summary>
	public interface ISearcher<T> {
		/// <summary>
		/// Searches for records based on the given search query
		/// </summary>
		/// <param name="query">The search term(s) to search for</param>
		/// <param name="maxResults">The maximum number of results to return (<c>0</c> is unlimited); the default is <c>10</c></param>
		IEnumerable<SearchResult<T>> Search(string query, int maxResults = 10);
	}

	/// <summary>
	/// Exposes an interface to perform a full-text search on a collection of entities
	/// </summary>
	public interface ISearcher<T, TId> where T : IIdentifiable<TId> {
		/// <summary>
		/// Searches for records based on the given search query
		/// </summary>
		/// <param name="query">The search term(s) to search for</param>
		/// <param name="maxResults">The maximum number of results to return (<c>0</c> is unlimited); the default is <c>10</c></param>
		IEnumerable<SearchResult<T>> Search(string query, int maxResults = 10);
	}
}