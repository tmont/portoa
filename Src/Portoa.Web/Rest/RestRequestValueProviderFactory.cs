using System.Web.Mvc;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Factory for creating value providers for RESTful requests
	/// </summary>
	/// <seealso cref="RestRequestValueProvider"/>
	public class RestRequestValueProviderFactory : ValueProviderFactory {
		public override IValueProvider GetValueProvider(ControllerContext controllerContext) {
			return new RestRequestValueProvider(controllerContext);
		}
	}
}