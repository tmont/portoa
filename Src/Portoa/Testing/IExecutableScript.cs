namespace Portoa.Testing {
	/// <summary>
	/// Represents a single object that will perform a group
	/// of actions against a database
	/// </summary>
	public interface IExecutableScript {
		/// <summary>
		/// The name of the script
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Executes the script
		/// </summary>
		/// <param name="queryExecutor">The object used to query the database</param>
		/// <param name="connectionString">The connection string identifying the database and server</param>
		void Execute(QueryExecutor queryExecutor, string connectionString);
	}
}