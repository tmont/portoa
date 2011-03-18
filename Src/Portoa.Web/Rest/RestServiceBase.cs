using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
		/// <typeparam name="T">The type on which to map the field name to a property</typeparam>
		/// <param name="fieldName">The name of the field given in the criterion</param>
		[CanBeNull]
		protected virtual Expression<Func<T, object>> GetFieldMapping<T>(string fieldName) {
			return BuildExpression<T, object>(fieldName);
		}

		private static Expression<Func<T, TReturn>> BuildExpression<T, TReturn>(string fieldName) {
			var type = typeof(T);
			var property = type.GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
			if (property == null) {
				return null;
			}

			var parameter = Expression.Parameter(type, "resource");
			var body = Expression.Convert(Expression.Property(parameter, property), typeof(TReturn));

			return Expression.Lambda<Func<T, TReturn>>(body, parameter);
		}

		private IQueryable<T> GetSortedRecords<T>(IQueryable<T> records, IEnumerable<SortGrouping> sortGroupings) {
			IOrderedQueryable<T> orderedRecords = null;
			foreach (var grouping in sortGroupings) {
				var fieldSelector = GetFieldMapping<T>(grouping.Field);
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

		private static ICriterionHandler GetCriterionHandler(string fieldName, IDictionary<string, ICriterionHandler> criterionHandlers) {
			if (criterionHandlers == null) {
				return new DefaultCriterionHandler();
			}

			if (!criterionHandlers.ContainsKey(fieldName)) {
				throw new UnknownCriterionException(string.Format("Unknown criterion: \"{0}\"", fieldName));
			}

			return criterionHandlers[fieldName];
		}

		/// <summary>
		/// Fetches records, filtering by the given <c cref="RestRequest.Criteria">criteria</c>
		/// </summary>
		/// <typeparam name="T">The type of entity to fetch</typeparam>
		/// <typeparam name="TDto">The DTO representation of the entity, suitable for serialization</typeparam>
		/// <param name="request">The object representing the RESTful request</param>
		/// <param name="records">Initial record set to filter</param>
		/// <param name="criterionHandlers">Specific criterion handlers to guide the filtering process; if not given, defaults to <see cref="DefaultCriterionHandler"/></param>
		/// <returns>The filtered record set</returns>
		protected IEnumerable<TDto> GetRecords<T, TDto>(RestRequest request, IQueryable<T> records, IDictionary<string, ICriterionHandler> criterionHandlers = null) where TDto : new() {
			var filter = request
				.Criteria
				.Aggregate<Criterion, Expression<Func<T, bool>>>(null, (expression, criterion) =>
					ExpressionHelper.Compose(
						expression, 
						GetCriterionHandler(criterion.FieldName, criterionHandlers).HandleCriterion<T>(criterion), 
						FieldValueModifier.BooleanAnd
					)
				);

			if (filter != null) {
				records = records.Where(filter);
			}

			records = GetSortedRecords(records, request.SortInfo);
			return records.ToArray().Select(entity => entity.ToDto<TDto>());
		}
	}
}