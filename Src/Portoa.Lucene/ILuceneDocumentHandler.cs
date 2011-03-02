using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace Portoa.Lucene {
	/// <summary>
	/// Handles interaction between objects and Lucene documents
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ILuceneDocumentHandler<in T> {
		/// <summary>
		/// Builds a <see cref="Document"/> out of another object
		/// </summary>
		/// <param name="source">The object from which to build the document</param>
		Document BuildDocument(T source);

		/// <summary>
		/// Gets a term representing the identifying <see cref="Field"/> in a
		/// <see cref="Document"/>. This <see cref="Term"/> can be used as a means
		/// to delete documents using an <see cref="IndexWriter"/>, for example.
		/// </summary>
		/// <param name="source">The object from which to create the term</param>
		Term GetIdTerm(T source);
	}
}