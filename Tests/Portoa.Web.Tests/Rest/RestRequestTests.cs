using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Portoa.Web.Rest;
using Portoa.Web.Util;

namespace Portoa.Web.Tests.Rest {
	[TestFixture, Ignore]
	public class RestRequestTests {

		public class AlwaysTrueIdParser : RestIdParserBase {
			protected override bool TryParse(string idValue, ref string id) {
				return true;
			}
		}

		private static ControllerContext CreateControllerContext() {
			return new ControllerContext(new RequestContext(), new Mock<ControllerBase> { CallBase = true }.Object);
		}

		[Test]
		public void Should_bind_RestRequest_with_single_valued_criteria() {
			var idParser = new AlwaysTrueIdParser();
			var binder = new RestRequestModelBinder(idParser);

			var values = new NameValueCollection {
				{ RestRequestModelBinder.IdValueKey, idParser.FetchAllIdValue },
				{ RestRequestModelBinder.CriteriaValueKey,  "foo/bar/baz/3" }
			};

			var bindingContext = new ModelBindingContext {
				ValueProvider = new NameValueCollectionValueProvider(values, CultureInfo.InvariantCulture)
			};

			var model = binder.BindModel(CreateControllerContext(), bindingContext);
			Assert.That(model, Is.TypeOf<RestRequest>());
			var request = (RestRequest)model;

			Assert.That(request.Criteria, Has.Count.EqualTo(2));

			Assert.That(request.Criteria["foo"], Is.Not.Null);
			Assert.That(request.Criteria["foo"].Values.Count(), Is.EqualTo(1));
			Assert.That(request.Criteria["foo"].Values.Single(), Is.EqualTo("bar"));

			Assert.That(request.Criteria["baz"], Is.Not.Null);
			Assert.That(request.Criteria["baz"].Values.Count(), Is.EqualTo(1));
			Assert.That(request.Criteria["baz"].Values.Single(), Is.EqualTo(3));
		}

		[Test]
		public void Should_bind_RestRequest_with_multi_valued_criteria() {
			var idParser = new AlwaysTrueIdParser();
			var binder = new RestRequestModelBinder(idParser);

			var values = new NameValueCollection {
				{ RestRequestModelBinder.IdValueKey, idParser.FetchAllIdValue },
				{ RestRequestModelBinder.CriteriaValueKey,  "foo/1,bar,bat" }
			};

			var bindingContext = new ModelBindingContext {
				ValueProvider = new NameValueCollectionValueProvider(values, CultureInfo.InvariantCulture)
			};

			var model = binder.BindModel(CreateControllerContext(), bindingContext);
			Assert.That(model, Is.TypeOf<RestRequest>());
			var request = (RestRequest)model;

			Assert.That(request.Criteria, Has.Count.EqualTo(1));

			Assert.That(request.Criteria["foo"], Is.Not.Null);
			Assert.That(request.Criteria["foo"].Values.Count(), Is.EqualTo(3));
			Assert.That(request.Criteria["foo"].Values.ElementAt(0), Is.EqualTo(1));
			Assert.That(request.Criteria["foo"].Values.ElementAt(1), Is.EqualTo("bar"));
			Assert.That(request.Criteria["foo"].Values.ElementAt(2), Is.EqualTo("bat"));
		}

		[Test]
		public void Should_bind_RestRequest_with_single_id() {
			var idParser = new AlwaysTrueIdParser();
			var binder = new RestRequestModelBinder(idParser);

			var values = new NameValueCollection {
				{ RestRequestModelBinder.IdValueKey, "5" }
			};

			var bindingContext = new ModelBindingContext {
				ValueProvider = new NameValueCollectionValueProvider(values, CultureInfo.InvariantCulture)
			};

			var model = binder.BindModel(CreateControllerContext(), bindingContext);
			Assert.That(model, Is.TypeOf<RestRequest>());
			var request = (RestRequest)model;

			Assert.That(request.Criteria, Has.Count.EqualTo(0));
			Assert.That(request.Id, Is.EqualTo("5"));
		}

		[Test]
		public void Should_add_model_error_for_invalid_id() {
			var idParser = new Mock<IRestIdParser>();
			idParser.Setup(parser => parser.ParseId("5")).Throws(new InvalidIdException("5"));
			var binder = new RestRequestModelBinder(idParser.Object);

			var values = new NameValueCollection {
				{ RestRequestModelBinder.IdValueKey, "5" }
			};

			var bindingContext = new ModelBindingContext {
				ValueProvider = new NameValueCollectionValueProvider(values, CultureInfo.InvariantCulture)
			};

			var controllerContext = CreateControllerContext();
			binder.BindModel(controllerContext, bindingContext);
			Assert.That(controllerContext.IsValid(), Is.False);
		}

		[Test]
		public void Should_parse_sort_info() {
			var idParser = new AlwaysTrueIdParser();
			var binder = new RestRequestModelBinder(idParser);

			var valueProvider = new DictionaryValueProvider<object>(
				new Dictionary<string, object> { 
					{ RestRequestModelBinder.SortValueKey, new[] { "foo|asc", "bar|desc", "baz|ascending", "bat|descending" } },
					{ RestRequestModelBinder.IdValueKey, idParser.FetchAllIdValue }
				},
				CultureInfo.InvariantCulture
			);

			var bindingContext = new ModelBindingContext {
				ValueProvider = valueProvider
			};

			var model = binder.BindModel(CreateControllerContext(), bindingContext);
			Assert.That(model, Is.TypeOf<RestRequest>());
			var request = (RestRequest)model;

			Assert.That(request.SortInfo, Has.Count.EqualTo(4));
			Assert.That(request.SortInfo[0].Field, Is.EqualTo("foo"));
			Assert.That(request.SortInfo[0].Order, Is.EqualTo(SortOrder.Ascending));
			Assert.That(request.SortInfo[1].Field, Is.EqualTo("bar"));
			Assert.That(request.SortInfo[1].Order, Is.EqualTo(SortOrder.Descending));
			Assert.That(request.SortInfo[2].Field, Is.EqualTo("baz"));
			Assert.That(request.SortInfo[2].Order, Is.EqualTo(SortOrder.Ascending));
			Assert.That(request.SortInfo[3].Field, Is.EqualTo("bat"));
			Assert.That(request.SortInfo[3].Order, Is.EqualTo(SortOrder.Descending));
		}

		[Test]
		public void Should_add_model_error_for_invalid_sort_value() {
			var idParser = new AlwaysTrueIdParser();
			var binder = new RestRequestModelBinder(idParser);

			var valueProvider = new DictionaryValueProvider<object>(
				new Dictionary<string, object> { 
					{ RestRequestModelBinder.SortValueKey, new[] { "foo|asdf" } },
					{ RestRequestModelBinder.IdValueKey, idParser.FetchAllIdValue }
				},
				CultureInfo.InvariantCulture
			);

			var bindingContext = new ModelBindingContext {
				ValueProvider = valueProvider
			};

			var controllerContext = CreateControllerContext();
			var model = binder.BindModel(controllerContext, bindingContext);
			Assert.That(model, Is.TypeOf<RestRequest>());

			Assert.That(controllerContext.IsValid(), Is.False);
		}
	}
}