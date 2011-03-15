using System;
using System.Collections.Generic;

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
		/// <param name="values">The collection of criterion values</param>
		public IEnumerable<Func<T, bool>> HandleCriterion(IEnumerable<object> values) {
			foreach (var value in values) {
				if (value is int) {
					yield return HandleInteger((int)value);
				} else {
					yield return HandleString((string)value ?? string.Empty);
				}
			}
		}

		/// <summary>
		/// If overridden handles converts integral criterion values to a dataset filter
		/// </summary>
		/// <param name="value">The criterion value</param>
		/// <exception cref="RestException">If the value is invalid</exception>
		protected virtual Func<T, bool> HandleInteger(int value) {
			throw new RestException(string.Format("The value \"{0}\" is invalid", value));
		}

		/// <summary>
		/// If overridden handles converts string criterion values to a dataset filter
		/// </summary>
		/// <param name="value">The criterion value (this value is never <c>null</c>)</param>
		/// <exception cref="RestException">If the value is invalid</exception>
		protected virtual Func<T, bool> HandleString(string value) {
			throw new RestException(string.Format("The value \"{0}\" is invalid", value));
		}
	}
}