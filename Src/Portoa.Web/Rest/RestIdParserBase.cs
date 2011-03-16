namespace Portoa.Web.Rest {
	/// <summary>
	/// Provides a convenient base class for <see cref="IRestIdParser"/> implementations.
	/// <see cref="AllowFetchAll"/> defaults to <c>true</c>, and <see cref="FetchAllIdValue"/>
	/// defaults to <c>all</c>.
	/// </summary>
	public abstract class RestIdParserBase : IRestIdParser {
		protected RestIdParserBase() {
			AllowFetchAll = true;
			FetchAllIdValue = "all";
		}

		public bool AllowFetchAll { get; set; }
		public string FetchAllIdValue { get; set; }

		public string ParseId(string idValue) {
			var id = idValue;
			if (!TryParse(idValue, ref id)) {
				throw new InvalidIdException(idValue);
			}

			return id;
		}

		/// <summary>
		/// Attempts to parse the given value
		/// </summary>
		/// <param name="idValue">The value of the identifier as given in the request</param>
		/// <param name="id">The transformed identifier; the default value is <paramref name="idValue"/></param>
		/// <returns><c>true</c> if the parse was successful, <c>false</c> if not</returns>
		protected abstract bool TryParse(string idValue, ref string id);
	}
}