using System.Collections.Generic;

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
		/// Gets the sorting information for the record set
		/// </summary>
		public IList<SortGrouping> SortInfo { get; private set; }

		/// <summary>
		/// Gets or sets the maximum number of records to return (zero means no limit)
		/// </summary>
		public int Limit { get; set; }

		/// <summary>
		/// Gets or sets the number of records to skip
		/// </summary>
		public int Offset { get; set; }

		/// <summary>
		/// Gets or sets the name of the requested resource
		/// </summary>
		public string ResourceName { get; set; }

		/// <summary>
		/// Gets the criteria passed in from the URL, including the id, if given
		/// </summary>
		public CriterionSet Criteria { get; private set; }
	}
}