using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Portoa.Persistence;
using Portoa.Web.Rest;

namespace Portoa.Web.Tests.Rest {
	[TestFixture]
	public class RestServiceTests {
		[Test]
		public void Should_fetch_all_records() {
			var service = new RestService();
			var request = new RestRequest();
			var records = service.GetResource1s(request);

			Assert.That(records.Count(), Is.EqualTo(4));
		}

		[Test]
		public void Should_fetch_filtered_records() {
			var service = new RestService();
			var request = new RestRequest();
			request.Criteria.Add("whatever", "asdf");
			var records = service.GetResource1s(request);

			Assert.That(records.Count(), Is.EqualTo(1));
			var record = records.Single();
			Assert.That(record.Id, Is.EqualTo(2));
			Assert.That(record.Whatever, Is.EqualTo("asdf"));
		}

		[Test]
		public void Should_fetch_sorted_records() {
			var service = new RestService();
			var request = new RestRequest();
			request.SortInfo.Add(new SortGrouping { Field = "whatever", Order = SortOrder.Ascending });
			request.SortInfo.Add(new SortGrouping { Field = "id", Order = SortOrder.Descending });
			var records = service.GetResource1s(request);

			Assert.That(records.Count(), Is.EqualTo(4));

			Assert.That(records.ElementAt(0).Id, Is.EqualTo(2));
			Assert.That(records.ElementAt(0).Whatever, Is.EqualTo("asdf"));
			Assert.That(records.ElementAt(1).Id, Is.EqualTo(3));
			Assert.That(records.ElementAt(1).Whatever, Is.EqualTo("jkl;"));
			Assert.That(records.ElementAt(2).Id, Is.EqualTo(4));
			Assert.That(records.ElementAt(2).Whatever, Is.EqualTo("whatever"));
			Assert.That(records.ElementAt(3).Id, Is.EqualTo(1));
			Assert.That(records.ElementAt(3).Whatever, Is.EqualTo("whatever"));
		}

		[Test, ExpectedException(typeof(UnknownCriterionException))]
		public void Should_not_allow_requests_for_unknown_criterion() {
			var request = new RestRequest();
			request.Criteria.Add("foo", "foo");

			new RestService().GetResource1s(request);
		}

		[Test, ExpectedException(typeof(UnknownCriterionException))]
		public void Should_not_allow_sorting_by_unknown_field() {
			var request = new RestRequest();
			request.SortInfo.Add(new SortGrouping { Field = "foo" });

			new RestService().GetResource1s(request);
		}

		[Test]
		public void Should_use_custom_criterion_handler() {
			var request = new RestRequest();
			var criterion = new Criterion { FieldName = "whatever", Values = new[] { new CriterionFieldValue { RawValue = "asdf" } } };
			request.Criteria.Add(criterion);

			var handler = new Mock<IValueHandler>();
			handler
				.Setup(h => h.CreateExpression<Resource1>(criterion))
				.Returns(r => true)
				.Verifiable();

			var handlers = new Dictionary<string, IValueHandler> {
				{ "whatever", handler.Object }
			};

			new RestService().GetResource1s(request, handlers);

			handler.VerifyAll();
		}

		[Test, ExpectedException(typeof(UnknownCriterionException))]
		public void Should_explode_if_field_does_not_have_a_registered_handler() {
			var request = new RestRequest();
			request.Criteria.Add("whatever", "asdf");

			var handlers = new Dictionary<string, IValueHandler> {
				{ "asdf", new DefaultValueHandler() }
			};

			new RestService().GetResource1s(request, handlers);
		}

		#region nested types
		public class Resource1 : IDtoMappable<Resource1Dto> {
			public int Id { get; set; }
			public string Whatever { get; set; }
			public Resource1Dto ToDto() {
				return new Resource1Dto {
					Id = Id,
					Whatever = Whatever
				};
			}
		}

		public class Resource1Dto {
			public int Id { get; set; }
			public string Whatever { get; set; }
		}

		public class RestService : RestServiceBase {
			private static IQueryable<Resource1> Resource1s {
				get {
					return new[] {
						new Resource1 { Id = 1, Whatever = "whatever" },
						new Resource1 { Id = 2, Whatever = "asdf" },
						new Resource1 { Id = 3, Whatever = "jkl;" },
						new Resource1 { Id = 4, Whatever = "whatever" },
					}.AsQueryable();
				}
			}

			public IEnumerable<Resource1Dto> GetResource1s(RestRequest request) {
				return GetRecords<Resource1, Resource1Dto>(request, Resource1s);
			}

			public IEnumerable<Resource1Dto> GetResource1s(RestRequest request, IDictionary<string, IValueHandler> handlers) {
				return GetRecords<Resource1, Resource1Dto>(request, Resource1s, handlers);
			}

		}
		#endregion
	}
}