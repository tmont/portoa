using System.Data;

namespace Portoa.Testing {
	/// <summary>
	/// Object that encapsulates interaction with a database (executing queries and such)
	/// </summary>
	public class QueryExecutor {
		/// <param name="dbFactory">Vendor-specific factory for actually executing queries</param>
		public QueryExecutor(IDbFactory dbFactory) {
			DbFactory = dbFactory;
		}

		/// <summary>
		/// Gets the vendor-specific factory associated with this executor
		/// </summary>
		public IDbFactory DbFactory { get; private set; }

		/// <summary>
		/// Executes a <c>SELECT</c> <paramref name="query"/> and returns it as a <see cref="DataTable"/>
		/// </summary>
		/// <param name="query">The query to execute</param>
		/// <param name="connectionString">The database connection string</param>
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

		/// <summary>
		/// Executes a <paramref name="query"/> that doesn't have a result set
		/// </summary>
		/// <param name="query">The query to execute</param>
		/// <param name="connectionString">The database connection string</param>
		public int ExecuteNonQuery(string query, string connectionString) {
			using (var conn = DbFactory.CreateConnection(connectionString)) {
				conn.Open();
				return DbFactory.CreateCommand(query, conn).ExecuteNonQuery();
			}
		}

		/// <sumary>
		/// Executes a <paramref name="query"/> that returns a single value
		/// </sumary>
		/// <param name="query">The query to execute</param>
		/// <param name="connectionString">The database connection string</param>
		public T ExecuteScalar<T>(string query, string connectionString) {
			using (var conn = DbFactory.CreateConnection(connectionString)) {
				conn.Open();
				return (T)(DbFactory.CreateCommand(query, conn).ExecuteScalar());
			}
		}
	}
}