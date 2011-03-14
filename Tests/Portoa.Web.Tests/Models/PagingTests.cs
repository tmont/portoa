using System;
using System.Linq;
using NUnit.Framework;
using Portoa.Web.Models;

namespace Portoa.Web.Tests.Models {
	[TestFixture]
	public class PagingTests {
		[Test]
		public void Should_correctly_calculate_values_on_first_page() {
			var model = new PagedModel {
				CurrentPage = 1,
				PageSize = 20,
				TotalCount = 70
			};

			Assert.That(model.Start, Is.EqualTo(1));
			Assert.That(model.End, Is.EqualTo(20));
			Assert.That(model.ActualEnd, Is.EqualTo(20));
			Assert.That(model.HasNext, Is.True);
			Assert.That(model.HasPrevious, Is.False);
			Assert.That(model.TotalPages, Is.EqualTo(4));

			var menuModel = model.GetMenuModel();
			Assert.That(menuModel.ActualEnd, Is.EqualTo(20));
			Assert.That(menuModel.CurrentPage, Is.EqualTo(1));
			Assert.That(menuModel.CurrentPageIsValid, Is.True);
			Assert.That(menuModel.End, Is.EqualTo(20));
			Assert.That(menuModel.HasNext, Is.True);
			Assert.That(menuModel.HasPrevious, Is.False);
			Assert.That(menuModel.PageSize, Is.EqualTo(20));
			Assert.That(menuModel.ShouldShowMenu, Is.True);
			Assert.That(menuModel.Start, Is.EqualTo(1));
			Assert.That(menuModel.TotalCount, Is.EqualTo(70));
			Assert.That(menuModel.TotalPages, Is.EqualTo(4));
		}

		[Test]
		public void Should_correctly_calculate_values_on_middle_page() {
			var model = new PagedModel {
				CurrentPage = 3,
				PageSize = 20,
				TotalCount = 70
			};

			Assert.That(model.Start, Is.EqualTo(41));
			Assert.That(model.End, Is.EqualTo(60));
			Assert.That(model.ActualEnd, Is.EqualTo(60));
			Assert.That(model.HasNext, Is.True);
			Assert.That(model.HasPrevious, Is.True);
			Assert.That(model.TotalPages, Is.EqualTo(4));

			var menuModel = model.GetMenuModel();
			Assert.That(menuModel.ActualEnd, Is.EqualTo(60));
			Assert.That(menuModel.CurrentPage, Is.EqualTo(3));
			Assert.That(menuModel.CurrentPageIsValid, Is.True);
			Assert.That(menuModel.End, Is.EqualTo(60));
			Assert.That(menuModel.HasNext, Is.True);
			Assert.That(menuModel.HasPrevious, Is.True);
			Assert.That(menuModel.PageSize, Is.EqualTo(20));
			Assert.That(menuModel.ShouldShowMenu, Is.True);
			Assert.That(menuModel.Start, Is.EqualTo(41));
			Assert.That(menuModel.TotalCount, Is.EqualTo(70));
			Assert.That(menuModel.TotalPages, Is.EqualTo(4));
		}

		[Test]
		public void Should_correctly_calculate_values_on_last_page() {
			var model = new PagedModel {
				CurrentPage = 4,
				PageSize = 20,
				TotalCount = 70
			};

			Assert.That(model.Start, Is.EqualTo(61));
			Assert.That(model.End, Is.EqualTo(80));
			Assert.That(model.ActualEnd, Is.EqualTo(70));
			Assert.That(model.HasNext, Is.False);
			Assert.That(model.HasPrevious, Is.True);
			Assert.That(model.TotalPages, Is.EqualTo(4));

			var menuModel = model.GetMenuModel();
			Assert.That(menuModel.ActualEnd, Is.EqualTo(70));
			Assert.That(menuModel.CurrentPage, Is.EqualTo(4));
			Assert.That(menuModel.CurrentPageIsValid, Is.True);
			Assert.That(menuModel.End, Is.EqualTo(80));
			Assert.That(menuModel.HasNext, Is.False);
			Assert.That(menuModel.HasPrevious, Is.True);
			Assert.That(menuModel.PageSize, Is.EqualTo(20));
			Assert.That(menuModel.ShouldShowMenu, Is.True);
			Assert.That(menuModel.Start, Is.EqualTo(61));
			Assert.That(menuModel.TotalCount, Is.EqualTo(70));
			Assert.That(menuModel.TotalPages, Is.EqualTo(4));
		}

		[Test]
		public void Should_detect_invalid_page() {
			var model = new PagedModel {
				CurrentPage = 12,
				PageSize = 20,
				TotalCount = 70
			};

			Assert.That(model.GetMenuModel().CurrentPageIsValid, Is.False);
		}

		[Test]
		public void Should_not_show_menu_when_total_count_is_less_than_page_size() {
			var model = new PagedModel {
				CurrentPage = 1,
				PageSize = 20,
				TotalCount = 10
			};

			Assert.That(model.GetMenuModel().ShouldShowMenu, Is.False);
		}

		[Test]
		public void Should_enumerate_pages_properly() {
			var model = new PagedModel {
				CurrentPage = 1,
				PageSize = 15,
				TotalCount = 40
			};

			var pages = model.GetMenuModel().Pages.ToArray();
			Assert.That(pages, Has.Length.EqualTo(3));

			Assert.That(pages[0].Start, Is.EqualTo(1));
			Assert.That(pages[0].End, Is.EqualTo(15));
			Assert.That(pages[0].Number, Is.EqualTo(1));

			Assert.That(pages[1].Start, Is.EqualTo(16));
			Assert.That(pages[1].End, Is.EqualTo(30));
			Assert.That(pages[1].Number, Is.EqualTo(2));

			Assert.That(pages[2].Start, Is.EqualTo(31));
			Assert.That(pages[2].End, Is.EqualTo(40));
			Assert.That(pages[2].Number, Is.EqualTo(3));
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Should_not_be_able_to_retrieve_out_of_range_page() {
			var model = new PagedModel {
				CurrentPage = 1,
				PageSize = 15,
				TotalCount = 40
			};

			model.GetMenuModel().GetPage(0);
		}
	}
}