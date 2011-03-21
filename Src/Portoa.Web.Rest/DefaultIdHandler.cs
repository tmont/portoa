namespace Portoa.Web.Rest {
	public sealed class DefaultIdHandler : ICriterionHandler {
		private readonly IRestIdParser idParser;

		public DefaultIdHandler(IRestIdParser idParser) {
			this.idParser = idParser;
		}

		public string Key { get; set; }

		public void Execute(string value, RestRequest model) {
			if (value == idParser.FetchAllIdValue && idParser.AllowFetchAll) {
				return;
			}

			try {
				model.Criteria.Add(idParser.IdKey, idParser.ParseId(value));
			} catch (InvalidIdException e) {
				throw new CriterionHandlingException(e.Message, e);
			}
		}
	}
}