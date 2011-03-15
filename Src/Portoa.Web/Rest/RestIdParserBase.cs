namespace Portoa.Web.Rest {
	public abstract class RestIdParserBase : IRestIdParser {
		protected RestIdParserBase() {
			AllowFetchAll = true;
			FetchAllIdValue = "all";
		}

		public bool AllowFetchAll { get; set; }
		public string FetchAllIdValue { get; set; }

		public abstract string ParseId(string idValue);
	}
}