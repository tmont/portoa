using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Provides a mechanism for handling a single criterion and its values
	/// </summary>
	public interface IValueHandler {
		/// <summary>
		/// Converts the given <paramref name="criterion"/> into a predicate
		/// </summary>
		/// <remarks>This method should never return null.</remarks>
		/// <typeparam name="T">The type that the criterion is operating on</typeparam>
		/// <param name="criterion">The criterion passed in on the request</param>
		[NotNull]
		Expression<Func<T, bool>> CreateExpression<T>(Criterion criterion);
	}
}