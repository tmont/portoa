namespace Portoa.Search {
	/// <summary>
	/// Indicates that this object is searchable
	/// </summary>
	public interface ISearchable {
		/// <summary>
		/// Gets whether or not the index for this object should be updated
		/// (a dirty flag, of sorts)
		/// </summary>
		bool ShouldIndex { get; }
	}
}