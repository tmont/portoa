using System;
using System.IO;
using System.Reflection;

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

	public class FileScript : IExecutableScript {
		private readonly IExecutableScript script;
		public FileScript(string fileName) {
			FileName = fileName;
			script = new DeferredScript(() => File.ReadAllText(FileName));
		}

		public string FileName { get; private set; }
		public string Name { get { return string.Format("File:{0}", FileName); } }

		public void Execute(QueryExecutor queryExecutor, string connectionString) {
			script.Execute(queryExecutor, connectionString);
		}
	}

	public class ResourceScript : IExecutableScript {
		private readonly Assembly assembly;
		private readonly string resourceName;

		public ResourceScript(Assembly assembly, string resourceName) {
			this.assembly = assembly;
			this.resourceName = resourceName;
		}

		public string Name { get { return string.Format("Embedded resource: {0}:{1}", assembly, resourceName); } }

		public void Execute(QueryExecutor queryExecutor, string connectionString) {
			string data;
			using (var stream = assembly.GetManifestResourceStream(resourceName)) {
				if (stream == null) {
					var message = string.Format("Resource {0} not found in {1}", resourceName, assembly);
					throw new Exception(message);
				}
				data = new StreamReader(stream).ReadToEnd();
			}

			queryExecutor.ExecuteNonQuery(data, connectionString);
		}
	}
}