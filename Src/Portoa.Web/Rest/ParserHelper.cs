using Antlr.Runtime;
using Portoa.Web.Rest.Parser;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Provides methods to make parsing more convenient
	/// </summary>
	public static class ParserHelper {
		/// <summary>
		/// Parses the given string into a <see cref="CriterionSet"/>
		/// </summary>
		/// <param name="unparsedCriteria">The value to parse</param>
		public static CriterionSet Parse(string unparsedCriteria) {
			var tokens = new CommonTokenStream(new CriterionLexer(new ANTLRStringStream(unparsedCriteria)));
			return new CriterionParser(tokens) { Builder = new CriteriaSetBuilder() }.getCriteria();
		}
	}
}