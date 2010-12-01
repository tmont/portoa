using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portoa.Web.Util {
	public static class HtmlHelperExtensions {
		/// <summary>
		/// Creates a submit button with the specified button text, input name and HTML attributes
		/// </summary>
		/// <param name="buttonText">The text to display on the submit button</param>
		/// <param name="name">The name of the input</param>
		/// <param name="htmlAttributes">Anonymous object with HTML attribute values</param>
		public static string Submit(this HtmlHelper htmlHelper, string buttonText = null, string name = null, object htmlAttributes = null) {
			var tagBuilder = new TagBuilder("input");

			tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes ?? new { }));
			tagBuilder.MergeAttribute("type", "submit", true);

			if (!string.IsNullOrEmpty(buttonText)) {
				tagBuilder.MergeAttribute("value", buttonText, true);
			}
			if (!string.IsNullOrEmpty(name)) {
				tagBuilder.MergeAttribute("name", name, true);
			}

			return tagBuilder.ToString(TagRenderMode.SelfClosing);
		}

		/// <summary>
		/// Creates a label with the specified text for the specified reference tag
		/// </summary>
		/// <param name="labelText">The text/HTML to display inside the label</param>
		/// <param name="htmlFor">The reference ID of the labeled node (e.g. the value of the "for" attribute)</param>
		/// <param name="htmlAttributes">The HTML attributes</param>
		public static string Label(this HtmlHelper htmlHelper, string labelText, string htmlFor, object htmlAttributes = null) {
			if (string.IsNullOrEmpty(htmlFor)) {
				throw new ArgumentException("The \"for\" attribute cannot be empty", "htmlFor");
			}

			var tagBuilder = new TagBuilder("label") {
				InnerHtml = labelText
			};

			tagBuilder.MergeAttribute("for", htmlFor);
			tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes ?? new { }));
			return tagBuilder.ToString(TagRenderMode.Normal);
		}

		/// <summary>
		/// Creates a button with the specified display text and the specified custom HTML attributes
		/// </summary>
		/// <param name="displayText">The text that the button will display</param>
		/// <param name="htmlAttributes">Any additional HTML attributes for the input tag</param>
		public static string Button(this HtmlHelper htmlHelper, string displayText, object htmlAttributes = null) {
			return htmlHelper.Button(displayText, new RouteValueDictionary(htmlAttributes ?? new { }));
		}

		/// <summary>
		/// Creates a button with the specified display text and the specified custom HTML attributes
		/// </summary>
		/// <param name="displayText">The text that the button will display</param>
		/// <param name="htmlAttributes">Any additional HTML attributes for the input tag</param>
		public static string Button(this HtmlHelper htmlHelper, string displayText, RouteValueDictionary htmlAttributes) {
			var tagBuilder = new TagBuilder("input");

			tagBuilder.MergeAttribute("type", "button");
			tagBuilder.MergeAttribute("value", displayText ?? string.Empty);
			tagBuilder.MergeAttributes(htmlAttributes);

			return tagBuilder.ToString(TagRenderMode.SelfClosing);
		}

	}
}