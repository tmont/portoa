using Microsoft.Practices.Unity;
using Moq;

namespace Portoa.Testing {
	public static class MoqExtensions {
		/// <summary>
		/// Registers a mock object with the container
		/// </summary>
		public static Mock<T> RegisterMock<T>(this IUnityContainer container) where T : class {
			var mock = new Mock<T>();
			container.RegisterInstance(mock);
			container.RegisterInstance(mock.Object);
			return mock;
		}

		/// <summary>
		/// Configure a mock object that has already been registered with
		/// the container
		/// </summary>
		public static Mock<T> ConfigureMock<T>(this IUnityContainer container) where T : class {
			return container.Resolve<Mock<T>>();
		}
	}
}