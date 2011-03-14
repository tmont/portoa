using System;
using System.Collections.Generic;
using System.Linq;
using Portoa.Logging;

namespace Portoa.Testing {
	public class DbTestingUtility {
		private readonly QueryExecutor queryExecutor;
		private readonly ILogger logger;
		private readonly List<Exception> errors;
		private bool handledErrors;

		private readonly IList<IScriptable> schemaScripts;
		private readonly IList<IScriptable> setupScripts;
		private readonly IList<IScriptable> tearDownScripts;

		public DbTestingUtility(QueryExecutor queryExecutor, ILogger logger = null) {
			this.queryExecutor = queryExecutor;
			this.logger = logger ?? new DebugLogger();
			errors = new List<Exception>();
			schemaScripts = new List<IScriptable>();
			setupScripts = new List<IScriptable>();
			tearDownScripts = new List<IScriptable>();
		}

		public string GeneratedDatabase { get; private set; }

		/// <summary>
		/// Gets the connection string for the fake database
		/// </summary>
		public string ConnectionString { get; private set; }

		/// <summary>
		/// Sets the default connection string (do not include the initial catalog)
		/// </summary>
		public string DefaultConnectionString { get; set; }

		public void SetUp() {
			handledErrors = false;
			errors.Clear();
			logger.Info("Setting up the test");

			CreateDatabase();
			CreateSchema();
			RunSetUpScripts();

			CheckForErrors();
		}
		
		private void CheckForErrors() {
			if (!handledErrors && errors.Count > 0) {
				var message = errors.Aggregate(errors.Count + " errors occurred\n", (current, next) => current + next + "\n");
				handledErrors = true;
				logger.Error("Encountered errors during test; barfing out an exception.");
				throw new Exception(message);
			}
		}

		public void TearDown() {
			logger.Info("Tearing down the test");

			RunTearDownScripts();
			DropDatabase();

			//handle any erors that occurred during setup
			CheckForErrors();
		}

		private void RunSetUpScripts() {
			foreach (var script in setupScripts) {
				RunSqlScript(script);
			}
		}

		private void RunTearDownScripts() {
			foreach (var script in tearDownScripts) {
				RunSqlScript(script);
			}
		}

		public void CreateSchema() {
			foreach (var script in schemaScripts) {
				RunSqlScript(script);
			}
		}

		/// <summary>
		/// Drops the temp database and resets the generated database name
		/// </summary>
		public void DropDatabase() {
			queryExecutor.DbFactory.CloseConnections(GeneratedDatabase, ConnectionString);
			queryExecutor.ExecuteNonQuery("DROP DATABASE " + GeneratedDatabase, ConnectionString);
			logger.Debug("Dropped database \"" + GeneratedDatabase + "\"");
			GeneratedDatabase = string.Empty;
		}

		/// <summary>
		/// Creates a temporary database with a unique name
		/// </summary>
		public DbTestingUtility CreateDatabase() {
			if (string.IsNullOrEmpty(GeneratedDatabase)) {
				//don't need to create more than one database
				GeneratedDatabase = "__fake_" + Guid.NewGuid().ToString().Replace("-", "_");
				ConnectionString = DefaultConnectionString;

				ConnectionString += ";Initial Catalog=" + GeneratedDatabase;
				logger.Debug("Set connection string to " + ConnectionString);
			}

			queryExecutor.ExecuteNonQuery("CREATE DATABASE " + GeneratedDatabase, DefaultConnectionString);
			logger.Debug("Created database " + GeneratedDatabase);

			return this;
		}

		/// <summary>
		/// Executes a SQL script on the test database
		/// </summary>
		protected void RunSqlScript(IScriptable script) {
			logger.Info("Running {0}", script.Name);

			try {
				script.Execute(queryExecutor, ConnectionString);
			} catch (Exception e) {
				errors.Add(e);
			}
		}

		public DbTestingUtility AddSchemaScript(IScriptable script) {
			schemaScripts.Add(script);
			return this;
		}

		public DbTestingUtility AddSetUpScript(IScriptable script) {
			setupScripts.Add(script);
			return this;
		}

		public DbTestingUtility AddTearDownScript(IScriptable script) {
			tearDownScripts.Add(script);
			return this;
		}
	}
}