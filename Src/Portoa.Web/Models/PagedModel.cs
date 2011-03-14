using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Portoa.Web.Models {

	/// <summary>
	/// Strongly-typed model for displaying data that should be paged
	/// </summary>
	/// <typeparam name="T">The type of data to be paged</typeparam>
	public class PagedModel<T> : PagedModel {
		public PagedModel() {
			Records = Enumerable.Empty<T>();
		}

		/// <summary>
		/// Gets or sets filtered record set; <c>Count()</c> should be less than or equal to the <see cref="PagedModel.PageSize">page size</see>
		/// </summary>
		[NotNull]
		public new IEnumerable<T> Records { get; set; }
	}

	/// <summary>
	/// Model for displaying data that should be paged
	/// </summary>
	public class PagedModel {
		public PagedModel() {
			Records = Enumerable.Empty<object>();
		}

		/// <summary>
		/// Gets a menu model for the current page
		/// </summary>
		public PagingMenuModel GetMenuModel() {
			return new PagingMenuModel(this);
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
}