using System;

namespace Portoa.Css {
	/// <summary>
	/// Options for changing the default behavior of a <see cref="ICssCompiler"/>
	/// implementation
	/// </summary>
	public class CssCompilerOptions {
		public ICssCacheStrategy Cache { get; set; }
		public TimeSpan Ttl { get; set; }
	}
}