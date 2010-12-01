using System.Collections.Generic;

namespace Portoa.Persistence {

	public interface IRepository<T> : IRepository<T, int> where T : Entity<T, int> { }

	public interface IRepository<T, in TId> where T : Entity<T, TId> {
		T Save(T entity);
		T Reload(T entity);
		void Delete(TId id);
		T FindById(TId id);
		IEnumerable<T> Records { get; }
	}
}