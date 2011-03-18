using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Portoa.Util;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Encapsulates a collection of criterion
	/// </summary>
	public class CriterionSet : IEnumerable<Criterion> {
		private readonly IList<Criterion> criteria = new List<Criterion>();

		/// <param name="criteria">Optional collection of criteria to initialize the set with</param>
		public CriterionSet(IEnumerable<Criterion> criteria = null) {
			if (criteria != null) {
				criteria.Walk(Add);
			}
		}

		/// <summary>
		/// Retrieves the criterion with the field name specified by <paramref name="field"/> or <c>null</c>
		/// if the key doesn't exist
		/// </summary>
		/// <param name="field">The name of the criterion's field</param>
		/// <returns>The criterion that matches the given field name or <c>null</c></returns>
		[CanBeNull]
		public Criterion this[string field] { get { return criteria.FirstOrDefault(criterion => criterion.FieldName == field); } }

		/// <summary>
		/// Adds a criterion to the collection; criterion in the set must be unique
		/// by <see cref="Criterion.FieldName"/>
		/// </summary>
		/// <param name="criterion">The criterion to add</param>
		/// <exception cref="ArgumentException">If the criterion already exists in the set</exception>
		public void Add(Criterion criterion) {
			if (criteria.Any(c => c.FieldName == criterion.FieldName)) {
				throw new ArgumentException(string.Format("Criterion with field \"{0}\" is already in the set", criterion.FieldName));
			}

			criteria.Add(criterion);
		}

		/// <summary>
		/// Creates a new criterion and adds it to the set
		/// </summary>
		/// <param name="field">The name of the criterion's field</param>
		/// <param name="values">The criterion's values</param>
		/// <see cref="Add(Criterion)"/>
		public void Add(string field, IEnumerable<string> values) {
			Add(new Criterion {
				FieldName = field,
				Values = values.Select(value => new CriterionFieldValue { RawValue = value })
			});
		}

		public IEnumerator<Criterion> GetEnumerator() {
			return criteria.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		/// <summary>
		/// Gets the number of criterion in the set
		/// </summary>
		public int Count { get { return criteria.Count; } }
	}
}