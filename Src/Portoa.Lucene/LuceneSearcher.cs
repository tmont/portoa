using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Portoa.Search;

namespace Portoa.Lucene {
	/// <summary>
	/// <see cref="ISearcher{T}"/> implementation based on <c>Lucene.NET</c>
	/// </summary>
	public abstract class LuceneSearcher<T> : ISearcher<T> {
		protected LuceneSearcher(QueryParser queryParser, Directory indexDirectory) {
			QueryParser = queryParser;
			IndexDirectory = indexDirectory;
		}

		/// <summary>
		/// Gets the query parser associated with this searcher
		/// </summary>
		protected QueryParser QueryParser { get; private set; }

		/// <summary>
		/// Gets the index directory associated with this searcher
		/// </summary>
		protected Directory IndexDirectory { get; private set; }

		public IEnumerable<SearchResult<T>> Search(string searchString, int maxResults) {
			if (string.IsNullOrWhiteSpace(searchString)) {
				return Enumerable.Empty<SearchResult<T>>();
			}

			if (maxResults < 0) {
				throw new ArgumentOutOfRangeException("maxResults", maxResults, "Maximum number of results must be greater than or equal to zero");
			}
			if (maxResults == 0) {
				maxResults = int.MaxValue;
			}

			var query = QueryParser.Parse(EscapeSearchString(searchString));
			var searcher = new IndexSearcher(IndexDirectory, true);
			try {
				var scoreDocs = searcher.Search(query, maxResults).scoreDocs;
				return TransformResults(scoreDocs, searcher);
			} finally {
				searcher.Close();
			}
		}

		/// <summary>
		/// Transforms the results of a search into a proper result set
		/// </summary>
		/// <param name="scoreDocs">The search results</param>
		/// <param name="searcher">The searcher that performed the search</param>
		protected abstract SearchResult<T>[] TransformResults(ScoreDoc[] scoreDocs, IndexSearcher searcher);

		/// <summary>
		/// Escapes a search string; default implementation calls <c>QueryParser.Escape(<paramref name="searchString"/>)</c>
		/// </summary>
		/// <param name="searchString">The search string to escape</param>
		protected virtual string EscapeSearchString(string searchString) {
			return QueryParser.Escape(searchString);
		}
	}
}