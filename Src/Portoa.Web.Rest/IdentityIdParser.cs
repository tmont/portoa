namespace Portoa.Web.Rest {
	/// <summary>
	/// <see cref="IRestIdParser"/> implementation that expects the id to be
	/// an integer greater than zero
	/// </summary>
	public sealed class IdentityIdParser : RestIdParserBase {
		protected override bool TryParse(string idValue, ref string id) {
			int integralId;
			if (!int.TryParse(idValue, out integralId) || integralId < 1) {
				return false;
			}

			id = integralId.ToString();
			return true;
		}
	}
}