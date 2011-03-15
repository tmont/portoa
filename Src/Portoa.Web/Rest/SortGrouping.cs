namespace Portoa.Web.Rest {
	/// <summary>
	/// Represents the order and field/property to sort by
	/// </summary>
	public class SortGrouping {
		/// <summary>
		/// Gets or sets the sort order
		/// </summary>
		public SortOrder Order { get; set; }
		/// <summary>
		/// Gets or sets the name of the field/property to sort by
		/// </summary>
		public string Field { get; set; }
	}
}