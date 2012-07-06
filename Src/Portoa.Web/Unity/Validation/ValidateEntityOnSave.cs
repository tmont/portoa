using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Persistence;
using Portoa.Validation.DataAnnotations;
using Portoa.Web.Unity.Matching;

namespace Portoa.Web.Unity.Validation {
	/// <summary>
	/// Exposes an interface to configure an entity validation policy
	/// </summary>
	public interface IEntityValidationConfigurator : IUnityContainerExtensionConfigurator {
		/// <summary>
		/// Tells the validator to validate nothing
		/// </summary>
		IEntityValidationConfigurator ClearAllowableTypes();
		/// <summary>
		/// Tells the validator to validate all objects that derive from <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">The type from which the objects should inherit</typeparam>
		IEntityValidationConfigurator ValidateAllThatDeriveFrom<T>();
		/// <summary>
		/// Tells the validator to validate all objects of type <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">The type of object to validate</typeparam>
		IEntityValidationConfigurator Validate<T>();
		/// <summary>
		/// Tells the validator to validate all objects that derive from <paramref name="type"/>
		/// </summary>
		IEntityValidationConfigurator ValidateAllThatDeriveFrom(Type type);
		/// <summary>
		/// Tells the validator to validate all objects of type <paramref name="type"/>
		/// </summary>
		IEntityValidationConfigurator Validate(Type type);
	}

	/// <summary>
	/// Supplies configuration for validating entities when they are saved via a <see cref="IRepository{T}"/>.
	/// By default, the allowable types are anything deriving from <see cref="Entity{TId}"/>.
	/// </summary>
	public class ValidateEntityOnSave : UnityContainerExtension, IEntityValidationConfigurator {
		private readonly IList<Type> specificTypes = new List<Type>();
		private readonly IList<Type> deriveFromTypes = new List<Type>();

		protected override void Initialize() {
			ValidateAllThatDeriveFrom(typeof(Entity<>));

			Container
				.RegisterType<IValidationAttributeProvider, ReflectionValidationAttributeProvider>()
				.Configure<Interception>()
				.AddPolicy("ValidationPolicy")
				.AddCallHandler<ValidationCallHandler>()
				.AddMatchingRule<OnEntitySaved>();
		}

		public IEntityValidationConfigurator ClearAllowableTypes() {
			specificTypes.Clear();
			deriveFromTypes.Clear();
			return this;
		}

		public IEntityValidationConfigurator ValidateAllThatDeriveFrom<T>() {
			return ValidateAllThatDeriveFrom(typeof(T));
		}

		public IEntityValidationConfigurator Validate<T>() {
			return Validate(typeof(T));
		}

		public IEntityValidationConfigurator ValidateAllThatDeriveFrom(Type type) {
			deriveFromTypes.Add(type);
			return this;
		}

		public IEntityValidationConfigurator Validate(Type type) {
			specificTypes.Add(type);
			return this;
		}
	}
}