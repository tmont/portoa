using System;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Portoa.NHibernate;
using Portoa.Util;
using Portoa.Web.Rest.MvcTest.Controllers;

namespace Portoa.Web.Rest.MvcTest.Data {
	[TestFixture, Ignore]
	public class SchemaExporter {
		[Test]
		public void GenerateSchemaAndData() {
			var cfg = new global::NHibernate.Cfg.Configuration().Configure();
			new SchemaExport(cfg)
				.SetDelimiter(";")
				.Execute(false, true, false);

			var random = new Random();
			Func<DateTime> before2000 = () => new DateTime(random.Next(1950, 2000), random.Next(1, 13), random.Next(1, 29));
			Func<DateTime> before2005 = () => new DateTime(random.Next(2000, 2005), random.Next(1, 13), random.Next(1, 29));

			//some user data
			var users = new[] {
				new User {Created = before2000(), Email = "foo@bar.com", Username = "Foo" },
				new User {Created = before2000(), Email = "f00@baz.com", Username = "Foobar" },
				new User {Created = before2000(), Email = "foo@bat.com", Username = "Fubar" },
				new User {Created = DateTime.Now, Email = "foo@quux.com", Username = "Bill" },
				new User {Created = before2000(), Email = "foo@asdf.com", Username = "Billy" },
				new User {Created = before2000(), Email = "asdf@asdf.com", Username = "Dilliam" },
				new User {Created = before2000(), Email = "asdf@asdf.com", Username = "William" },
				new User { Email = "jkl@bar.com", Username = "Squilliam" },
				new User { Email = "foo@jkl.com", Username = "Bob" },
				new User { Email = "jklasdf@bar.com", Username = "Bobby" },
				new User { Email = "asdfd@asdf.com", Username = "Bobert" },
				new User {Created = before2005(), Email = "skank1@skankcity.com", Username = "Skank #1" },
				new User {Created = before2005(), Email = "skank2@skankcity.com", Username = "Skank #2" },
				new User {Created = before2005(), Email = "skank3@skankcity.com", Username = "Skank #3" },
				new User { Email = "not-a-skank@iwanttobeaskank.com", Username = "Wannabe Skank" },
			};

			using (var sessionFactory = cfg.BuildSessionFactory()) {
				using (var session = sessionFactory.OpenSession()) {
					using (var tx = session.BeginTransaction()) {
						var repo = new NHibernateRepository<User>(session);
						users.Walk(user => repo.Save(user));
						tx.Commit();
					}
				}
			}

		}
	}
}