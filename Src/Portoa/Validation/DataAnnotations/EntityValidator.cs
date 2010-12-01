using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Portoa.Validation.DataAnnotations {
	public class EntityValidator : IEntityValidator {
		private readonly IServiceProvider provider;

		public EntityValidator(IServiceProvider provider) {
			this.provider = provider;
		}

		public IEnumerable<IValidationResult> Validate(object instance) {
			var results = new Collection<ValidationResult>();
			Validator.TryValidateObject(instance, new ValidationContext(instance, provider, null), results, true);
			return results.Select(result => new ResultAdapter(result));
		}
	}

}