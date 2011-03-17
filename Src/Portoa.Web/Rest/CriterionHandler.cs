using System;
using System.Collections.Generic;
using System.Linq;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Base class for handling criterion passed in URL/query string of a RESTful request
	/// </summary>
	/// <typeparam name="T">The type of object that the criterion is for</typeparam>
	public abstract class CriterionHandler<T> {
		/// <summary>
		/// Creates a collection of <c>WHERE</c> clauses suitable for translating
		/// to SQL or otherwise filtering a data set
		/// </summary>
		public IEnumerable<Func<T, bool>> HandleCriterion(Criterion criterion) {
			return criterion.Values.Select(fieldValue => HandleValue(criterion.FieldName, fieldValue));
		}

		/// <summary>
		/// If overridden converts criterion values to a dataset filter
		/// </summary>
		/// <param name="fieldName">The name of the field the <paramref name="value"/> is associated with</param>
		/// <param name="value">The criterion value</param>
		/// <exception cref="RestException">If the value is invalid</exception>
		protected virtual Func<T, bool> HandleValue(string fieldName, CriterionFieldValue value) {
			throw new RestException(string.Format("The value \"{0}\" is invalid for field \"{1}\"", value, fieldName));
		}
	}
}