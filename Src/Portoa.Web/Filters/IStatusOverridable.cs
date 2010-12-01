using System.Net;

namespace Portoa.Web.Filters {
	public interface IStatusOverridable {
		HttpStatusCode StatusCode { get; set; }
	}
}