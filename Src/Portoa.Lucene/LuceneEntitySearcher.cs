using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Portoa.Persistence;
using Portoa.Search;

namespace Portoa.Lucene {
	/// <summary>
	/// <see cref="ISearcher{T, TId}"/> implementation for entities based on <c>Lucene.NET</c>
	/// </summary>
	public class LuceneEntitySearcher<T, TId> : ISearcher<T, TId> where T : IIdentifiable<TId> {
		private readonly QueryParser queryParser;
		private readonly Directory indexDirectory;
		private readonly ISearchService<T, TId> searchService;

		public LuceneEntitySearcher(QueryParser queryParser, Directory indexDirectory, ISearchService<T, TId> searchService) {
			this.queryParser = queryParser;
			this.indexDirectory = indexDirectory;
			this.searchService = searchService;
			IdFieldName = "id";
		}

		/// <summary>
		/// Gets or sets the name of the field (in Lucene) that stores the identifier; the default is "<c>id</c>"
		/// </summary>
		public string IdFieldName { get; set; }

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

			var query = queryParser.Parse(QueryParser.Escape(searchString));
			var searcher = new IndexSearcher(indexDirectory, true);
			try {
				var scoreDocs = searcher.Search(query, maxResults).scoreDocs;
				return TransformResults(scoreDocs, searcher);
			} finally {
				searcher.Close();
			}
		}

		private IEnumerable<SearchResult<T>> TransformResults(IEnumerable<ScoreDoc> scoreDocs, Searchable searcher) {
			if (!scoreDocs.Any()) {
				return Enumerable.Empty<SearchResult<T>>();
			}

			var ids = scoreDocs.Select(doc => searchService.ConvertIdFromStringValue(searcher.Doc(doc.doc).GetField(IdFieldName).StringValue()));

			return searchService
				.FindByIds(ids)
				.ToArray() //must array-ify so that the query is actually executed
				.Zip(scoreDocs, (entity, doc) => new SearchResult<T> { Record = entity, Score = doc.score });
		}
	}
}