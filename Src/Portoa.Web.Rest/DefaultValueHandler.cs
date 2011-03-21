using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Portoa.Web.Rest {
	/// <summary>
	/// <see cref="IValueHandler"/> used by default by <see cref="RestServiceBase"/>. It uses
	/// reflection to find the property matching <see cref="Criterion.FieldName"/> and composes an
	/// expression representing the values.
	/// </summary>
	public class DefaultValueHandler : IValueHandler {
		private static readonly MethodInfo likeMethod = typeof(RegexExtensions).GetMethod("Like", new[] { typeof(string), typeof(string) });

		/// <summary>
		/// The regex format used for <see cref="FieldValueOperator.Like"/> values. Value is <c>{0}.*</c>.
		/// </summary>
		public const string LikeRegexFormat = "{0}.*";

		public Expression<Func<T, bool>> CreateExpression<T>(Criterion criterion) {
			if (!criterion.Values.Any()) {
				throw new InvalidOperationException("Cannot handle criterion without any values");
			}

			var parameter = ExpressionHelper.CreateResourceParameter<T>();
			return criterion
				.Values
				.Aggregate<CriterionFieldValue, Expression<Func<T, bool>>>(null, (expression, value) =>
					ExpressionHelper.Compose(expression, BuildExpression<T>(criterion.FieldName, value, parameter), value.Modifier)
				);
		}

		private static Expression<Func<T, bool>> BuildExpression<T>(string fieldName, CriterionFieldValue value, ParameterExpression parameter) {
			var property = ExpressionHelper.GetProperty(typeof(T), fieldName);
			if (property == null) {
				throw new UnknownCriterionException(string.Format("Unknown criterion \"{0}\"", fieldName));
			}

			var propertyExpression = Expression.Property(parameter, property);
			return GetBody(propertyExpression, value).ToLambda<T, bool>(parameter);
		}

		private static Expression GetBody(Expression property, CriterionFieldValue value) {
			if (value.Operator != FieldValueOperator.Like) {
				return Expression.MakeBinary(
					ExpressionHelper.GetExpressionType(value.Operator),
					property,
					Expression.Constant(value.Value, value.ParsedType)
				);
			}

			return Expression.Call(
				likeMethod,
				property,
				Expression.Constant(string.Format(LikeRegexFormat, Regex.Escape(value.RawValue ?? string.Empty)))
			);
		}
	}
}