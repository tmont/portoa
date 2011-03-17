using System;

namespace Portoa.Web.Rest {
	public class CriterionFieldValue {
		private object value;

		public CriterionFieldValue() {
			Operator = FieldValueOperator.Equal;
			Modifier = FieldValueModifier.BooleanAnd;
			ParsedType = typeof(string);
		}

		public FieldValueOperator Operator { get; set; }
		public FieldValueModifier Modifier { get; set; }

		public object Value { 
			get { return value; } 
			set { 
				this.value = value;

				int i;
				double d;
				DateTime date;
				if (double.TryParse(value.ToString(), out d)) {
					ParsedType = typeof(double);
				} else if (int.TryParse(value.ToString(), out i)) {
					ParsedType = typeof(int);
				} else if (DateTime.TryParse(value.ToString(), out date)) {
					ParsedType = typeof(DateTime);
				} else {
					ParsedType = typeof(string);
				}
			}
		}

		public Type ParsedType { get; private set; }

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