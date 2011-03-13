using System.Text;
using System.Web.Mvc;
using Portoa.Json;

namespace Portoa.Web.Results {
	/// <summary>
	/// <see cref="ActionResult"/> that allows you to inject an implementation of <see cref="IJsonSerializer"/>
	/// that will perform the JSON serialization
	/// </summary>
	public class InjectableJsonResult : ActionResult {
		private readonly IJsonSerializer jsonSerializer;

		/// <param name="jsonSerializer">The JSON serializer to use; defaults to <see cref="JsonNetSerializer"/></param>
		public InjectableJsonResult(IJsonSerializer jsonSerializer = null) {
			this.jsonSerializer = jsonSerializer ?? new JsonNetSerializer();
		}

		/// <summary>
		/// Gets or sets the encoding of the response (default is <see cref="Encoding.UTF8"/>)
		/// </summary>
		public Encoding ContentEncoding { get; set; }

		/// <summary>
		/// Gets or sets the content type (default is <c>application/json</c>)
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// Gets or sets the data to serialize to <c>JSON</c>
		/// </summary>
		public object Data { get; set; }

		public override void ExecuteResult(ControllerContext context) {
			new ContentResult { 
				ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json",
				ContentEncoding = ContentEncoding ?? Encoding.UTF8,
				Content = jsonSerializer.Serialize(Data)
			}.ExecuteResult(context);
		}
	}
}