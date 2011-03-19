using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Portoa.Util;

namespace Portoa.Persistence {
	public static class EntityExtensions {
		private static readonly MethodInfo identifiableIsTransientMethod;

		static EntityExtensions() {
			identifiableIsTransientMethod = typeof(EntityExtensions)
				.GetMethods()
				.Single(method => method.Name == "IsTransient" && method.IsGenericMethodDefinition);
		}

		/// <summary>
		/// Determines if <paramref name="entity"/> has already been persisted
		/// </summary>
		public static bool IsTransient<TId>(this IIdentifiable<TId> entity) {
			return entity != null && Equals(entity.Id, default(TId));
		}

		/// <summary>
		/// Determines if the entity has already been persisted by invoking <see cref="IsTransient{TId}"/> via
		/// reflection if <paramref name="entity"/> implements <see cref="IIdentifiable{T}"/>
		/// </summary>
		/// <exception cref="ArgumentException">If <paramref name="entity"/> does not implement <see cref="IIdentifiable{T}"/></exception>
		public static bool IsTransient([NotNull]this object entity) {
			var type = entity.GetType();
			if (!type.IsAssignableToGenericType(typeof(IIdentifiable<>))) {
				throw new ArgumentException("entity does not implement IIdentifiable<TId>");
			}

			return (bool)identifiableIsTransientMethod
				.MakeGenericMethod(type.GetProperty("Id").PropertyType)
				.Invoke(null, new[] { entity });
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