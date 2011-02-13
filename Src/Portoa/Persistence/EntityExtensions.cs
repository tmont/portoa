namespace Portoa.Persistence {
	public static class EntityExtensions {
		/// <summary>
		/// Determines if the entity has already been persisted
		/// </summary>
		public static bool IsNew<T, TId>(this Entity<T, TId> entity) where T : Entity<T, TId> {
			if (entity == null) {
				return false;
			}

// ReSharper disable CompareNonConstrainedGenericWithNull
			return typeof(TId).IsValueType ? entity.Id.Equals(default(TId)) : entity.Id != null;
// ReSharper restore CompareNonConstrainedGenericWithNull
		}
	}
}