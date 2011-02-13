namespace Portoa.Persistence {
	/// <summary>
	/// Represents an objec that is uniquely identifiable
	/// </summary>
	/// <typeparam name="T">The identifier type</typeparam>
	public interface IIdentifiable<T> {
		/// <summary>
		/// The unique identifier of this object
		/// </summary>
		T Id { get; set; }
	}
}