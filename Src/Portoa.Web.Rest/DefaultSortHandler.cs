using System.Linq;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Parses the sort info for the request
	/// </summary>
	public sealed class DefaultSortHandler : ICriterionHandler {
		public void Execute(string value, RestRequest model) {
			if (value == null) {
				return;
			}

			foreach (var splitSortValue in value.Split(',').Select(sortValue => sortValue.Split('|'))) {
				var sortOrder = SortOrder.Ascending;
				if (splitSortValue.Length > 1) {
					switch (splitSortValue[1].ToUpperInvariant()) {
						case "DESCENDING":
						case "DESC":
							sortOrder = SortOrder.Descending;
							break;
						case "ASCENDING":
						case "ASC":
							sortOrder = SortOrder.Ascending;
							break;
						default:
							throw new CriterionHandlingException(string.Format("The sort order \"{0}\" is invalid for field \"{1}\"", splitSortValue[1], splitSortValue[0]));
					}
				}

				model.SortInfo.Add(new SortGrouping { Field = splitSortValue[0], Order = sortOrder });
			}
		}

		public string Key { get; set; }
	}
}