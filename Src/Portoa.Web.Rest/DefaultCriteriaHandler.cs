using System;
using Portoa.Web.Rest.Parser;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Parses the request into a group of criterion
	/// </summary>
	public sealed class DefaultCriteriaHandler : ICriterionHandler {
		private readonly ICriterionParserFactory parserFactory;

		public DefaultCriteriaHandler(ICriterionParserFactory parserFactory) {
			this.parserFactory = parserFactory;
		}

		/// <summary>
		/// This value is ignored
		/// </summary>
		string ICriterionHandler.Key { get; set; }

		public void Execute(string value, RestRequest model) {
			//var valueResult = bindingContext.ValueProvider.GetValue(RestRequestModelBinder.CriteriaValueKey);
			//if (valueResult == null) {
			//    return;
			//}

			//try {
			//    model.Criteria.AddRange(parserFactory.Create(valueResult.AttemptedValue).getCriteria());
			//} catch (Exception e) {
			//    controllerContext.AddModelError(RestRequestModelBinder.CriteriaValueKey, string.Format("An error occurred while parsing criteria: {0}", e.Message));
			//}
		}
	}
}