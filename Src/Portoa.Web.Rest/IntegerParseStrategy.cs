namespace Portoa.Web.Rest {
	/// <summary>
	/// Parses a <c cref="CriterionFieldValue.RawValue">raw criterion value</c> as an <see cref="int"/>
	/// </summary>
	public sealed class IntegerParseStrategy : IFieldValueParseStrategy {
		public bool Parse(string rawValue, ref object value) {
			int i;
			if (!int.TryParse(rawValue, out i)) {
				return false;
			}

			value = i;
			return true;
		}
	}
}