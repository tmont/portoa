namespace Portoa.Web.Rest {
	/// <summary>
	/// Provides an interface for parsing identifier values that come in from
	/// a RESTful request
	/// </summary>
	public interface IRestIdParser {
		/// <summary>
		/// Gets whether or not this parser allows fetching all records instead
		/// of only fetching records by ID
		/// </summary>
		bool AllowFetchAll { get; }

		/// <summary>
		/// Gets the value that should be sent in place of an actual ID when the
		/// user wants to fetch all records
		/// </summary>
		string FetchAllIdValue { get; }

		/// <summary>
		/// Verifies the given value as an identifier, possibly transforming it
		/// in the process
		/// </summary>
		/// <param name="idValue">The value of the id given in the request</param>
		/// <exception cref="InvalidIdException"/>
		string ParseId(string idValue);

		/// <summary>
		/// Gets the key of the identifier value in the request
		/// </summary>
		string IdKey { get; }
	}
}