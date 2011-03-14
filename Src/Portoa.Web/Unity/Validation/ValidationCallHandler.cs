using System.Linq;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Validation;

namespace Portoa.Web.Unity.Validation {
	/// <summary>
	/// Call handler that validates entities
	/// </summary>
	public class ValidationCallHandler : ICallHandler {
		private readonly IEntityValidator validator;

		public ValidationCallHandler(IEntityValidator validator) {
			this.validator = validator;
		}

		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext) {
			var entity = input.Arguments[0];
			var results = validator.Validate(entity);
			if (!results.Any()) {
				return getNext()(input, getNext);
			}

			return input.CreateExceptionMethodReturn(new ValidationFailedException(results));
		}

		public int Order { get; set; }
	}
}