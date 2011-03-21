using System;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Interface for creating <see cref="ICriterionHandler"/>
	/// </summary>
	public interface ICriterionHandlerFactory {
		/// <summary>
		/// Creates a criterion handler for the given <paramref name="type"/>
		/// </summary>
		/// <param name="key">The request key</param>
		/// <param name="type">The type of criterion handler to create</param>
		ICriterionHandler Create(string key, Type type);
	}
}