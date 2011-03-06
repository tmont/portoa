using NUnit.Framework;
using Portoa.Json;

namespace Portoa.Tests.Json {
	[TestFixture]
	public class JsonSerializationTests {

		[Test]
		public void Should_serialize_to_json() {
			var o = new JsonTester { Foo = "bar", Bar = new JsonTester { Baz = 2 }, Baz = 1 };
			var serialized = new JsonNetSerializer().Serialize(o);

			Assert.That(serialized, Is.EqualTo("{\"Foo\":\"bar\",\"Bar\":{\"Foo\":null,\"Bar\":null,\"Baz\":2},\"Baz\":1}"));
		}

		[Test]
		public void Should_deserialize_to_specific_type_from_json() {
			const string json = "{\"Foo\":\"bar\",\"Bar\":{\"Foo\":null,\"Bar\":null,\"Baz\":2},\"Baz\":1}";
			var deserialized = new JsonNetSerializer().Deserialize<JsonTester>(json);

			Assert.That(deserialized, Is.Not.Null);
			Assert.That(deserialized, Has.Property("Foo").EqualTo("bar"));
			Assert.That(deserialized, Has.Property("Baz").EqualTo(1));
			Assert.That(deserialized, Has.Property("Bar").Not.Null);
			Assert.That(deserialized, Has.Property("Bar").Property("Foo").Null);
			Assert.That(deserialized, Has.Property("Bar").Property("Bar").Null);
			Assert.That(deserialized, Has.Property("Bar").Property("Baz").EqualTo(2));
		}

		[Test]
		public void Should_deserialize_to_runtime_type_from_json() {
			const string json = "{\"Foo\":\"bar\",\"Bar\":{\"Foo\":null,\"Bar\":null,\"Baz\":2},\"Baz\":1}";
			var deserialized = (JsonTester)new JsonNetSerializer().Deserialize(json, typeof(JsonTester));

			Assert.That(deserialized, Is.Not.Null);
			Assert.That(deserialized, Has.Property("Foo").EqualTo("bar"));
			Assert.That(deserialized, Has.Property("Baz").EqualTo(1));
			Assert.That(deserialized, Has.Property("Bar").Not.Null);
			Assert.That(deserialized, Has.Property("Bar").Property("Foo").Null);
			Assert.That(deserialized, Has.Property("Bar").Property("Bar").Null);
			Assert.That(deserialized, Has.Property("Bar").Property("Baz").EqualTo(2));
		}

		public class JsonTester {
			public string Foo { get; set; }
			public JsonTester Bar { get; set; }
			public int Baz { get; set; }
		}

	}
}
