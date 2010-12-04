using System.Web.Mvc;

namespace Portoa.Web.Filters {
	/// <summary>
	/// Instructs ASP.NET MVC to not validate input on the request
	/// </summary>
	public sealed class DoNotValidateInput : ValidateInputAttribute {
		public DoNotValidateInput() : base(false) { }

		public new bool EnableValidation { get { return false; }  }
	}
}