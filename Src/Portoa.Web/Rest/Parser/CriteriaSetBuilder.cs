using System.Collections.Generic;

namespace Portoa.Web.Rest.Parser {
	public class CriteriaSetBuilder {
		private readonly CriterionSet criteria = new CriterionSet();

		public CriteriaSetBuilder() {
			Current = new Criterion();
			Value = new CriterionFieldValue();
		}

		public Criterion Current { get; private set; }
		public CriterionFieldValue Value { get; private set; }
		public string Ident { get; set; }

		public void CommitValue() {
			Current.Add(Value);
			Value = new CriterionFieldValue();
		}

		public void CommitCriterion() {
			criteria.Add(Current);
			Current = new Criterion();
		}

		public IEnumerable<Criterion> Criteria { get { return criteria; } }
	}
}