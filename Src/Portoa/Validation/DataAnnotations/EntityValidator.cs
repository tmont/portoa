using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Portoa.Validation.DataAnnotations {
	/// <summary>
	/// Default entity validator for validating entities annotated with attributes
	/// derived from <see cref="ValidationAttribute"/>
	/// </summary>
	public class EntityValidator : IEntityValidator {
		private readonly IServiceProvider provider;

		public EntityValidator(IServiceProvider provider) {
			this.provider = provider;
		}

		public IEnumerable<IValidationResult> Validate(object instance, bool stopOnFirstError = false) {
			var results = new Collection<ValidationResult>();
			Validator.TryValidateObject(instance, new ValidationContext(instance, provider, null), results, validateAllProperties: !stopOnFirstError);
			return results.Select(result => new ResultAdapter(result));
		}
	}
}