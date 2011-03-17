using Antlr.Runtime;

namespace Portoa.Web.Rest.Parser {
	/// <summary>
	/// Parser factory that uses an <see cref="ANTLRStringStream"/>, the default token stream and 
	/// the default lexer to create a <see cref="CriterionParser"/>
	/// </summary>
	public class DefaultCriterionParserFactory : ICriterionParserFactory{
		public CriterionParser Create(string unparsedCriteria) {
			var tokens = new CommonTokenStream(new CriterionLexer(new ANTLRStringStream(unparsedCriteria)));
			return new CriterionParser(tokens) { Builder = new CriterionBuilder() };
		}
	}
}