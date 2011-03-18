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
		public const string SortValueKey = "sort";
		public const string CriteriaValueKey = "criteria";
		

		/// <param name="idParser">Optional object to use to parse ids; default is <see cref="IdentityIdParser"/></param>
		/// <param name="parserFactory">Optional object to use to create criterion parsers; default is <see cref="DefaultCriterionParserFactory"/></param>
		public RestRequestModelBinder(IRestIdParser idParser = null, ICriterionParserFactory parserFactory = null) {
			ParserFactory = parserFactory ?? new DefaultCriterionParserFactory();
			IdParser = idParser ?? new IdentityIdParser();
		}

		protected ICriterionParserFactory ParserFactory { get; private set; }
		protected IRestIdParser IdParser { get; private set; }

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			return new CommandCenter(GetCommands()).Execute(controllerContext, bindingContext, new RestRequest());
		}

		/// <summary>
		/// Gets the commands to execute while the <see cref="ViewDataDictionary.ModelState"/> is valid.
		/// The default commands are <see cref="CriteriaCommand"/>,<see cref="IdCommand"/> and <see cref="SortCommand"/>.
		/// </summary>
		protected virtual IEnumerable<ICommand> GetCommands() {
			return new ICommand[] {
				new CriteriaCommand(ParserFactory),
				new IdCommand(IdParser),
				new SortCommand()
			};
		}

		/// <summary>
		/// Provides an interface for executing an action (such as parsing a value from the request) that
		/// populates the <c cref="RestRequest">model</c> in some way.
		/// </summary>
		protected interface ICommand {
			void Execute(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model);
		}

		private class CommandCenter {
			private readonly List<ICommand> commands = new List<ICommand>();

			public CommandCenter(IEnumerable<ICommand> commands) {
				this.commands.AddRange(commands);
			}

			public RestRequest Execute(ControllerContext controllerContext, ModelBindingContext bindingContext, RestRequest model) {
				foreach (var command in commands) {
					command.Execute(controllerContext, bindingContext, model);
					if (!controllerContext.IsValid()) {
						break;
					}
				}

				return model;
			}
		}

		/// <summary>
		/// Parses the value in the <see cref="ModelBindingContext.ValueProvider"/> keyed by <see cref="CriteriaValueKey"/>
		/// </summary>
		protected sealed class CriteriaCommand : ICommand {
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

		/// <summary>
		/// Parses the value in the <see cref="ModelBindingContext.ValueProvider"/> keyed by <see cref="SortValueKey"/>
		/// </summary>
		protected sealed class SortCommand : ICommand {
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

		/// <summary>
		/// Parses the id value in the <see cref="ModelBindingContext.ValueProvider"/> keyed by <see cref="IRestIdParser.IdKey"/>
		/// </summary>
		protected sealed class IdCommand : ICommand {
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