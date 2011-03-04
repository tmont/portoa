using System;
using JetBrains.Annotations;
using Lucene.Net.Index;
using Portoa.Logging;
using Portoa.Persistence;
using Portoa.Search;

namespace Portoa.Lucene {
	/// <summary>
	/// <see cref="ISearchIndexBuilder{T, TId}"/> implementation for entities based on <c>Lucene.NET</c>
	/// </summary>
	/// <typeparam name="T">The entity to build the index for</typeparam>
	/// <typeparam name="TId">The entity identifier type</typeparam>
	public class LuceneEntityIndexBuilder<T, TId> : ISearchIndexBuilder<T, TId> where T : IIdentifiable<TId> {
		private readonly IndexWriter indexWriter;
		private readonly ILuceneDocumentHandler<T> documentHandler;
		private readonly ILogger logger;
		private readonly ISearchService<T, TId> searchService;

		public LuceneEntityIndexBuilder([NotNull]IndexWriter indexWriter, [NotNull]ILuceneDocumentHandler<T> documentHandler, [NotNull]ILogger logger, [NotNull]ISearchService<T, TId> searchService) {
			this.indexWriter = indexWriter;
			this.documentHandler = documentHandler;
			this.logger = logger;
			this.searchService = searchService;
		}

		public void BuildIndex() {
			logger.Info("Building lucene index");
			foreach (var entity in searchService.GetAllIndexableRecords()) {
				indexWriter.AddDocument(documentHandler.BuildDocument(entity));
			}

			indexWriter.Optimize();
			indexWriter.Commit();
			logger.Info("Finished building lucene index");
		}

		public void UpdateIndex(T indexableObject) {
			if (!CanUpdateIndex(indexableObject)) {
				throw new SearchIndexException(string.Format("Cannot update index for object {0}", indexableObject));
			}

			logger.Info(string.Format("Updating index for {0}", indexableObject));
			//delete current document, if it exists
			RemoveDocument(indexableObject);
			indexWriter.AddDocument(documentHandler.BuildDocument(indexableObject));

			indexWriter.Commit();
			logger.Info(string.Format("Finished updating index for {0}", indexableObject));
		}

		public void DeleteIndex(T entity) {
			logger.Info("Removing index for {0}", entity);
			RemoveDocument(entity);
			indexWriter.Commit();
			logger.Info("Finished removing index for {0}", entity);
		}

		private void RemoveDocument(T entity) {
			indexWriter.DeleteDocuments(documentHandler.GetIdTerm(entity));
		}

		private static bool CanUpdateIndex(T objectToVerify) {
			return !Equals(objectToVerify, default(T)) && !objectToVerify.IsTransient();
		}
	}
}