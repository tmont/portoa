using Portoa.Util;

namespace Portoa.Testing {
	public class EmbeddedResourceScript<T> : ISqlScript where T : class {
		private readonly string resourceName;

		public EmbeddedResourceScript(string resourceName) {
			this.resourceName = resourceName;
		}

		public string Name { get { return string.Format("value from resource {0}.{1}", typeof(T).GetFriendlyName(), resourceName); } }
		public string Content { get { return (string)typeof(T).GetProperty(resourceName).GetValue(null, null); } }
	}
}