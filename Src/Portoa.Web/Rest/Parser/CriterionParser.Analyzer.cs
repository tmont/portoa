using Antlr.Runtime;

namespace Portoa.Web.Rest.Parser {
	public partial class CriterionParser {
		internal CriterionBuilder Builder { get; set; }

		partial void SetIdent(string identValue) {
			Builder.Ident = identValue;
		}

		partial void SetCriteria(ref CriterionSet set) {
			set = new CriterionSet(Builder.Criteria);
		}

		partial void Leave_criterion() {
			Builder.CommitCriterion();
		}

		partial void Leave_fieldName() {
			Builder.Criterion.FieldName = Builder.Ident;
		}

		partial void Leave_fieldValue() {
			Builder.Value.RawValue = Builder.Ident;
			Builder.CommitValue();
		}

		private IToken LastToken { get { return ((BufferedTokenStream)input).LastToken; } }

		partial void Leave_booleanModifier() {
			switch (LastToken.Type) {
				case AND:
					Builder.Value.Modifier = FieldValueModifier.BooleanAnd;
					break;
				case OR:
					Builder.Value.Modifier = FieldValueModifier.BooleanOr;
					break;
			}
		}

		partial void Leave_fieldValueModifier() {
			switch (LastToken.Type) {
				case NOT:
					Builder.Value.Operator = FieldValueOperator.NotEqual;
					break;
				case LESS_THAN:
					Builder.Value.Operator = FieldValueOperator.LessThan;
					break;
				case LESS_THAN_OR_EQUAL_TO:
					Builder.Value.Operator = FieldValueOperator.LessThanOrEqualTo;
					break;
				case GREATER_THAN:
					Builder.Value.Operator = FieldValueOperator.GreaterThan;
					break;
				case GREATER_THAN_OR_EQUAL_TO:
					Builder.Value.Operator = FieldValueOperator.GreaterThanOrEqualTo;
					break;
				case LIKE:
					Builder.Value.Operator = FieldValueOperator.Like;
					break;
			}
		}
	}
}