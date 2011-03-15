namespace Portoa.Web.Rest {
	public sealed class IdentityIdParser : RestIdParserBase {
		public override string ParseId(string idValue) {
			int id;
			if (!int.TryParse(idValue, out id) || id < 1) {
				throw new RestException(string.Format("Invalid ID value: {0}", idValue));
			}

			return id.ToString();
		}
	}
}