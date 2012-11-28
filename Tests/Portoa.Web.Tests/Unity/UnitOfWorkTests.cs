using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Moq;
using NUnit.Framework;
using Portoa.Persistence;
using Portoa.Web.Unity;
using Portoa.Web.Unity.Persistence;

namespace Portoa.Web.Tests.Unity {
	[TestFixture]
	public class UnitOfWorkTests {
		private IUnityContainer container;

		[SetUp]
		public void SetUp() {
			container = new UnityContainer()
				.AddNewExtension<Interception>()
				.AddNewExtension<EnableUnitOfWork>()
				.RegisterAndIntercept<Foo>();
		}

		[Test]
		public void Should_commit_transaction_when_method_is_invoked() {
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(uow => uow.Start()).Verifiable();
			mockUnitOfWork.Setup(uow => uow.Commit()).Verifiable();
			mockUnitOfWork.Setup(uow => uow.Rollback()).Verifiable();

			container.RegisterInstance(mockUnitOfWork.Object);

			container.Resolve<Foo>().Success();

			mockUnitOfWork.Verify(uow => uow.Start(), Times.Once());
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once());
			mockUnitOfWork.Verify(uow => uow.Rollback(), Times.Never());
		}

		[Test]
		public void Should_rollback_transaction_when_method_throws_exception() {
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(uow => uow.Start()).Verifiable();
			mockUnitOfWork.Setup(uow => uow.Commit()).Verifiable();
			mockUnitOfWork.Setup(uow => uow.Rollback()).Verifiable();

			container.RegisterInstance(mockUnitOfWork.Object);

			try {
				container.Resolve<Foo>().Failure();
			} catch { }

			mockUnitOfWork.Verify(uow => uow.Start(), Times.Once());
			mockUnitOfWork.Verify(uow => uow.Commit(), Times.Never());
			mockUnitOfWork.Verify(uow => uow.Rollback(), Times.Once());
		}

		public class Foo : MarshalByRefObject {
			[UnitOfWork]
			public void Success() { }

			[UnitOfWork]
			public void Failure() {
				throw new Exception();
			}
		}
	}
}