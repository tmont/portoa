using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Portoa.Util;
using Portoa.Validation.DataAnnotations;

namespace Portoa.Web.Unity.Validation {
	public class BuildUpAttributeProvider : IValidationAttributeProvider {
		private readonly IUnityContainer container;

		public BuildUpAttributeProvider(IUnityContainer container) {
			this.container = container;
		}

		public IEnumerable<ValidationAttribute> GetAttributes(ICustomAttributeProvider attributeProvider) {
			return attributeProvider
				.GetAttributes<ValidationAttribute>()
				.Select(attr => container.BuildUp(attr.GetType(), attr))
				.Cast<ValidationAttribute>();
		}
	}
}