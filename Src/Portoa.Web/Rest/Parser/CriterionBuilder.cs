using System.Collections.Generic;

namespace Portoa.Web.Rest.Parser {
	/// <summary>
	/// Aids in building <see cref="Criterion"/> during parsing
	/// </summary>
	internal class CriterionBuilder {
		private readonly CriterionSet criteria = new CriterionSet();

		public CriterionBuilder() {
			Criterion = new Criterion();
			Value = new CriterionFieldValue();
		}

		/// <summary>
		/// Gets the criterion currently being parsed
		/// </summary>
		public Criterion Criterion { get; private set; }
		/// <summary>
		/// Gets the value currently being parsed
		/// </summary>
		public CriterionFieldValue Value { get; private set; }
		/// <summary>
		/// Gets the sets the last parsed identifier
		/// </summary>
		public string Ident { get; set; }

		/// <summary>
		/// Adds the current <see cref="Value"/> to the criterion and resets
		/// the value
		/// </summary>
		public void CommitValue() {
			Criterion.Add(Value);
			Value = new CriterionFieldValue();
		}

		/// <summary>
		/// Adds the current <see cref="Criterion"/> to the set and resets
		/// the criterion
		/// </summary>
		public void CommitCriterion() {
			criteria.Add(Criterion);
			Criterion = new Criterion();
		}

		/// <summary>
		/// Get the collection of parsed criterion
		/// </summary>
		public IEnumerable<Criterion> Criteria { get { return criteria; } }
	}
}