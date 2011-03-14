using System;

namespace Portoa.Testing {
	public class DeferredScript : IScriptable {
		private readonly Func<string> function;

		public DeferredScript(Func<string> function) {
			this.function = function;
		}

		public string Name { get; set; }
		public void Execute(QueryExecutor queryExecutor, string connectionString) {
			queryExecutor.ExecuteNonQuery(function(), connectionString);
		}
	}
}