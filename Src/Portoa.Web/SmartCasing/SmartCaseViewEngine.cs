using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Portoa.Logging;
using Portoa.Util;

namespace Portoa.Web.SmartCasing {
	/// <summary>
	/// View engine for creating views that have been routed by a <see cref="SmartCaseRoute"/>
	/// </summary>
	public class SmartCaseViewEngine : WebFormViewEngine {
		private readonly ILogger logger;
		private static readonly SmartCaseConverter caseConverter = new SmartCaseConverter();
		private static readonly IDictionary<string, string> deconstructedViewNameCache = new Dictionary<string, string>();

		public SmartCaseViewEngine(ILogger logger) {
			this.logger = logger;
		}

		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath) {
			if (!deconstructedViewNameCache.ContainsKey(partialPath)) {
				throw new InvalidOperationException("Trying to create a view from partialPath that has not been deconstructed");
			}

			return new WebFormView(deconstructedViewNameCache[partialPath], null);
		}

		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath) {
			if (!deconstructedViewNameCache.ContainsKey(viewPath)) {
				throw new InvalidOperationException("Trying to create a view from viewPath that has not been deconstructed");
			}

			return new WebFormView(deconstructedViewNameCache[viewPath], masterPath);
		}

		protected override bool FileExists(ControllerContext controllerContext, string virtualPath) {
			if (!deconstructedViewNameCache.ContainsKey(virtualPath)) {
				var newPath = virtualPath
					.Split('/')
					.Select(segment => caseConverter.ConvertFrom(segment))
					.Implode(segment => segment, "/");

				deconstructedViewNameCache[virtualPath] = newPath;
				logger.Debug("added deconstructed view to the cache: {0} -> {1}", virtualPath, deconstructedViewNameCache[virtualPath]);
			}

			return base.FileExists(controllerContext, deconstructedViewNameCache[virtualPath]);
		}
	}
}