using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Portoa.Search {
	/// <summary>
	/// Index builder that will build/update indexes for each of its child index builders
	/// </summary>
	public sealed class CompositeIndexBuilder : ISearchIndexBuilder<object> {
		private readonly IDictionary<Type, IList<object>> typeToBuilderMap = new Dictionary<Type, IList<object>>();

		/// <summary>
		/// Adds an index builder to the collection
		/// </summary>
		public CompositeIndexBuilder Add<T>([NotNull]ISearchIndexBuilder<T> indexBuilder) {
			var type = typeof(T);
			if (!typeToBuilderMap.ContainsKey(type)) {
				typeToBuilderMap[type] = new List<object>();
			}

			typeToBuilderMap[type].Add(indexBuilder);
			return this;
		}

		public void BuildIndex() {
			foreach (var kvp in typeToBuilderMap) {
				foreach (var indexBuilder in kvp.Value) {
					kvp.Key.GetMethod("BuildIndex").Invoke(indexBuilder, new object[0]);
				}
			}
		}

		public void UpdateIndex(object indexableObject) {
			foreach (var kvp in typeToBuilderMap) {
				foreach (var indexBuilder in kvp.Value) {
					kvp.Key.GetMethod("UpdateIndex", new[] { kvp.Key }).Invoke(indexBuilder, new[] { indexableObject });
				}
			}
		}
	}
}