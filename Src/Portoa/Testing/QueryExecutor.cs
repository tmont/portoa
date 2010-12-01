using System.Data;

namespace Portoa.Testing {
	public class QueryExecutor {
		
		public QueryExecutor(IDbFactory dbFactory) {
			DbFactory = dbFactory;
		}

		public IDbFactory DbFactory { get; private set; }

		public DataTable ExecuteDataTable(string query, string connectionString) {
			using (var conn = DbFactory.CreateConnection(connectionString)) {
				conn.Open();
				var cmd = DbFactory.CreateCommand(query, conn);
				var reader = cmd.ExecuteReader();
				var dataTable = new DataTable();
				if (reader != null) {
					dataTable.Load(reader);
				}

				return dataTable;
			}
		}

		public int ExecuteNonQuery(string query, string connectionString) {
			using (var conn = DbFactory.CreateConnection(connectionString)) {
				conn.Open();
				return DbFactory.CreateCommand(query, conn).ExecuteNonQuery();
			}
		}

		public T ExecuteScalar<T>(string query, string connectionString) {
			using (var conn = DbFactory.CreateConnection(connectionString)) {
				conn.Open();
				return (T)(DbFactory.CreateCommand(query, conn).ExecuteScalar());
			}
		}
	}
}