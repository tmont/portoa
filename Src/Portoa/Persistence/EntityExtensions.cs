namespace Portoa.Persistence {
	public static class EntityExtensions {
		/// <summary>
		/// Determines if the entity has already been persisted
		/// </summary>
		public static bool IsTransient<TId>(this IIdentifiable<TId> entity) {
			return entity != null && Equals(entity.Id, default(TId));
		}

		/// <summary>
		/// Converts an entity to its DTO representation. If the entity does not implement
		/// <see cref="IDtoMappable{TDto}"/>, then an empty <typeparamref name="TDto"/> object
		/// is returned.
		/// </summary>
		/// <typeparam name="TDto">The type to map the entity to</typeparam>
		public static TDto ToDto<TDto>(this object entity) where TDto : new() {
			var mappable = entity as IDtoMappable<TDto>;
			return mappable == null ? new TDto() : mappable.ToDto();
		}
	}
}