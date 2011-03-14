namespace Portoa.Testing {
	public interface IScriptable {
		string Name { get; }
		void Execute(QueryExecutor queryExecutor, string connectionString);
	}
}