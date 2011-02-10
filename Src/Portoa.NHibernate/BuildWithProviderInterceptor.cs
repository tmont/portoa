using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Mapping;

namespace Portoa.NHibernate {
	/// <remarks>Stolen mostly from Mike Valenty</remarks>
	public class BuildWithProviderInterceptor : EmptyInterceptor, IInstantiatable {
		private readonly ICollection<PersistentClass> classMappings;
		private readonly IServiceProvider provider;

		public event Action<string, object, IServiceProvider> BeforeInstatiation;
		public event Action<object, IServiceProvider> AfterInstantiation;

		public BuildWithProviderInterceptor(IServiceProvider provider, ICollection<PersistentClass> classMappings) {
			this.classMappings = classMappings;
			this.provider = provider;
		}

		public override object Instantiate(string entityName, EntityMode entityMode, object id) {
			if (BeforeInstatiation != null) {
				BeforeInstatiation.Invoke(entityName, id, provider);
			}

			var pclass = classMappings.Where(c => c.EntityName == entityName).SingleOrDefault();
			var type = pclass.MappedClass;

			if (type == null || pclass.IdentifierProperty == null) {
				return null;
			}

			var entity = provider.GetService(type);
			pclass.IdentifierProperty.GetSetter(type).Set(entity, id);

			if (AfterInstantiation != null) {
				AfterInstantiation.Invoke(entity, provider);
			}

			return entity;
		}
	}
}