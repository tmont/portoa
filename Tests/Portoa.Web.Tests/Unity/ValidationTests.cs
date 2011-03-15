using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Moq;
using NUnit.Framework;
using Portoa.Persistence;
using Portoa.Validation;
using Portoa.Validation.DataAnnotations;
using Portoa.Web.Unity;
using Portoa.Web.Unity.Validation;

namespace Portoa.Web.Tests.Unity {
	[TestFixture]
	public class ValidationTests {
		private IUnityContainer container;

		[SetUp]
		public void SetUp() {
			container = new UnityContainer()
				.AddNewExtension<Interception>()
				.AddNewExtension<ValidateEntityOnSave>()
				.RegisterType<IServiceProvider, ContainerResolvingServiceProvider>()
				.RegisterType<IEntityValidator, EntityValidator>()
				.RegisterAndIntercept(typeof(IRepository<>), typeof(MyRepository<>));
		}

		[TearDown]
		public void TearDown() {
			container.Dispose();
		}

		[Test]
		public void Should_validate_when_save_is_called_and_throw_exception() {
			var repo = container.Resolve<IRepository<ValidatableEntity>>();
			try {
				repo.Save(new ValidatableEntity());
				Assert.Fail("Exception was not thrown");
			} catch (ValidationFailedException e) {
				Assert.That(e.Results.Count(), Is.EqualTo(2));

				var result = e.Results.First();
				Assert.That(result.ErrorMessage, Is.EqualTo("Bar fail"));
				Assert.That(result.MemberNames.Count(), Is.EqualTo(1));
				Assert.That(result.MemberNames.First(), Is.EqualTo("Bar"));

				result = e.Results.Last();
				Assert.That(result.ErrorMessage, Is.EqualTo("foo fail"));
				Assert.That(result.MemberNames.Count(), Is.EqualTo(1));
				Assert.That(result.MemberNames.First(), Is.EqualTo("foo"));
			}
		}

		[Test]
		public void Should_validate_when_save_is_called_and_continue_when_valid() {
			var mockRepo = new Mock<IRepository<ValidatableEntity>>();
			mockRepo.Setup(r => r.Save(It.IsAny<ValidatableEntity>())).Verifiable();

			container.RegisterAndIntercept(mockRepo.Object);

			var repo = container.Resolve<IRepository<ValidatableEntity>>();

			repo.Save(new ValidatableEntity("not null") { Bar = "not null" });
			mockRepo.VerifyAll();
		}

		public class MyRepository<T> : IRepository<T> {
			public T Save(T entity) {
				return entity;
			}

			public T Reload(T entity) {
				return entity;
			}

			public void Delete(object id) { }

			public T FindById(object id) {
				return default(T);
			}

			public IQueryable<T> Records { get { return Enumerable.Empty<T>().AsQueryable(); } }
		}

		public class ValidatableEntity : Entity<int> {
			[Required(ErrorMessage = "foo fail")]
			private string foo;

			public ValidatableEntity(string foo = null) {
				this.foo = foo;
			}

			[Required(ErrorMessage = "Bar fail")]
			public string Bar { get; set; }
		}

	}
}