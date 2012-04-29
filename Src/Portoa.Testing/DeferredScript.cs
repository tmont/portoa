using System;

namespace Portoa.Testing {
	/// <summary>
	/// Executable script that retrieves the queries to be executed at runtime
	/// </summary>
	public class DeferredScript : IExecutableScript {
		private readonly Func<string> queryRetriever;

		/// <param name="queryRetriever">Function to retrieve the queries to be run</param>
		public DeferredScript(Func<string> queryRetriever) {
			this.queryRetriever = queryRetriever;
		}

		public string Name { get; set; }

		public void Execute(QueryExecutor queryExecutor, string connectionString) {
			queryExecutor.ExecuteNonQuery(queryRetriever(), connectionString);
		}
	}
}