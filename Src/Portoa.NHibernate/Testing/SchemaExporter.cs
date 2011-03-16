using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Portoa.Testing;

namespace Portoa.NHibernate.Testing {
	public class SchemaExporter : IExecutableScript {
		private readonly Configuration cfg;
		private readonly string delimiter;
		private readonly string outputFile;

		public SchemaExporter(Configuration cfg, string delimiter = null, string outputFile = null) {
			this.cfg = cfg;
			this.delimiter = delimiter;
			this.outputFile = outputFile;
		}

		public string Name { get { return "NHibernate Schema Exporter"; } }

		public void Execute(QueryExecutor queryExecutor, string connectionString) {
			cfg.Properties[Environment.ConnectionString] = connectionString;

			var exporter = new SchemaExport(cfg)
				.SetDelimiter(delimiter);

			if (!string.IsNullOrEmpty(outputFile)) {
				exporter.SetOutputFile(outputFile);
			}

			exporter.Execute(script: false, export: true, justDrop: false);
		}
	}
}