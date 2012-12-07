using System.IO;

namespace Portoa.Css {
	/// <summary>
	/// Compiles a source string (e.g. LESS) to CSS
	/// </summary>
	public interface ICssCompiler {
		/// <summary>
		/// Compiles <c>source</c> to CSS
		/// </summary>
		/// <param name="source">The raw source to compile to CSS</param>
		/// <param name="fileName">The originating file name</param>
		/// <param name="options">Optional options to modify the behavior of the compiler</param>
		/// <returns>CSS code</returns>
		string Compile(string source, string fileName, CssCompilerOptions options = null);
	}

	public static class CssCompilerExtensions {
		/// <summary>
		/// Reads from <c>fileName</c> and compiles its contents into CSS.
		/// Note that this method does no error handling, so all IO exceptions
		/// should be handled by the caller.
		/// </summary>
		/// <param name="fileName">The name of the file to compile</param>
		/// <returns>CSS code</returns>
		public static string Compile(this ICssCompiler compiler, string fileName) {
			return compiler.Compile(File.ReadAllText(fileName), fileName);
		}
	}
}