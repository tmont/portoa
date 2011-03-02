using System.Collections.Generic;
using Portoa.Persistence;

namespace Portoa.Search {
	/// <summary>
	/// Default search service implementation for entities with integral identifiers
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class DefaultSearchService<T> : ISearchService<T, int> where T : class, IIdentifiable<int> {
		public abstract IEnumerable<T> FindByIds(IEnumerable<int> ids);
		public abstract IEnumerable<T> GetAllIndexableRecords();

		public int ConvertIdFromStringValue(string idValue) {
			return int.Parse(idValue);
		}
	}
}