using System.Web.Mvc;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Provides an interface for executing an action (such as parsing a value from the request) that
	/// populates the <c cref="RestRequest">model</c> in some way.
	/// </summary>
	public interface ICriterionHandler {
		/// <summary>
		/// Gets or sets the key in the request for this criterion
		/// </summary>
		string Key { get; set; }

		/// <summary>
		/// Executes the handler against the given value
		/// </summary>
		/// <param name="value">The value in the request keyed by <see cref="Key"/></param>
		/// <param name="model">The model that is being built</param>
		void Execute(string value, RestRequest model);
	}
}