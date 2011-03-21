using System;
using System.Linq;
using System.Web.Mvc;
using Portoa.Web.Rest.Configuration;
using Portoa.Web.Util;

namespace Portoa.Web.Rest {
	/// <summary>
	/// <see cref="IModelBinder"/> implementation for a <see cref="RestRequest"/>
	/// </summary>
	public class RestRequestModelBinder : IModelBinder {
		private readonly RestSectionHandler config;
		private readonly ICriterionHandlerFactory criterionHandlerFactory;

		public RestRequestModelBinder(RestSectionHandler config, ICriterionHandlerFactory criterionHandlerFactory) {
			this.config = config;
			this.criterionHandlerFactory = criterionHandlerFactory;
		}

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			return new CommandCenter(GetCriterionHandler)
				.Execute(controllerContext, bindingContext, new RestRequest());
		}

		private ICriterionHandler GetCriterionHandler(string resourceName, string criterionFieldName) {
			var specialCriterion = config.SpecialCriteria[criterionFieldName];
			var specialType = specialCriterion != null ? specialCriterion.HandlerType : null;
			if (specialType != null) {
				//special criterion overrides everything else
				return criterionHandlerFactory.Create(criterionFieldName, specialType);
			}

			var resource = config.Resources.FirstOrDefault(element => element.Key == resourceName);
			if (resource == null || resource.CriterionHandlers.Count == 0) {
				//return global criteria handler if no specific ones are registered
				return criterionHandlerFactory.Create(criterionFieldName, config.GlobalCriteriaHandlerType);
			}

			var registeredHandler = resource.CriterionHandlers.FirstOrDefault(element => element.Field == criterionFieldName);
			if (registeredHandler != null) {
				return criterionHandlerFactory.Create(criterionFieldName, registeredHandler.Type);
			}

			//nothing found, so this criterion should not be handled at all unless UseDefaultForOmittedFields is true
			return resource.CriterionHandlers.UseDefaultForOmittedFields
				? criterionHandlerFactory.Create(criterionFieldName, config.GlobalCriteriaHandlerType)
				: null;
		}

		private class CommandCenter {
			private readonly Func<string, string, ICriterionHandler> getCriterionHandler;

			public CommandCenter(Func<string, string, ICriterionHandler> getCriterionHandler) {
				this.getCriterionHandler = getCriterionHandler;
			}

			public RestRequest Execute(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model) {
				var valueProvider = bindingContext.ValueProvider as RestRequestValueProvider;
				if (valueProvider == null) {
					throw new RestConfigurationException(string.Format("Expected a {0} in the binding context", typeof(RestRequestValueProvider).Name));
				}

				model.ResourceName = (string)controllerContext.RouteData.Values["resource"];

				if (string.IsNullOrEmpty(model.ResourceName)) {
					throw new RestConfigurationException("Expected a non-empty route value for key \"resource\"");
				}

				foreach (var entry in valueProvider) {
					var handler = getCriterionHandler((string)controllerContext.RouteData.Values["resource"], entry.Key);
					if (handler == null) {
						continue;
					}

					try {
						handler.Execute(entry.Value.AttemptedValue, model);
					} catch (CriterionHandlingException e) {
						controllerContext.AddModelError(handler.Key, e.Message);
						break;
					}
				}

				return model;
			}
		}
	}
}