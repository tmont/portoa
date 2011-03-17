using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Portoa.Util;

namespace Portoa.Web.Rest {
	public class CriterionSet : IEnumerable<Criterion> {
		private readonly IList<Criterion> criteria = new List<Criterion>();

		public CriterionSet(IEnumerable<Criterion> criteria = null) {
			if (criteria != null) {
				this.criteria.AddRange(criteria);
			}
		}

		public Criterion this[string field] { get { return criteria.SingleOrDefault(criterion => criterion.FieldName == field); } }

		public void Add(Criterion criterion) {
			if (criteria.Any(c => c.FieldName == criterion.FieldName)) {
				throw new ArgumentException(string.Format("Criterion with field \"{0}\" is already in the set", criterion.FieldName));
			}

			criteria.Add(criterion);
		}

		public void Add(string field, IEnumerable<object> values) {
			Add(new Criterion {
				FieldName = field,
				Values = values.Select(value => new CriterionFieldValue { Value = value })
			});
		}

		public IEnumerator<Criterion> GetEnumerator() {
			return criteria.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public int Count { get { return criteria.Count; } }
	}
}