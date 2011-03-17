namespace Portoa.Web.Rest {
	/// <summary>
	/// Indicates how to compose field values through boolean logic
	/// </summary>
	public enum FieldValueModifier {
		/// <summary>
		/// Equivalent of <c>&&</c>
		/// </summary>
		BooleanAnd,
		/// <summary>
		/// Equivalent of <c>||</c>
		/// </summary>
		BooleanOr
	}
}