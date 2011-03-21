namespace Portoa.Web.Rest {
	/// <summary>
	/// Represents the order and field/property to sort by
	/// </summary>
	public class SortGrouping {
		public SortGrouping() {
			Order = SortOrder.Ascending;
		}

		/// <summary>
		/// Gets or sets the sort order; default is <see cref="SortOrder.Ascending"/>
		/// </summary>
		public SortOrder Order { get; set; }

		/// <summary>
		/// Gets or sets the name of the field/property to sort by
		/// </summary>
		public string Field { get; set; }
	}
}