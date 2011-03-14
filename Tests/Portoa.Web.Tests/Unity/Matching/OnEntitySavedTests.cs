using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using Portoa.Persistence;
using Portoa.Web.Unity.Matching;

namespace Portoa.Web.Tests.Unity.Matching {
	[TestFixture]
	public class OnEntitySavedTests {
		[Test]
		public void Should_not_match_save_method_using_derived_type() {
			var method = ((MethodCallExpression)((Expression<Action<MyRepository<MyEntity, int>>>)(repo => repo.Save(new MyEntity()))).Body).Method;
			Assert.That(new OnEntitySaved().Matches(method), Is.False);
		}

		[Test]
		public void Should_match_save_method_using_interface() {
			var method = ((MethodCallExpression)((Expression<Action<IRepository<MyEntity, int>>>)(repo => repo.Save(new MyEntity()))).Body).Method;
			Assert.That(new OnEntitySaved().Matches(method), Is.True);
		}

		public class MyEntity : Entity<int> { }
		public class MyRepository<T, TId> : IRepository<T, TId> where T : IIdentifiable<TId> {
			public T Save(T entity) {
				return entity;
			}

			public T Reload(T entity) {
				return entity;
			}

			public void Delete(TId id) {

			}

			public T FindById(TId id) {
				return default(T);
			}

			public IQueryable<T> Records { get { return Enumerable.Empty<T>().AsQueryable(); } }
		}
	}
}