namespace Portoa.Persistence {
	public static class EntityExtensions {
		public static bool IsNew<T, TId>(this Entity<T, TId> entity) where T : Entity<T, TId> {
			if (entity == null) {
				return false;
			}

			return typeof(TId).IsValueType ? entity.Id.Equals(default(TId)) : entity.Id != null;
		}
	}
}