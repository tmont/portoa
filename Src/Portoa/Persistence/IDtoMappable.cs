namespace Portoa.Persistence {
	/// <summary>
	/// Exposes an interface to map an entity to a DTO representation
	/// </summary>
	/// <typeparam name="TDto">The DTO to map to</typeparam>
	/// <seealso cref="IdentifiableDto"/>
	public interface IDtoMappable<out TDto> {
		/// <summary>
		/// Returns a DTO representation of this object
		/// </summary>
		TDto ToDto();
	}
}