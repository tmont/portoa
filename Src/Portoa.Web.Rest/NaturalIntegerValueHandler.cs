using System;
using System.Linq.Expressions;

namespace Portoa.Web.Rest {
	public abstract class NaturalIntegerValueHandler : ICriterionHandler {
		public string Key { get; set; }
		public void Execute(string value, RestRequest model) {
			int i;
			if (!int.TryParse(value, out i) || i < 0) {
				throw new CriterionHandlingException("Expected value to be an integer greater than or equal to zero");
			}

			Expression.Lambda<Action<RestRequest>>(
				Expression.Assign(PropertyToSet.Body, Expression.Constant(i)),
				PropertyToSet.Parameters
			).Compile()(model);
		}

		protected abstract Expression<Func<RestRequest, int>> PropertyToSet { get; }
	}
}