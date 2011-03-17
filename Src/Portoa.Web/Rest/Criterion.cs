using System.Collections.Generic;
using System.Linq;

namespace Portoa.Web.Rest {
	public class Criterion {
		public Criterion() {
			Values = Enumerable.Empty<CriterionFieldValue>();
		}

		public string FieldName { get; set; }
		public IEnumerable<CriterionFieldValue> Values { get; set; }

		public void Add(CriterionFieldValue value) {
			Values = Values.Concat(new[] { value });
		}
	}
}