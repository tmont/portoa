using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Portoa.Testing {
	[TestFixture]
	public abstract class ContainerWiredTest {

		protected IUnityContainer Container { get; private set; }

		[SetUp]
		public virtual void SetUp() {
			Container = new UnityContainer();
		}

		[TearDown]
		public virtual void TearDown() {
			if (Container != null) {
				Container.Dispose();
			}
		}
	}
}