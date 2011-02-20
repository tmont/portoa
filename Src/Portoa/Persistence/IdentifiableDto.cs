namespace Portoa.Persistence {
	/// <summary>
	/// Convenience class for data transfer objects that have an integral
	/// identifier
	/// </summary>
	public abstract class IdentifiableDto : IIdentifiable<int> {
		public int Id { get; set; }
	}
}