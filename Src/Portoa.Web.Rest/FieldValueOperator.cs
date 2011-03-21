namespace Portoa.Web.Rest {
	/// <summary>
	/// Indicates the binary relationship between the actual value and the
	/// <see cref="CriterionFieldValue.Value">comparison value</see>
	/// </summary>
	public enum FieldValueOperator {
		/// <summary>
		/// The values should be eqaul
		/// </summary>
		Equal,
		/// <summary>
		/// The values should not be equal
		/// </summary>
		NotEqual,
		/// <summary>
		/// The actual value should be less than the comparison value
		/// </summary>
		LessThan,
		/// <summary>
		/// The actual value should be less than or equal to the comparison value
		/// </summary>
		LessThanOrEqualTo,
		/// <summary>
		/// The actual value should be greater than the comparison value
		/// </summary>
		GreaterThan,
		/// <summary>
		/// The actual value should be greater than or equal to the comparison value
		/// </summary>
		GreaterThanOrEqualTo,
		/// <summary>
		/// The actual value should match the wildcard value given in the comparison value
		/// </summary>
		Like
	}
}