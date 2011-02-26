using System.Collections.Generic;
using JetBrains.Annotations;
using Lucene.Net.Index;
using Portoa.Logging;
using Portoa.Persistence;
using Portoa.Search;

namespace Portoa.Lucene {
	/// <summary>
	/// <see cref="ISearchIndexBuilder{T}"/> implementation for entities based on <c>Lucene.NET</c>
	/// </summary>
	/// <typeparam name="T">The entity to build the index for</typeparam>
	public class LuceneEntityIndexBuilder<T> : LuceneIndexBuilder<T> where T : Entity<T, int> {
		private readonly ISearchService<T> searchService;

		public LuceneEntityIndexBuilder([NotNull]IndexWriter indexWriter, [NotNull]ILuceneDocumentHandler<T> documentHandler, [NotNull]ILogger logger, [NotNull]ISearchService<T> searchService)
			: base(indexWriter, documentHandler, logger) {
			this.searchService = searchService;
		}

		protected override IEnumerable<T> GetAllIndexableRecords() {
			return searchService.GetAllIndexableRecords();
		}

		protected override bool CanUpdateIndex(T objectToVerify) {
			return objectToVerify != null && !objectToVerify.IsTransient();
		}
	}
}