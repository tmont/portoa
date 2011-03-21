using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Provides static helper methods for building expressions
	/// </summary>
	public static class ExpressionHelper {
		private static readonly IDictionary<Type, IDictionary<string, PropertyInfo>> propertyCache = new Dictionary<Type, IDictionary<string, PropertyInfo>>();

		/// <summary>
		/// Composes two expressions into a binary expression using the given modifier. The parameters
		/// from the <paramref name="left"/> expression will be used, and the parameters from the
		/// <paramref name="right"/> expression will be assumed to be the same as the left's.
		/// </summary>
		/// <param name="left">The left hand side of the binary expression</param>
		/// <param name="right">The right hand side of the binary expression</param>
		/// <param name="booleanModifier">The boolean operator</param>
		public static Expression<Func<T, bool>> Compose<T>([CanBeNull]Expression<Func<T, bool>> left, [NotNull]Expression<Func<T, bool>> right, FieldValueModifier booleanModifier) {
			if (left == null) {
				return right;
			}

			switch (booleanModifier) {
				case FieldValueModifier.BooleanOr:
					return Expression.Lambda<Func<T, bool>>(Expression.MakeBinary(ExpressionType.Or, left.Body, right.Body), left.Parameters);
				default:
					return Expression.Lambda<Func<T, bool>>(Expression.MakeBinary(ExpressionType.AndAlso, left.Body, right.Body), left.Parameters);
			}
		}

		/// <summary>
		/// Gets a <see cref="PropertyInfo"/> for the given <paramref name="type"/> and <paramref name="propertyName"/>
		/// or <c>null</c> is no such property exists
		/// </summary>
		/// <param name="type">The type of the object that contains the property</param>
		/// <param name="propertyName">The name of the property</param>
		[CanBeNull]
		public static PropertyInfo GetProperty(Type type, string propertyName) {
			if (!propertyCache.ContainsKey(type)) {
				propertyCache[type] = new Dictionary<string, PropertyInfo>();
			}

			if (!propertyCache[type].ContainsKey(propertyName)) {
				propertyCache[type][propertyName] = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
			}

			return propertyCache[type][propertyName];
		}

		/// <summary>
		/// Gets a <see cref="PropertyInfo"/> for the type given by <typeparamref name="T"/> and <paramref name="propertyName"/>
		/// or <c>null</c> is no such property exists
		/// </summary>
		/// <typeparam name="T">The type of the object that contains the property</typeparam>
		/// <param name="propertyName">The name of the property</param>
		[CanBeNull]
		public static PropertyInfo GetProperty<T>(string propertyName) {
			return GetProperty(typeof(T), propertyName);
		}

		/// <summary>
		/// Creates a parameter for use in lambda expressions
		/// </summary>
		/// <typeparam name="T">The resource type</typeparam>
		public static ParameterExpression CreateResourceParameter<T>() {
			return Expression.Parameter(typeof(T), "resource");
		}

		/// <summary>
		/// Creates a lambda expression with the given parameters
		/// </summary>
		/// <typeparam name="T">The resource type</typeparam>
		/// <typeparam name="TReturn">The return value</typeparam>
		/// <param name="body">The body of the lambda expression</param>
		/// <param name="parameters">The parameters for the lambda expression</param>
		public static Expression<Func<T, TReturn>> ToLambda<T, TReturn>(this Expression body, params ParameterExpression[] parameters) {
			return Expression.Lambda<Func<T, TReturn>>(body, parameters);
		}

		/// <summary>
		/// Translates between <see cref="FieldValueOperator"/> and <see cref="ExpressionType"/>.
		/// The default value is <see cref="ExpressionType.Equal"/>.
		/// </summary>
		/// <param name="op">The operator to translate to an <see cref="ExpressionType"/></param>
		public static ExpressionType GetExpressionType(FieldValueOperator op) {
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