using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Portoa.Web.Rest {
	public static class ExpressionHelper {
		/// <summary>
		/// Composes two expressions into a binary expression using the given modifier. The parameters
		/// from the <paramref name="left"/> expression will be used, and the parameters from the
		/// <paramref name="right"/> expression will be assumed to be the same as the left's.
		/// </summary>
		/// <param name="left">The left hand side of the binary expression</param>
		/// <param name="right">The right hand side of the binary expression</param>
		/// <param name="booleanModifier">The boolean operator</param>
		public static Expression<Func<T, bool>> Compose<T>([CanBeNull]Expression<Func<T, bool>> left, [NotNull]Expression<Func<T, bool>> right, FieldValueModifier booleanModifier) {
			if (left == null) {
				return right;
			}

			switch (booleanModifier) {
				case FieldValueModifier.BooleanOr:
					return Expression.Lambda<Func<T, bool>>(Expression.MakeBinary(ExpressionType.Or, left.Body, right.Body), left.Parameters);
				default:
					return Expression.Lambda<Func<T, bool>>(Expression.MakeBinary(ExpressionType.AndAlso, left.Body, right.Body), left.Parameters);
			}
		}
	}
}