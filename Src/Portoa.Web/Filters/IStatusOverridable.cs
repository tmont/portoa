using System.Net;

namespace Portoa.Web.Filters {
	/// <summary>
	/// Enables action results to override the HTTP status code
	/// </summary>
	public interface IStatusOverridable {
		/// <summary>
		/// Gets or sets the new HTTP status code for the result
		/// </summary>
		HttpStatusCode StatusCode { get; set; }
	}
}