using Portoa.Css;
using dotless.Core;

namespace Portoa.Less {
	/// <summary>
	/// Compiles LESS source code into CSS
	/// </summary>
	public class LessCompiler : ICssCompiler {
		private readonly ILessEngine lessEngine;

		private class NoOpCacheStrategy : ICssCacheStrategy {
			public CssCacheItem Get(string key) {
				return null;
			}

			public void Set(string key, string compiled, CssCacheOptions options = null) { }
			public void Delete(string key) { }
		}

		public LessCompiler(ILessEngine lessEngine) {
			this.lessEngine = lessEngine;
		}

		public string Compile(string source, string fileName, CssCompilerOptions options = null) {
			if (options == null) {
				options = new CssCompilerOptions();
			}

			options.Cache = options.Cache ?? new NoOpCacheStrategy();

			var item = options.Cache.Get(fileName);
			string css;
			if (item == null) {
				css = lessEngine.TransformToCss(source, fileName);
				options.Cache.Set(fileName, css, new CssCacheOptions { Ttl = options.Ttl });
			}
			else {
				css = item.Css;
			}

			return css;
		}
	}
}