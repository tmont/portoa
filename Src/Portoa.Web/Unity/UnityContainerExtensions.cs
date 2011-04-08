using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity {
	public static class UnityContainerExtensions {
		/// <summary>
		/// Adds an extension if it hasn't already been registered with the container
		/// </summary>
		public static IUnityContainer AddExtensionOnce<T>(this IUnityContainer container) where T : UnityContainerExtension, new() {
			if (container.Configure<T>() == null) {
				container.AddNewExtension<T>();
			}

			return container;
		}

		/// <summary>
		/// Determines whether any of the given types are registered in the container
		/// </summary>
		public static bool AnyAreRegistered(this IUnityContainer container, params Type[] types) {
			return types.Any(type => container.IsRegistered(type));
		}

		/// <summary>
		/// Determines if all of the given types are registered in the container
		/// </summary>
		/// <param name="container"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public static bool AllAreRegistered(this IUnityContainer container, params Type[] types) {
			return types.All(type => container.IsRegistered(type));
		}

		/// <summary>
		/// Resolves <typeparamref name="T"/> via the <paramref name="container"/> if it's registered,
		/// or the default value if not
		/// </summary>
		/// <typeparam name="T">The type to resolve</typeparam>
		[CanBeNull]
		public static T TryResolve<T>(this IUnityContainer container) {
			return container.IsRegistered<T>() ? container.Resolve<T>() : default(T);
		}

		#region type registration with interception
		private static void VerifyTypeIsInterface(Type type) {
			if (!type.IsInterface) {
				throw new ArgumentException("The type parameter must be an interface");
			}
		}

		/// <summary>
		/// Registers the type and configures an <see cref="InterfaceInterceptor"/> for
		/// <typeparamref name="TFrom"/>
		/// </summary>
		/// <typeparam name="TFrom">This must be an interface type</typeparam>
		/// <typeparam name="TTo">The type to resolve the interface to</typeparam>
		public static IUnityContainer RegisterInterfaceAndIntercept<TFrom, TTo>(this IUnityContainer container, LifetimeManager lifetimeManager = null, params InjectionMember[] members) where TTo : TFrom {
			VerifyTypeIsInterface(typeof(TFrom));

			if (lifetimeManager == null) {
				container.RegisterType<TFrom, TTo>(members);
			} else {
				container.RegisterType<TFrom, TTo>(lifetimeManager, members);
			}

			container
				.Configure<Interception>()
				.SetInterceptorFor<TFrom>(new InterfaceInterceptor());

			return container;
		}

		/// <summary>
		/// Registers the interface type and configures interception for it
		/// </summary>
		public static IUnityContainer RegisterInterfaceAndIntercept<TInterface>(this IUnityContainer container, LifetimeManager lifetimeManager = null, params InjectionMember[] members) {
			VerifyTypeIsInterface(typeof(TInterface));

			if (lifetimeManager == null) {
				container.RegisterType<TInterface>(members);
			} else {
				container.RegisterType<TInterface>(lifetimeManager, members);
			}
			
			container
				.Configure<Interception>()
				.SetInterceptorFor<TInterface>(new InterfaceInterceptor());

			return container;
		}

		/// <summary>
		/// Registers the type and configures interception for it
		/// </summary>
		public static IUnityContainer RegisterAndIntercept<TFrom, TTo>(this IUnityContainer container, LifetimeManager lifetimeManager = null, params InjectionMember[] members) where TTo : TFrom {
			if (lifetimeManager == null) {
				container.RegisterType<TFrom, TTo>(members);
			} else {
				container.RegisterType<TFrom, TTo>(lifetimeManager, members);
			}

			container
				.Configure<Interception>()
				.SetInterceptorFor<TFrom>(new TransparentProxyInterceptor());

			return container;
		}

		/// <summary>
		/// Registers the type and configures interception for it
		/// </summary>
		public static IUnityContainer RegisterAndIntercept<T>(this IUnityContainer container, LifetimeManager lifetimeManager = null, params InjectionMember[] members) {
			if (lifetimeManager == null) {
				container.RegisterType<T>(members);
			} else {
				container.RegisterType<T>(lifetimeManager, members);
			}

			container
				.Configure<Interception>()
				.SetInterceptorFor<T>(new TransparentProxyInterceptor());

			return container;
		}

		/// <summary>
		/// Registers the instance and configures interception for it
		/// </summary>
		public static IUnityContainer RegisterAndIntercept<T>(this IUnityContainer container, T instance, LifetimeManager lifetimeManager = null) {
			if (lifetimeManager == null) {
				container.RegisterInstance(instance);
			} else {
				container.RegisterInstance(instance, lifetimeManager);
			}

			container
				.Configure<Interception>()
				.SetInterceptorFor<T>(new TransparentProxyInterceptor());

			return container;
		}

		/// <summary>
		/// Registers the type and configures interception for it
		/// </summary>
		/// <param name="typeFrom">This must be an interface type</param>
		public static IUnityContainer RegisterInterfaceAndIntercept(this IUnityContainer container, Type typeFrom, Type typeTo, LifetimeManager lifetimeManager = null, params InjectionMember[] members) {
			VerifyTypeIsInterface(typeFrom);

			if (lifetimeManager == null) {
				container.RegisterType(typeFrom, typeTo, members);
			} else {
				container.RegisterType(typeFrom, typeTo, lifetimeManager, members);
			}
			
			container
				.Configure<Interception>()
				.SetInterceptorFor(typeFrom, new InterfaceInterceptor());

			return container;
		}

		/// <summary>
		/// Registers the type and configures interception for it
		/// </summary>
		public static IUnityContainer RegisterAndIntercept(this IUnityContainer container, Type typeFrom, Type typeTo, LifetimeManager lifetimeManager = null, params InjectionMember[] members) {
			if (lifetimeManager == null) {
				container.RegisterType(typeFrom, typeTo, members);
			} else {
				container.RegisterType(typeFrom, typeTo, lifetimeManager, members);
			}

			container
				.Configure<Interception>()
				.SetInterceptorFor(typeFrom, new TransparentProxyInterceptor());

			return container;
		}
		#endregion
	}
}