using System.Collections.Generic;
using JetBrains.Annotations;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Represents a RESTful request
	/// </summary>
	public class RestRequest {
		public RestRequest() {
			Criteria = new CriterionSet();
			SortInfo = new List<SortGrouping>();
		}

		/// <summary>
		/// Gets or sets the sorting information for the record set
		/// </summary>
		[NotNull]
		public IList<SortGrouping> SortInfo { get; private set; }
		/// <summary>
		/// Gets or sets the criteria passed in from the URL, including the id, if given
		/// </summary>
		[NotNull]
		public CriterionSet Criteria { get; set; }
	}
}