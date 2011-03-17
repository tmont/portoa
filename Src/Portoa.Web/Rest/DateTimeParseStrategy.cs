using System;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Parses a <c cref="CriterionFieldValue.RawValue">raw criterion value</c> as a <see cref="DateTime"/>
	/// </summary>
	public sealed class DateTimeParseStrategy : IFieldValueParseStrategy {
		public bool Parse(string rawValue, ref object value) {
			DateTime date;
			if (!DateTime.TryParse(rawValue, out date)) {
				return false;
			}

			value = date;
			return true;
		}
	}
}