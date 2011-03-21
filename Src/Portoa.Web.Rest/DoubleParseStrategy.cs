namespace Portoa.Web.Rest {
	/// <summary>
	/// Parses a <c cref="CriterionFieldValue.RawValue">raw criterion value</c> as a <see cref="double"/>
	/// </summary>
	public sealed class DoubleParseStrategy : IFieldValueParseStrategy {
		public bool Parse(string rawValue, ref object value) {
			double d;
			if (!double.TryParse(rawValue, out d)) {
				return false;
			}

			value = d;
			return true;
		}
	}
}