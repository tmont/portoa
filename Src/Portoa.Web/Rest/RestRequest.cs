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
		/// Gets or sets whether or not to fetch all records
		/// </summary>
		public bool FetchAll { get; set; }
		/// <summary>
		/// Gets or sets the specific ID of the entity to fetch
		/// </summary>
		[CanBeNull]
		public string Id { get; set; }
		/// <summary>
		/// Gets or sets the sorting information for the record set
		/// </summary>
		[NotNull]
		public IList<SortGrouping> SortInfo { get; private set; }
		/// <summary>
		/// Gets or sets the criteria passed in from the URL
		/// </summary>
		[NotNull]
		public CriterionSet Criteria { get; set; }
	}
}