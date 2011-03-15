namespace Portoa.Web.Rest {
	public interface IRestIdParser {
		bool AllowFetchAll { get; }
		string FetchAllIdValue { get; }
		string ParseId(string idValue);
	}
}