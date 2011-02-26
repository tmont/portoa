namespace Portoa.Search {
	/// <summary>
	/// Represents a search result
	/// </summary>
	public class SearchResult<T> {
		/// <summary>
		/// A value (between 0 and 1, the higher the better) representing how good
		/// the match is between the search query and the value
		/// </summary>
		public double Score { get; set; }

		/// <summary>
		/// The matched object
		/// </summary>
		public T Record { get; set; }
	}
}