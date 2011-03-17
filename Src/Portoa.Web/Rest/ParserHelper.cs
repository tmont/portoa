using Antlr.Runtime;
using Portoa.Web.Rest.Parser;

namespace Portoa.Web.Rest {

	public static class ParserHelper {
		public static CriterionSet Parse(string unparsedCriteria) {
			return new CriterionParser(
				new CommonTokenStream(
					new CriterionLexer(
						new ANTLRStringStream(unparsedCriteria)
					)
				)
			) { Builder = new CriteriaSetBuilder() }.getCriteria();
		}
	}
}