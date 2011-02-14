using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Persistence;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Call handler that wraps a method call in a unit of work
	/// </summary>
	/// <seealso cref="UnitOfWorkAttribute"/>
	/// <seealso cref="IUnitOfWork"/>
	public class UnitOfWorkCallHandler : ICallHandler {
		private readonly IUnitOfWork unitOfWork;

		public UnitOfWorkCallHandler(IUnitOfWork unitOfWork) {
			this.unitOfWork = unitOfWork;
		}

		public int Order { get; set; }

		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext) {
			using (unitOfWork.Start()) {
				var result = getNext()(input, getNext);
				

				if (result.Exception == null) {
					unitOfWork.Commit();
				} else {
					unitOfWork.Rollback();
				}

				return result;
			}
		}
	}
}