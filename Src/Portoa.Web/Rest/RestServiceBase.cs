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
		/// no mapping
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
					throw new UnknownFieldNameException(grouping.Field);
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
		/// Fetches records, filtering by the given <c cref="RestRequest.Criteria">criteria</c>
		/// </summary>
		/// <typeparam name="T">The type of entity to fetch</typeparam>
		/// <typeparam name="TDto">The DTO representation of the entity, suitable for serialization</typeparam>
		/// <typeparam name="TId">The entity's identifier type </typeparam>
		/// <param name="request">The object representing the RESTful request</param>
		/// <param name="records">Initial record set to filter</param>
		/// <param name="criterionHandlers">Specific criterion handlers to guide the filtering process</param>
		/// <param name="idSelector">Optional expression identifying the entity's identifier property</param>
		/// <exception cref="RestException">If <paramref name="idSelector"/> is not given but a single resource by ID is requested</exception>
		/// <exception cref="UnknownCriterionException">If a criterion key is not present in the <c cref="criterionHandlers">dictionary</c></exception>
		/// <exception cref="InvalidOperationException">If the <paramref name="idSelector"/> cannot be evaluated to a valid property</exception>
		/// <returns>The filtered record set</returns>
		protected IEnumerable<TDto> GetRecords<T, TDto, TId>(RestRequest request, IQueryable<T> records, IDictionary<string, CriterionHandler<T>> criterionHandlers, Expression<Func<T, TId>> idSelector) where TDto : new() {
			if (request.FetchAll) {
				var expressionBuilder = new List<Func<T, bool>>();
				foreach (var kvp in request.Criteria) {
					if (!criterionHandlers.ContainsKey(kvp.Key)) {
						throw new UnknownCriterionException(string.Format("Unknown criterion: \"{0}\"", kvp.Key));
					}

					expressionBuilder.AddRange(criterionHandlers[kvp.Key].HandleCriterion(kvp.Value));
				}

				if (expressionBuilder.Count > 0) {
					records = records.Where(entity => expressionBuilder.Aggregate(false, (current, next) => current || next(entity)));
				}

				records = GetSortedRecords(records, request.SortInfo);
			} else if (idSelector == null) {
				throw new RestException("Unable to fetch single values based on ID");
			} else {
				records = records.Where(entity => Equals(idSelector.Compile()(entity), ConvertId(request.Id, GetPropertyType(idSelector))));
			}

			return records.ToArray().Select(entity => entity.ToDto<TDto>());
		}

		/// <summary>
		/// Fetches records, filtering by the given <c cref="RestRequest.Criteria">criteria</c>. This method does not allow
		/// selecting a single record by ID; if that is desired use the <see cref="GetRecords{T,TDto,TId}"/> overload.
		/// </summary>
		/// <typeparam name="T">The type of entity to fetch</typeparam>
		/// <typeparam name="TDto">The DTO representation of the entity, suitable for serialization</typeparam>
		/// <param name="request">The object representing the RESTful request</param>
		/// <param name="records">Initial record set to filter</param>
		/// <param name="criterionHandlers">Specific criterion handlers to guide the filtering process</param>
		/// <returns>The filtered record set</returns>
		/// <seealso cref="GetRecords{T,TDto,TId}"/>
		protected IEnumerable<TDto> GetRecords<T, TDto>(RestRequest request, IQueryable<T> records, IDictionary<string, CriterionHandler<T>> criterionHandlers) where TDto : new() {
			return GetRecords<T, TDto, object>(request, records, criterionHandlers, idSelector: null);
		}

		private static Type GetPropertyType<T, TReturn>(Expression<Func<T, TReturn>> expression) {
			try {
				return ((PropertyInfo)((MemberExpression)expression.Body).Member).PropertyType;
			} catch (Exception e) {
				throw new InvalidOperationException("idSelector needs to be a MemberExpression pointing to a property in the form of foo => foo.Id", e);
			}
		}

		/// <summary>
		/// Converts the string value of an entity's identifier to the correct type. Default
		/// implementation uses <see cref="Convert.ChangeType(object, Type)"/>.
		/// </summary>
		/// <param name="idValue">The string value of the entity's identifier</param>
		/// <param name="idType">The type of the identifier</param>
		protected virtual object ConvertId(string idValue, Type idType) {
			return Convert.ChangeType(idValue, idType);
		}
	}
}