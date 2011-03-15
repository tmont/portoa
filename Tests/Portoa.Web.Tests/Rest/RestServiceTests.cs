using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using Portoa.Persistence;
using Portoa.Web.Rest;

namespace Portoa.Web.Tests.Rest {
	[TestFixture]
	public class RestServiceTests {

		[Test, Ignore]
		public void Should_do_stuff() {
			Expression<Func<Resource1, object>> expression = resource => resource.Id;

			var propertyType = ((PropertyInfo)((MemberExpression)expression.Body).Member).PropertyType;
		}

		[Test]
		public void Should_fetch_all_records() {
			var service = new RestService();
			var request = new RestRequest { FetchAll = true };
			var records = service.GetResource1s(request);

			Assert.That(records.Count(), Is.EqualTo(4));
		}

		[Test]
		public void Should_fetch_filtered_records() {
			var service = new RestService();
			var request = new RestRequest { FetchAll = true };
			request.Criteria.Add("whatever", new[] { "asdf" });
			var records = service.GetResource1s(request);

			Assert.That(records.Count(), Is.EqualTo(1));
			var record = records.Single();
			Assert.That(record.Id, Is.EqualTo(2));
			Assert.That(record.Whatever, Is.EqualTo("asdf"));
		}

		[Test]
		public void Should_fetch_sorted_records() {
			var service = new RestService();
			var request = new RestRequest { FetchAll = true };
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

		[Test]
		public void Should_fetch_single_record_by_id() {
			var service = new RestService();
			var request = new RestRequest { Id = "1" };
			var records = service.GetResource1s(request);

			Assert.That(records.Count(), Is.EqualTo(1));

			var record = records.Single();
			Assert.That(record.Id, Is.EqualTo(1));
			Assert.That(record.Whatever, Is.EqualTo("whatever"));
		}

		public class Resource1 : IDtoMappable<Resource1Dto> {
			public int Id { get; set; }
			public string Whatever { get; set; }
			public Resource1Dto ToDto() {
				return new Resource1Dto {
					Id = Id,
					Whatever = Whatever
				};
			}

			public class WhateverCriterionHandler : CriterionHandler<Resource1> {
				protected override Func<Resource1, bool> HandleString(string value) {
					return resource1 => resource1.Whatever == value;
				}
			}
		}

		public class Resource1Dto {
			public int Id { get; set; }
			public string Whatever { get; set; }
		}

		public class RestService : RestServiceBase {
			private readonly IDictionary<string, CriterionHandler<Resource1>> resource1Handlers = new Dictionary<string, CriterionHandler<Resource1>> {
				{ "whatever", new Resource1.WhateverCriterionHandler() }
			};

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
				return GetRecords<Resource1, Resource1Dto, int>(request, Resource1s, resource1Handlers, resource1 => resource1.Id);
			}
		}
	}
}