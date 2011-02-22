using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

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

		public string Action { get; set; }
		public string Controller { get; set; }
		public int Start { get { return pagedModel.Start; } }
		public int ActualEnd { get { return pagedModel.ActualEnd; } }
		public int End { get { return pagedModel.End; } }
		public int TotalPages { get { return pagedModel.TotalPages; } }
		public int CurrentPage { get { return pagedModel.CurrentPage; } }
		public bool HasPrevious { get { return pagedModel.HasPrevious; } }
		public bool HasNext { get { return pagedModel.HasNext; } }
		public int PageSize { get { return pagedModel.PageSize; } }
		public int TotalCount { get { return pagedModel.TotalCount; } }

		public Page GetPage(int pageNumber) {
			if (pageNumber < 1) {
				throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "Page number must be greater than 0");
			}

			var start = PageSize * (pageNumber - 1) + 1;
			var end = Math.Min(start + PageSize - 1, TotalCount);

			return new Page { Number = pageNumber, Start = start, End = end };
		}

		public IEnumerable<Page> Pages { get { return Enumerable.Range(1, TotalPages).Select(GetPage); } }

		public class Page {
			public int Start { get; set; }
			public int End { get; set; }
			public int Number { get; set; }
		}
	}

	/// <summary>
	/// Model for displaying data that should be paged
	/// </summary>
	public class PagedModel {
		public PagedModel() {
			Records = Enumerable.Empty<object>();
		}

		/// <summary>
		/// Gets or sets the current page number (starting from 1)
		/// </summary>
		public int CurrentPage { get; set; }

		/// <summary>
		/// Gets the starting point of the current page
		/// </summary>
		public int Start { get { return (CurrentPage - 1) * PageSize + 1; } }

		/// <summary>
		/// Gets the actual ending point of the current page
		/// </summary>
		public int ActualEnd { get { return Math.Min(TotalCount, CurrentPage * PageSize); } }
		
		/// <summary>
		/// Gets the theoretical ending point of the current page
		/// </summary>
		public int End { get { return CurrentPage * PageSize; } }

		/// <summary>
		/// Gets the total number of pages in the set
		/// </summary>
		public int TotalPages { get { return (int)Math.Ceiling((double)TotalCount / PageSize); } }

		/// <summary>
		/// Gets or sets the total number of records
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// Gets the page size
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// Gets whether or not a previous page is possible
		/// </summary>
		public bool HasPrevious { get { return CurrentPage > 1; } }

		/// <summary>
		/// Gets whether or not a next page is possible
		/// </summary>
		public bool HasNext { get { return CurrentPage * PageSize < TotalCount; } }

		/// <summary>
		/// Gets or sets filtered record set; <c>Count()</c> should be less than or equal to the <see cref="PageSize">page size</see>
		/// </summary>
		[NotNull]
		public IEnumerable<object> Records { get; set; }
	}

	/// <summary>
	/// Strongly-typed model for displaying data that is paged
	/// </summary>
	/// <typeparam name="T">The type of data to be paged</typeparam>
	public class PagedModel<T> : PagedModel {
		public PagedModel() {
			Records = Enumerable.Empty<T>();
		}

		[NotNull]
		public new IEnumerable<T> Records { get; set; }
	}
}