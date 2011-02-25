using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Portoa.Web.Util {

	public static class TagBuilderExtensions {
		/// <summary>
		/// Gets the HTML-safe string representation of the tag. Thanks, Microsoft, for making
		/// this method internal; it was really helpful.
		/// </summary>
		public static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode) {
			return MvcHtmlString.Create(tagBuilder.ToString(renderMode));
		}
	}

	public static class HtmlHelperExtensions {
		/// <summary>
		/// Creates a submit button with the specified button text, input name and HTML attributes
		/// </summary>
		/// <param name="buttonText">The text to display on the submit button</param>
		/// <param name="name">The name of the input</param>
		/// <param name="htmlAttributes">Anonymous object with HTML attribute values</param>
		public static MvcHtmlString Submit(this HtmlHelper htmlHelper, string buttonText = null, string name = null, object htmlAttributes = null) {
			var tagBuilder = new TagBuilder("input");

			tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes ?? new { }));
			tagBuilder.MergeAttribute("type", "submit", true);

			if (!string.IsNullOrEmpty(buttonText)) {
				tagBuilder.MergeAttribute("value", buttonText, true);
			}
			if (!string.IsNullOrEmpty(name)) {
				tagBuilder.MergeAttribute("name", name, true);
			}

			return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
		}

		/// <summary>
		/// Creates a label with the specified text for the specified reference tag
		/// </summary>
		/// <param name="labelText">The text/HTML to display inside the label</param>
		/// <param name="htmlFor">The reference ID of the labeled node (e.g. the value of the "for" attribute)</param>
		/// <param name="htmlAttributes">The HTML attributes</param>
		public static MvcHtmlString Label(this HtmlHelper htmlHelper, string labelText, string htmlFor, object htmlAttributes = null) {
			if (string.IsNullOrEmpty(htmlFor)) {
				throw new ArgumentException("The \"for\" attribute cannot be empty", "htmlFor");
			}

			var tagBuilder = new TagBuilder("label") {
				InnerHtml = labelText
			};

			tagBuilder.MergeAttribute("for", htmlFor);
			tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes ?? new { }));
			return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
		}

		/// <summary>
		/// Creates a label for the model property identified by <paramref name="expression"/>, applying
		/// the given <paramref name="htmlAttributes">HTML attributes</paramref>
		/// </summary>
		/// <param name="expression">A lambda expression identifying the model property to create a label for</param>
		/// <param name="htmlAttributes">Any extra HTML attributes that should be applied to the <c>&lt;label&gt;</c> tag</param> 
		public static MvcHtmlString LabelFor<T, TValue>(this HtmlHelper<T> html, Expression<Func<T, TValue>> expression, [NotNull]object htmlAttributes) {
			var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
			var htmlFieldName = ExpressionHelper.GetExpressionText(expression);

			var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
			if (string.IsNullOrEmpty(labelText)) {
				return MvcHtmlString.Empty;
			}

			var tagBuilder = new TagBuilder("label");
			tagBuilder.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
			tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			tagBuilder.SetInnerText(labelText);
			return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
		}

		/// <summary>
		/// Creates a button with the specified display text and the specified custom HTML attributes
		/// </summary>
		/// <param name="displayText">The text that the button will display</param>
		/// <param name="htmlAttributes">Any additional HTML attributes for the input tag</param>
		public static MvcHtmlString Button(this HtmlHelper htmlHelper, string displayText, object htmlAttributes = null) {
			var tagBuilder = new TagBuilder("input");

			tagBuilder.MergeAttribute("type", "button");
			tagBuilder.MergeAttribute("value", displayText ?? string.Empty);
			tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes ?? new { }));

			return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
		}
	}
}