using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Portoa.Web.Rest {
	/// <summary>
	/// <see cref="ICriterionHandler"/> used by default by <see cref="RestServiceBase"/>. It uses
	/// reflection to find the property matching <see cref="Criterion.FieldName"/> and composes an
	/// expression representing the values.
	/// </summary>
	public class DefaultCriterionHandler : ICriterionHandler {
		private readonly IDictionary<Type, IDictionary<string, PropertyInfo>> propertyCache = new Dictionary<Type, IDictionary<string, PropertyInfo>>();

		/// <summary>
		/// The regex format used for <see cref="FieldValueOperator.Like"/> values. Values is <c>{0}.*</c>.
		/// </summary>
		public const string LikeRegexFormat = "{0}.*";

		public Expression<Func<T, bool>> HandleCriterion<T>(Criterion criterion) {
			if (!criterion.Values.Any()) {
				throw new InvalidOperationException("Cannot handle criterion without any values");
			}

			var parameter = Expression.Parameter(typeof(T), "resource");
			return criterion
				.Values
				.Aggregate<CriterionFieldValue, Expression<Func<T, bool>>>(null, (expression, value) => 
					ExpressionHelper.Compose(expression, BuildExpression<T>(criterion.FieldName, value, parameter), value.Modifier)
				);
		}

		private Expression<Func<T, bool>> BuildExpression<T>(string fieldName, CriterionFieldValue value, ParameterExpression parameter) {
			var property = GetProperty(typeof(T), fieldName);
			if (property == null) {
				throw new UnknownCriterionException(string.Format("Unknown criterion \"{0}\"", fieldName));
			}

			Expression body;
			if (value.Operator != FieldValueOperator.Like) {
				body = Expression.MakeBinary(
					GetExpressionType(value.Operator),
					Expression.Property(parameter, property),
					Expression.Constant(value.Value, value.ParsedType)
				);
			} else {
				body = Expression.Call(
					typeof(RegexExtensions).GetMethod("Like", new[] { typeof(string), typeof(string) }),
					Expression.Property(parameter, property),
					Expression.Constant(string.Format(LikeRegexFormat, value.RawValue ?? string.Empty))
				);
			}

			return Expression.Lambda<Func<T, bool>>(body, parameter);
		}

		private PropertyInfo GetProperty(Type type, string fieldName) {
			if (!propertyCache.ContainsKey(type)) {
				propertyCache[type] = new Dictionary<string, PropertyInfo>();
			}

			if (!propertyCache[type].ContainsKey(fieldName)) {
				propertyCache[type][fieldName] = type.GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
			}

			return propertyCache[type][fieldName];
		}

		private static ExpressionType GetExpressionType(FieldValueOperator op) {
			switch (op) {
				case FieldValueOperator.NotEqual:
					return ExpressionType.NotEqual;
				case FieldValueOperator.LessThan:
					return ExpressionType.LessThan;
				case FieldValueOperator.LessThanOrEqualTo:
					return ExpressionType.LessThanOrEqual;
				case FieldValueOperator.GreaterThan:
					return ExpressionType.GreaterThan;
				case FieldValueOperator.GreaterThanOrEqualTo:
					return ExpressionType.GreaterThanOrEqual;
				default:
					return ExpressionType.Equal;
			}
		}
	}
}