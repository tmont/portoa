using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Represents a single value associated with a field in a criterion
	/// </summary>
	public class CriterionFieldValue {
		private readonly IEnumerable<IFieldValueParseStrategy> parseStrategies;
		private string rawValue;

		public CriterionFieldValue(IEnumerable<IFieldValueParseStrategy> parseStrategies = null) {
			this.parseStrategies = (parseStrategies ?? new IFieldValueParseStrategy[] {
				new IntegerParseStrategy(),
				new DoubleParseStrategy(),
				new DateTimeParseStrategy()
			}).Concat(new[] { new DefaultParseStrategy() });

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
				object actualValue = null;
				if (parseStrategies.Any(parseStrategy => parseStrategy.Parse(RawValue, ref actualValue))) {
					Value = actualValue;
				}

				Debug.Assert(Value != null); //should be enforced by DefaultParseStrategy
			}
		}

		/// <summary>
		/// Gets the parsed value of the <see cref="string"/> that came in on the request; default 
		/// value is <see cref="string.Empty"/>
		/// </summary>
		[NotNull]
		public object Value { get; private set; }

		/// <summary>
		/// Gets the type of the <c cref="Value">parsed value</c>
		/// </summary>
		public Type ParsedType { get { return Value.GetType(); } }

		public override string ToString() {
			return RawValue + string.Format("({0}, {1}, {2})", Operator, Modifier, ParsedType);
		}

		private class DefaultParseStrategy : IFieldValueParseStrategy {
			public bool Parse(string rawValue, ref object value) {
				value = rawValue;
				return true;
			}
		}
	}

	public static class CriterionFieldValueExtensions {
		/// <summary>
		/// Casts the value to type <typeparamref name="T"/>. <typeparamref name="T"/> should
		/// probably match <see cref="CriterionFieldValue.ParsedType"/>
		/// </summary>
		/// <typeparam name="T">The type to cast the value to</typeparam>
		public static T As<T>(this CriterionFieldValue fieldValue) {
			return (T)fieldValue.Value;
		}
	}

}