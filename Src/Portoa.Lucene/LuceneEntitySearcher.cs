﻿using System.Linq;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Portoa.Persistence;
using Portoa.Search;

namespace Portoa.Lucene {
	/// <summary>
	/// <see cref="ISearcher{T}"/> implementation for entities based on <c>Lucene.NET</c>
	/// </summary>
	public class LuceneEntitySearcher<T> : LuceneSearcher<T> where T : Entity<T, int> {
		private readonly ISearchService<T> searchService;

		public LuceneEntitySearcher(QueryParser queryParser, Directory indexDirectory, ISearchService<T> searchService) : base(queryParser, indexDirectory) {
			this.searchService = searchService;
			IdFieldName = "id";
		}

		public string IdFieldName { get; set; }

		protected override SearchResult<T>[] TransformResults(ScoreDoc[] scoreDocs, IndexSearcher searcher) {
			var ids = scoreDocs.Select(doc => int.Parse(searcher.Doc(doc.doc).GetField(IdFieldName).StringValue()));

			if (!ids.Any()) {
				return new SearchResult<T>[0];
			}

			var quotes = searchService.FindByIds(ids);
			return quotes
				.Zip(scoreDocs, (entity, doc) => new SearchResult<T> { Record = entity, Score = doc.score })
				.ToArray();
		}
	}
}