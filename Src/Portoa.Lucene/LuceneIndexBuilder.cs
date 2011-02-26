using System.Collections.Generic;
using JetBrains.Annotations;
using Lucene.Net.Index;
using Portoa.Logging;
using Portoa.Search;

namespace Portoa.Lucene {
	/// <summary>
	/// <see cref="ISearchIndexBuilder{T}"/> implementation based on <c>Lucene.NET</c>
	/// </summary>
	/// <typeparam name="T">The object to build the index for</typeparam>
	public abstract class LuceneIndexBuilder<T> : ISearchIndexBuilder<T> {
		/// <param name="indexWriter">The index writer to use to build/update indexes</param>
		/// <param name="documentHandler">Object to handle manipulation between indexable objects and Lucene documents</param>
		/// <param name="logger">Application logger</param>
		public LuceneIndexBuilder([NotNull]IndexWriter indexWriter, [NotNull]ILuceneDocumentHandler<T> documentHandler, [NotNull]ILogger logger) {
			IndexWriter = indexWriter;
			DocumentHandler = documentHandler;
			Logger = logger;
		}

		/// <summary>
		/// Gets the index writer to use to build/update indexes
		/// </summary>
		protected IndexWriter IndexWriter { get; private set; }

		/// <summary>
		/// Gets the object to handle manipulation between indexable objects and Lucene documents
		/// </summary>
		protected ILuceneDocumentHandler<T> DocumentHandler { get; private set; }

		/// <summary>
		/// Application logger
		/// </summary>
		protected ILogger Logger { get; private set; }

		public void BuildIndex() {
			Logger.Info("Building lucene index");
			foreach (var entity in GetAllIndexableRecords()) {
				IndexWriter.AddDocument(DocumentHandler.BuildDocument(entity));
			}

			IndexWriter.Optimize();
			IndexWriter.Commit();
			Logger.Info("Finished building lucene index");
		}

		/// <summary>
		/// Gets all indexable records; used for (re)building the index from scratch
		/// </summary>
		[NotNull]
		protected abstract IEnumerable<T> GetAllIndexableRecords();

		public void UpdateIndex(T indexableObject) {
			if (!CanUpdateIndex(indexableObject)) {
				throw new SearchIndexException(string.Format("Cannot update index for object {0}", indexableObject));
			}

			Logger.Info(string.Format("Updating index for {0}", indexableObject));
			//delete current document, if it exists
			IndexWriter.DeleteDocuments(DocumentHandler.GetIdTerm(indexableObject));
			IndexWriter.AddDocument(DocumentHandler.BuildDocument(indexableObject));

			IndexWriter.Commit();
			Logger.Info(string.Format("Finished updating index for {0}", indexableObject));
		}

		/// <summary>
		/// Determines whether <paramref name="objectToVerify"/> can have its index updated
		/// </summary>
		protected virtual bool CanUpdateIndex(T objectToVerify) {
			return !Equals(objectToVerify, default(T));
		}
	}
}