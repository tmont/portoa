namespace Portoa.Persistence {
	/// <summary>
	/// Convenience class for data transfer objects that have an integral
	/// identifier
	/// </summary>
	public abstract class IdentifiableDto : IIdentifiable<int> {
		/// <summary>
		/// Gets or sets the identifier of this object
		/// </summary>
		public int Id { get; set; }
	}
}