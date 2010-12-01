namespace Portoa.Persistence {
	public interface ISoftDeletable {
		void Delete();
		void Undelete();
		bool IsDeleted { get; }
	}
}