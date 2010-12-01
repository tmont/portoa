using System;

namespace Portoa.Testing {
	public class DeferredScript : ISqlScript {
		private readonly Func<string> function;

		public DeferredScript(Func<string> function) {
			this.function = function;
		}

		public string Name { get; set; }
		public string Content { get { return function(); } }
	}
}