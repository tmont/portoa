namespace Portoa.Persistence {
	/// <summary>
	/// Represents a service object that can start a transaction
	/// </summary>
	public interface ITransactableService {
		/// <summary>
		/// Begins a transaction and returns the resultant <see cref="IUnitOfWork"/>
		/// </summary>
		IUnitOfWork BeginTransaction();
	}
}