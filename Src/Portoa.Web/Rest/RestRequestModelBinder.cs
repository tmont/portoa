using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Portoa.Web.Util;

namespace Portoa.Web.Rest {
	public class RestRequestModelBinder : IModelBinder {
		private const string SortValueKey = "sort";
		private const string IdValueKey = "id";
		private readonly IRestIdParser idParser;

		public RestRequestModelBinder(IRestIdParser idParser) {
			this.idParser = idParser;
		}

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			var model = new RestRequest();

			ParseId(controllerContext, bindingContext, model);
			if (model.FetchAll) {
				ParseSort(controllerContext, bindingContext, model);
				ParseCriteria(controllerContext, bindingContext, model);
			}

			return model;
		}

		private static void ParseCriteria(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model) {
			var valueResult = bindingContext.ValueProvider.GetValue("criteria");
			if (valueResult == null) {
				return;
			}

			// /api/{resource}/{id}/{*criteria}[?sort[]=...]
			//e.g. /api/user/all/name/foo|bar/created/>2008-01-02,<2008-02-01?sort[]=name&sort[]=created|desc
			//        -> all users with name=foo or bar and created between 2008-01-01 and 2008-02-01, sorted by name ascending and then by created descending

			//field modifiers
			//| = boolean OR
			//, = boolean AND
			//> = greater than
			//< = less than
			//>= = greater than or equal to
			//<= = less than or equal to
			//~ = wildcard match

			//acceptable query string variables
			//sort[]=field|order -> field is the name of the field, order is asc/ascending, desc/descending
			//limit -> maximum number of records to return
			//offset -> offset from beginning of recordset to begin taking records

			var criteria = valueResult.AttemptedValue.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			for (var i = 0; i < criteria.Length; i++) {
				if (i < criteria.Length - 1) {
					model.Criteria[criteria[i]] = ParseCommaSeparatedCriterion(criteria[i + 1]);
					i++;
				} else {
					controllerContext.AddModelError("criteria", string.Format("Unable to parse criteria for key \"{0}\"", criteria[i]));
				}
			}
		}

		private static IEnumerable<object> ParseCommaSeparatedCriterion(string csv) {
			foreach (var value in csv.Split(',')) {
				int result;
				if (int.TryParse(value, out result)) {
					yield return result;
				} else {
					yield return value;
				}
			}
		}

		private static void ParseSort(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model) {
			var valueResult = bindingContext.ValueProvider.GetValue(SortValueKey);
			if (valueResult == null) {
				return;
			}

			var sortValues = (string[])valueResult.ConvertTo(typeof(string[]));

			if (sortValues == null || sortValues.Length == 0) {
				return;
			}

			var sortGroupings = new List<SortGrouping>();
			foreach (var splitSortValue in sortValues.Select(sortValue => sortValue.Split('|'))) {
				//field|order e.g. created|descending
				//asc and desc are shorthands for ascending/descending

				var grouping = new SortGrouping { Field = splitSortValue[0] };
				var sortOrder = SortOrder.Ascending;
				if (splitSortValue.Length > 1) {
					switch (splitSortValue[1]) {
						case "descending":
						case "desc":
							sortOrder = SortOrder.Descending;
							break;
						case "ascending":
						case "asc":
							sortOrder = SortOrder.Ascending;
							break;
						default:
							controllerContext.AddModelError(SortValueKey, string.Format("The sort order \"{0}\" is invalid", sortValues[1]));
							break;
					}
				}

				grouping.Order = sortOrder;
			}

			model.SortInfo = sortGroupings;
		}

		private void ParseId(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model) {
			var valueResult = bindingContext.ValueProvider.GetValue(IdValueKey);
			var idValue = valueResult != null ? valueResult.AttemptedValue : string.Empty;

			if (idValue == idParser.FetchAllIdValue && idParser.AllowFetchAll) {
				model.FetchAll = true;
				return;
			}

			try {
				model.Id = idParser.ParseId(idValue);
			} catch (RestException e) {
				controllerContext.AddModelError(IdValueKey, e.Message);
			}
		}
	}
}