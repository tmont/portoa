using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Portoa.Web.Rest {

	public interface ICriterionHandler {
		Expression<Func<T, bool>> HandleCriterion<T>(Criterion criterion);
	}

	public static class RegexExtensions {
		public static bool Like(this string value, string pattern) {
			return Regex.IsMatch(value, pattern);
		}
	}

	public static class ExpressionHelper {
		public static Expression<Func<T, bool>> Compose<T>([CanBeNull]Expression<Func<T, bool>> left, [NotNull]Expression<Func<T, bool>> right, FieldValueModifier booleanModifier) {
			if (left == null) {
				return right;
			}

			Debug.Assert(left.Parameters == right.Parameters);

			switch (booleanModifier) {
				case FieldValueModifier.BooleanAnd:
					return Expression.Lambda<Func<T, bool>>(Expression.MakeBinary(ExpressionType.And, left, right), left.Parameters);
				case FieldValueModifier.BooleanOr:
					return Expression.Lambda<Func<T, bool>>(Expression.MakeBinary(ExpressionType.Or, left, right), left.Parameters);
				default:
					throw new ArgumentOutOfRangeException("booleanModifier");
			}
		}
	}

	

	public class DefaultCriterionHandler : ICriterionHandler {
		private readonly IDictionary<Type, IDictionary<string, PropertyInfo>> propertyCache = new Dictionary<Type, IDictionary<string, PropertyInfo>>();

		private PropertyInfo GetProperty(Type type, string fieldName) {
			if (!propertyCache.ContainsKey(type)) {
				propertyCache[type] = new Dictionary<string, PropertyInfo>();
			}

			if (!propertyCache[type].ContainsKey(fieldName)) {
				propertyCache[type][fieldName] = type.GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
			}

			return propertyCache[type][fieldName];
		}

		public Expression<Func<T, bool>> HandleCriterion<T>(Criterion criterion) {
			return criterion
				.Values
				.Aggregate<CriterionFieldValue, Expression<Func<T, bool>>>(null, (expression, value) => 
					ExpressionHelper.Compose(expression, BuildExpression<T>(criterion.FieldName, value), value.Modifier)
				);
		}

		private Expression<Func<T, bool>> BuildExpression<T>(string fieldName, CriterionFieldValue value) {
			var type = typeof(T);
			var property = GetProperty(type, fieldName);
			if (property == null) {
				throw new UnknownCriterionException(string.Format("Unknown criterion \"{0}\"", fieldName));
			}

			var parameter = Expression.Parameter(type, "resource");

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
					Expression.Constant(string.Format("{0}*", value.RawValue ?? string.Empty))
				);
			}

			return Expression.Lambda<Func<T, bool>>(body, parameter);
		}

		private static ExpressionType GetExpressionType(FieldValueOperator op) {
			switch (op) {
				case FieldValueOperator.Equal:
					return ExpressionType.Equal;
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
					throw new ArgumentOutOfRangeException("op");
			}
		}
	}
}