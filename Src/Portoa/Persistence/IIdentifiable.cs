namespace Portoa.Persistence {
	public interface IIdentifiable<T> {
		T Id { get; set; }
	}
}