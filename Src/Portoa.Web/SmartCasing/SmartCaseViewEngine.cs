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
	public class SmartCaseViewEngine : RazorViewEngine {
		private readonly ILogger logger;
		private static readonly IDictionary<string, string> deconstructedViewNameCache = new Dictionary<string, string>();

		public SmartCaseViewEngine(ILogger logger = null) {
			this.logger = logger ?? new NullLogger();

			AreaViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
			AreaMasterLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
			AreaPartialViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

			ViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
			MasterLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
			PartialViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

			FileExtensions = new[] {
                "cshtml"
            };
		}

		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath) {
			if (!deconstructedViewNameCache.ContainsKey(partialPath)) {
				throw new InvalidOperationException("Trying to create a view from partialPath that has not been deconstructed");
			}

			return new RazorView(
				controllerContext, 
				deconstructedViewNameCache[partialPath], 
				layoutPath: null, 
				runViewStartPages: true, 
				viewStartFileExtensions: FileExtensions, 
				viewPageActivator: ViewPageActivator
			);
		}

		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath) {
			if (!deconstructedViewNameCache.ContainsKey(viewPath)) {
				throw new InvalidOperationException("Trying to create a view from viewPath that has not been deconstructed");
			}

			return new RazorView(
				controllerContext, 
				deconstructedViewNameCache[viewPath], 
				layoutPath: masterPath, 
				runViewStartPages: true, 
				viewStartFileExtensions: FileExtensions, 
				viewPageActivator: ViewPageActivator
			);
		}

		protected override bool FileExists(ControllerContext controllerContext, string virtualPath) {
			if (!deconstructedViewNameCache.ContainsKey(virtualPath)) {
				var newPath = virtualPath
					.Split('/')
					.Select(SmartCaseConverter.ConvertFrom)
					.Implode(segment => segment, "/");

				deconstructedViewNameCache[virtualPath] = newPath;
				logger.Info("added deconstructed view to the cache: {0} -> {1}", virtualPath, deconstructedViewNameCache[virtualPath]);
			}

			return base.FileExists(controllerContext, deconstructedViewNameCache[virtualPath]);
		}
	}
}