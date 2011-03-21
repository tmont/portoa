using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Portoa.Persistence;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Base class for fetching records in response to a <c cref="RestRequest">RESTful request</c>
	/// </summary>
	public abstract class RestServiceBase {
		/// <summary>
		/// Gets an expression for a property on <typeparamref name="T"/> that maps to a
		/// <paramref name="fieldName"/> given in the criteria, or <c>null</c> if there is
		/// no mapping. By default, it uses reflection to automatically build the expression.
		/// </summary>
		/// <example><code>GetFieldName&lt;Foo&gt;("bar")</code> might return <code>foo =&gt; foo.Bar</code></example>
		/// <typeparam name="T">The type on which to map the field name to a property</typeparam>
		/// <param name="fieldName">The name of the field given in the criterion</param>
		[CanBeNull]
		protected virtual Expression<Func<T, object>> GetFieldMapping<T>(string fieldName) {
			var property = ExpressionHelper.GetProperty(typeof(T), fieldName);
			if (property == null) {
				return null;
			}

			var parameter = ExpressionHelper.CreateResourceParameter<T>();
			return Expression
				.Convert(Expression.Property(parameter, property), typeof(object))
				.ToLambda<T, object>(parameter);
		}

		/// <summary>
		/// Fetches records, filtering by the given <c cref="RestRequest.Criteria">criteria</c>
		/// </summary>
		/// <typeparam name="T">The type of entity to fetch</typeparam>
		/// <typeparam name="TDto">The DTO representation of the entity, suitable for serialization</typeparam>
		/// <param name="request">The object representing the RESTful request</param>
		/// <param name="records">Initial record set to filter</param>
		/// <param name="valueHandlers">Specific criterion value handlers to guide the filtering process; if not given, defaults to <see cref="DefaultValueHandler"/></param>
		/// <returns>The filtered record set</returns>
		protected IEnumerable<TDto> GetRecords<T, TDto>(RestRequest request, IQueryable<T> records, IDictionary<string, IValueHandler> valueHandlers = null) where TDto : new() {
			return records
				.Filter(request.Criteria, valueHandlers)
				.Sort(GetFieldMapping<T>, request.SortInfo)
				.Offset(request.Offset)
				.Limit(request.Limit)
				.ToArray()
				.Select(entity => entity.ToDto<TDto>());
		}
	}

	/// <summary>
	/// Provides static helper methods for REST services
	/// </summary>
	internal static class RestServiceHelper {
		/// <summary>
		/// Limits the record set to the specified <paramref name="limit"/>
		/// </summary>
		/// <typeparam name="T">The resource type</typeparam>
		/// <param name="limit">The number of records to skip</param>
		public static IQueryable<T> Limit<T>(this IQueryable<T> records, int limit) {
			return limit > 0 ? records.Take(limit) : records;
		}

		/// <summary>
		/// Skips <paramref name="count"/> records from the beginning of the record set
		/// </summary>
		/// <typeparam name="T">The resource type</typeparam>
		/// <param name="count">The number of records to skip</param>
		public static IQueryable<T> Offset<T>(this IQueryable<T> records, int count) {
			return records.Skip(count);
		}

		/// <summary>
		/// Sorts the record set
		/// </summary>
		/// <typeparam name="T">The resource type</typeparam>
		/// <param name="getFieldMapping">Function to get an expression representing the mapping from a field name to a property on <typeparamref name="T"/></param>
		/// <param name="sortGroupings">Sort information from the RESTful request</param>
		/// <returns>The sorted record set</returns>
		public static IQueryable<T> Sort<T>(this IQueryable<T> records, Func<string, Expression<Func<T, object>>> getFieldMapping, IEnumerable<SortGrouping> sortGroupings) {
			IOrderedQueryable<T> orderedRecords = null;
			foreach (var grouping in sortGroupings) {
				var fieldSelector = getFieldMapping(grouping.Field);
				if (fieldSelector == null) {
					throw new UnknownCriterionException(grouping.Field);
				}

				if (grouping.Order == SortOrder.Descending) {
					orderedRecords = orderedRecords != null ? orderedRecords.ThenByDescending(fieldSelector) : records.OrderByDescending(fieldSelector);
				} else {
					orderedRecords = orderedRecords != null ? orderedRecords.ThenBy(fieldSelector) : records.OrderBy(fieldSelector);
				}
			}

			return orderedRecords ?? records;
		}

		/// <summary>
		/// Filters the record set
		/// </summary>
		/// <typeparam name="T">The resource type</typeparam>
		/// <param name="criteria">Filter criteria from the RESTful request</param>
		/// <param name="valueHandlers">Optional criterion value handlers</param>
		/// <returns>The filtered record set</returns>
		public static IQueryable<T> Filter<T>(this IQueryable<T> records, CriterionSet criteria, IDictionary<string, IValueHandler> valueHandlers) {
			var filter = criteria
				.Aggregate<Criterion, Expression<Func<T, bool>>>(null, (expression, criterion) =>
					ExpressionHelper.Compose(
						expression,
						GetCriterionHandler(criterion.FieldName, valueHandlers).CreateExpression<T>(criterion),
						FieldValueModifier.BooleanAnd
					)
				);

			return filter != null ? records.Where(filter) : records;
		}

		private static IValueHandler GetCriterionHandler(string fieldName, IDictionary<string, IValueHandler> valueHandlers) {
			if (valueHandlers == null) {
				return new DefaultValueHandler();
			}

			if (!valueHandlers.ContainsKey(fieldName) || valueHandlers[fieldName] == null) {
				throw new UnknownCriterionException(fieldName);
			}

			return valueHandlers[fieldName];
		}
	}
}