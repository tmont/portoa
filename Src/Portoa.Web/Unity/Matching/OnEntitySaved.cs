using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Persistence;
using Portoa.Util;

namespace Portoa.Web.Unity.Matching {
	/// <summary>
	/// Matching rule that matches the <see cref="IRepository{T,TId}.Save"/> method
	/// </summary>
	public class OnEntitySaved : IMatchingRule {
		private static readonly Type repoType = typeof(IRepository<,>);

		public bool Matches(MethodBase member) {
			var declaringType = member.DeclaringType;
			if (!declaringType.IsAssignableToGenericType(repoType)) {
				return false;
			}

			var genericArguments = declaringType.GetGenericArguments();
			if (!declaringType.IsGenericType || genericArguments.Length != 2) {
				return false; 
			}

			return repoType.MakeGenericType(genericArguments).GetMethod("Save") == member;
		}
	}
}