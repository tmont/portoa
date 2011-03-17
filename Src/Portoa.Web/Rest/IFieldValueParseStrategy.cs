namespace Portoa.Web.Rest {
	/// <summary>
	/// Exposes an interface to parse the <c cref="CriterionFieldValue.RawValue">raw criterion value</c>
	/// to another type
	/// </summary>
	public interface IFieldValueParseStrategy {
		/// <summary>
		/// Parses <paramref name="rawValue"/> and if successful, sets the <paramref name="value"/>
		/// to parsed object
		/// </summary>
		/// <param name="rawValue">The raw value that came in on the request</param>
		/// <param name="value">The parsed value</param>
		/// <returns><c>true</c> if parsed successfully, <c>false</c> if not</returns>
		bool Parse(string rawValue, ref object value);
	}
}