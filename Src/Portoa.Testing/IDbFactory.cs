using System.Data;

namespace Portoa.Testing {
	/// <summary>
	/// Wrapper around a database that enables creating connections and commands
	/// </summary>
	public interface IDbFactory {
		/// <summary>
		/// Creates a vendor-specific <see cref="IDbConnection"/>
		/// </summary>
		/// <param name="connectionString">The connection string used to identify the server to connect to</param>
		IDbConnection CreateConnection(string connectionString);
		/// <summary>
		/// Creates a vendor-specific <see cref="IDbCommand"/> for the given <paramref name="query"/>
		/// </summary>
		/// <param name="query">The query to create the command for</param>
		/// <param name="conn">The connection (probably created by <see cref="CreateConnection"/>)</param>
		IDbCommand CreateCommand(string query, IDbConnection conn);
		/// <summary>
		/// Closes all open connections for the given <paramref name="database"/> and 
		/// <paramref name="connectionString">connection string</paramref>
		/// </summary>
		/// <param name="database">The database to close connections on</param>
		/// <param name="connectionString">The connection string identifying the server</param>
		void CloseConnections(string database, string connectionString);
	}
}