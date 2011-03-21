using System;
using System.Linq.Expressions;

namespace Portoa.Web.Rest {
	public sealed class DefaultOffsetHandler : NaturalIntegerValueHandler {
		protected override Expression<Func<RestRequest, int>> PropertyToSet { get { return request => request.Offset; } }
	}
}