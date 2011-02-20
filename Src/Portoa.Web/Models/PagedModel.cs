using System;
using System.Collections.Generic;

namespace Portoa.Web.Models {
	/// <summary>
	/// Model for displaying data that is paged
	/// </summary>
	/// <typeparam name="T">The type of data to be paged</typeparam>
	public class PagedModel<T> {
		/// <summary>
		/// The starting point of the records (e.g. the number you <c>Skip()</c>'d)
		/// </summary>
		public int Start { get; set; }

		/// <summary>
		/// The ending point of the records (e.g. the number you <c>Take()</c>'d)
		/// </summary>
		public int End { get; set; }

		/// <summary>
		/// The total number of records
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// The page size
		/// </summary>
		public int PageSize { get { return End - Start + 1; } }

		/// <summary>
		/// The starting point of the next page
		/// </summary>
		/// <seealso cref="NextEnd"/>
		public int NextStart { get { return End + 1; } }

		/// <summary>
		/// The ending point of the next page
		/// </summary>
		/// <seealso cref="NextStart"/>
		public int NextEnd { get { return End + PageSize; } }

		/// <summary>
		/// The starting point of the previous page (<c>0</c> is the minimum)
		/// </summary>
		/// <seealso cref="PreviousEnd"/>
		public int PreviousStart { get { return Math.Max(0, Start - PageSize); } }

		/// <summary>
		/// The ending point of the previous page (<c>0</c> is the minimum)
		/// </summary>
		/// <seealso cref="PreviousStart"/>
		public int PreviousEnd { get { return Math.Max(PageSize - 1, End - PageSize); } }

		/// <summary>
		/// Gets whether or not a previous page is possible
		/// </summary>
		public bool HasPrevious { get { return Start > 0; } }

		/// <summary>
		/// The filtered record set; <c>Count()</c> should be less than or equal to the <see cref="PageSize">page size</see>
		/// </summary>
		public IEnumerable<T> Records { get; set; }
	}
}