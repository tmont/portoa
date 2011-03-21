using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Represents a filter on a field in a RESTful request
	/// </summary>
	public class Criterion {
		public Criterion() {
			Values = Enumerable.Empty<CriterionFieldValue>();
		}

		/// <summary>
		/// Gets or sets the name of the field to filter
		/// </summary>
		public string FieldName { get; set; }

		/// <summary>
		/// Gets or sets the values of the filter
		/// </summary>
		[NotNull]
		public IEnumerable<CriterionFieldValue> Values { get; set; }

		/// <summary>
		/// Adds a value
		/// </summary>
		/// <param name="value">The value to add</param>
		public void Add([NotNull]CriterionFieldValue value) {
			Values = Values.Concat(new[] { value });
		}
	}
}