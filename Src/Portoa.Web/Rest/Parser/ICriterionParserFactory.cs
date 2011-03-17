namespace Portoa.Web.Rest.Parser {
	/// <summary>
	/// Provides a mechanism for creating <see cref="CriterionParser"/> instances
	/// </summary>
	public interface ICriterionParserFactory {
		/// <summary>
		/// Creates a parser for the given criteria string
		/// </summary>
		/// <param name="unparsedCriteria">The criteria string that came in on the request</param>
		CriterionParser Create(string unparsedCriteria);
	}
}