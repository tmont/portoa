using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Portoa.Web.Rest.Parser;
using Portoa.Web.Util;

namespace Portoa.Web.Rest {
	/// <summary>
	/// <see cref="IModelBinder"/> implementation for a <see cref="RestRequest"/>
	/// </summary>
	public class RestRequestModelBinder : IModelBinder {
		private readonly ICriterionParserFactory parserFactory;
		public const string SortValueKey = "sort";
		public const string CriteriaValueKey = "criteria";
		private readonly IRestIdParser idParser;

		/// <param name="idParser">Optional object to use to parse ids; default is <see cref="IdentityIdParser"/></param>
		/// <param name="parserFactory">Optional object to use to create criterion parsers; default is <see cref="DefaultCriterionParserFactory"/></param>
		public RestRequestModelBinder(IRestIdParser idParser = null, ICriterionParserFactory parserFactory = null) {
			this.parserFactory = parserFactory ?? new DefaultCriterionParserFactory();
			this.idParser = idParser ?? new IdentityIdParser();
		}

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			var model = new RestRequest();

			new CommandCenter()
				.Add(new CriteriaCommand(parserFactory))
				.Add(new IdCommand(idParser))
				.Add(new SortCommand())
				.Execute(controllerContext, bindingContext, model);

			return model;
		}

		private interface ICommand {
			void Execute(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model);
		}

		private class CommandCenter : ICommand {
			private readonly IList<ICommand> commands = new List<ICommand>();

			public CommandCenter Add(ICommand command) {
				commands.Add(command);
				return this;
			}

			public void Execute(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model) {
				foreach (var command in commands) {
					command.Execute(controllerContext, bindingContext, model);
					if (!controllerContext.IsValid()) {
						break;
					}
				}
			}
		}

		private class CriteriaCommand : ICommand {
			private readonly ICriterionParserFactory parserFactory;

			public CriteriaCommand(ICriterionParserFactory parserFactory) {
				this.parserFactory = parserFactory;
			}

			public void Execute(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model) {
				var valueResult = bindingContext.ValueProvider.GetValue(CriteriaValueKey);
				if (valueResult == null) {
					return;
				}

				try {
					model.Criteria = parserFactory.Create(valueResult.AttemptedValue).getCriteria();
				} catch (Exception e) {
					controllerContext.AddModelError(CriteriaValueKey, string.Format("An error occurred while parsing criteria: {0}", e.Message));
				}
			}
		}

		private class SortCommand : ICommand {
			public void Execute(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model) {
				var valueResult = bindingContext.ValueProvider.GetValue(SortValueKey);
				if (valueResult == null) {
					return;
				}

				var sortValues = (string[])valueResult.ConvertTo(typeof(string[]));

				if (sortValues == null || sortValues.Length == 0) {
					return;
				}

				foreach (var splitSortValue in sortValues.Select(sortValue => sortValue.Split('|'))) {
					var sortOrder = SortOrder.Ascending;
					if (splitSortValue.Length > 1) {
						switch (splitSortValue[1]) {
							case "descending":
							case "desc":
								sortOrder = SortOrder.Descending;
								break;
							case "ascending":
							case "asc":
								sortOrder = SortOrder.Ascending;
								break;
							default:
								controllerContext.AddModelError(SortValueKey, string.Format("The sort order \"{0}\" is invalid", splitSortValue[1]));
								return;
						}
					}

					model.SortInfo.Add(new SortGrouping { Field = splitSortValue[0], Order = sortOrder });
				}
			}
		}

		private class IdCommand : ICommand {
			private readonly IRestIdParser idParser;

			public IdCommand(IRestIdParser idParser) {
				this.idParser = idParser;
			}

			public void Execute(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model) {
				var valueResult = bindingContext.ValueProvider.GetValue(idParser.IdKey);
				var idValue = valueResult != null ? valueResult.AttemptedValue : string.Empty;

				if (idValue == idParser.FetchAllIdValue && idParser.AllowFetchAll) {
					return;
				}

				try {
					model.Criteria.Add(idParser.IdKey, new[] { idParser.ParseId(idValue) });
				} catch (InvalidIdException e) {
					controllerContext.AddModelError(idParser.IdKey, e.Message);
				}
			}
		}
	}
}