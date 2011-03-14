using System;
using System.Collections.Generic;
using System.Linq;

namespace Portoa.Web.Models {
	/// <summary>
	/// Model for displaying a paging menu
	/// </summary>
	public class PagingMenuModel {
		private readonly PagedModel pagedModel;

		/// <param name="pagedModel">The model used for displaying the paged data</param>
		public PagingMenuModel(PagedModel pagedModel) {
			this.pagedModel = pagedModel;
		}

		/// <summary>
		/// Gets or sets action to navigate to when going to the next or previous page
		/// </summary>
		public string Action { get; set; }
		/// <summary>
		/// Gets or sets the controller to navigate to when going to the next or previous page
		/// </summary>
		public string Controller { get; set; }

		/// <summary>
		/// Gets the starting point of the current page
		/// </summary>
		public int Start { get { return pagedModel.Start; } }

		/// <summary>
		/// Gets the actual ending point of the current page
		/// </summary>
		public int ActualEnd { get { return pagedModel.ActualEnd; } }

		/// <summary>
		/// Gets the theoretical ending point of the current page
		/// </summary>
		public int End { get { return pagedModel.End; } }

		/// <summary>
		/// Gets the total number of pages in the set
		/// </summary>
		public int TotalPages { get { return pagedModel.TotalPages; } }

		/// <summary>
		/// Gets the current page number
		/// </summary>
		public int CurrentPage { get { return pagedModel.CurrentPage; } }

		/// <summary>
		/// Gets whether or not a previous page is possible
		/// </summary>
		public bool HasPrevious { get { return pagedModel.HasPrevious; } }

		/// <summary>
		/// Gets whether or not a next page is possible
		/// </summary>
		public bool HasNext { get { return pagedModel.HasNext; } }

		/// <summary>
		/// Gets the page size
		/// </summary>
		public int PageSize { get { return pagedModel.PageSize; } }

		/// <summary>
		/// Gets the total number of records
		/// </summary>
		public int TotalCount { get { return pagedModel.TotalCount; } }

		/// <summary>
		/// Gets whether or not the menu should be shown
		/// </summary>
		public bool ShouldShowMenu { get { return HasNext || HasPrevious; } }

		/// <summary>
		/// Gets whether or not the current page is a valid page number for the given data
		/// </summary>
		public bool CurrentPageIsValid { get { return CurrentPage > 0 && CurrentPage <= TotalPages; } }

		/// <summary>
		/// Gets the data needed to display a link to a certain page
		/// </summary>
		/// <param name="pageNumber">The number of the page to retrieve</param>
		public Page GetPage(int pageNumber) {
			if (pageNumber < 1) {
				throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "Page number must be greater than zero");
			}

			var start = PageSize * (pageNumber - 1) + 1;
			var end = Math.Min(start + PageSize - 1, TotalCount);

			return new Page { Number = pageNumber, Start = start, End = end };
		}

		/// <summary>
		/// Gets the data needed to display a link to all the pages
		/// </summary>
		public IEnumerable<Page> Pages { get { return Enumerable.Range(1, TotalPages).Select(GetPage); } }

		/// <summary>
		/// Represents the data needed to show a link to a page
		/// </summary>
		public class Page {
			/// <summary>
			/// Gets or sets the starting point of this page
			/// </summary>
			public int Start { get; set; }

			/// <summary>
			/// Gets or sets the ending point of this page
			/// </summary>
			public int End { get; set; }

			/// <summary>
			/// Gets or sets the page number
			/// </summary>
			public int Number { get; set; }
		}
	}
}