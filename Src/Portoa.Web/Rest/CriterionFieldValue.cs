using System;
using JetBrains.Annotations;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Represents a single value associated with a field in a criterion
	/// </summary>
	public class CriterionFieldValue {
		private string rawValue;

		public CriterionFieldValue() {
			Operator = FieldValueOperator.Equal;
			Modifier = FieldValueModifier.BooleanAnd;
			Value = string.Empty;
		}

		/// <summary>
		/// Gets or sets the binary operator associated with this value; default
		/// is <see cref="FieldValueOperator.Equal"/>
		/// </summary>
		public FieldValueOperator Operator { get; set; }

		/// <summary>
		/// Gets or sets the boolean modifier associated with this value; default
		/// is <see cref="FieldValueModifier.BooleanAnd"/>
		/// </summary>
		public FieldValueModifier Modifier { get; set; }

		/// <summary>
		/// Gets or sets the unparsed value of the field; this value can be <c>null</c>
		/// </summary>
		[CanBeNull]
		public string RawValue { 
			get { return rawValue; } 
			set {
				rawValue = value;

				int i;
				double d;
				DateTime date;
				if (double.TryParse(rawValue, out d)) {
					Value = d;
				} else if (int.TryParse(rawValue, out i)) {
					Value = i;
				} else if (DateTime.TryParse(rawValue, out date)) {
					Value = date;
				} else {
					Value = (rawValue ?? string.Empty);
				}
			}
		}

		/// <summary>
		/// Gets the parsed value of the value; default value is <see cref="String.Empty"/>
		/// </summary>
		[NotNull]
		public object Value { get; private set; }

		/// <summary>
		/// Gets the parsed type of the value
		/// </summary>
		public Type ParsedType { get { return Value.GetType(); } }

		public static explicit operator string(CriterionFieldValue value) {
			return value.ToString();
		}

		public static explicit operator DateTime(CriterionFieldValue value) {
			return DateTime.Parse(value.Value.ToString());
		}

		public static explicit operator int(CriterionFieldValue value) {
			return int.Parse(value.ToString());
		}

		public static explicit operator double(CriterionFieldValue value) {
			return double.Parse(value.ToString());
		}

		public override string ToString() {
			return Value + string.Format("({0}, {1})", Operator, Modifier);
		}
	}
}