using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Portoa.Util;

namespace Portoa.Web.Results {
	/// <summary>
	/// <see cref="ActionResult"/> that decorates multiple action results
	/// and executes each of them in the order that they were added
	/// </summary>
	public class CompositeResult : ActionResult, IEnumerable<ActionResult> {
		private readonly IList<ActionResult> results = new List<ActionResult>();

		/// <summary>
		/// Adds a new <see cref="ActionResult"/>
		/// </summary>
		/// <param name="result">The <see cref="ActionResult"/> to add</param>
		public CompositeResult Add(ActionResult result) {
			results.Add(result);
			return this;
		}

		public override void ExecuteResult(ControllerContext context) {
			this.Walk(result => result.ExecuteResult(context));
		}

		public IEnumerator<ActionResult> GetEnumerator() {
			return results.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}