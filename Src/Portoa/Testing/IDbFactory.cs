using System.Data;

namespace Portoa.Testing {
	public interface IDbFactory {
		IDbConnection CreateConnection(string connectionString);
		IDbCommand CreateCommand(string query, IDbConnection conn);
		void CloseConnections(string database, string connectionString);
	}
}