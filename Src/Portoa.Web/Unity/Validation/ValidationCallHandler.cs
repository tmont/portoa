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
			var results = input.Arguments.Cast<object>().SelectMany(entity => validator.Validate(entity));
			return !results.Any() 
				? getNext()(input, getNext) //no errors
				: input.CreateExceptionMethodReturn(new ValidationFailedException(results));
		}

		public int Order { get; set; }
	}
}