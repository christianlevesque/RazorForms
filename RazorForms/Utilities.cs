using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms;

public static class Utilities
{
	/// <summary>
	/// Appends a value to the given <see cref="StringBuilder"/> instance, including a leading space if the instance is not currently empty
	/// </summary>
	/// <param name="self">the <see cref="StringBuilder"/> instance</param>
	/// <param name="input">the value to append</param>
	public static void AppendWithLeadingSpace(this StringBuilder self, string? input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return;
		}

		var value = self.Length == 0 ? input : $" {input}";
		self.Append(value);
	}

	/// <summary>
	/// Generates the &lt;label&gt; inner text and surrounds it with an HTML tag if necessary
	/// </summary>
	/// <param name="options">the current tag helper's <see cref="FormComponentOptions"/></param>
	/// <param name="text">the inner text to display in the &lt;label&gt;</param>
	/// <returns></returns>
	public static string GenerateLabelText(FormComponentOptions options, string text)
	{
		return string.IsNullOrWhiteSpace(options.LabelTextHtmlWrapper)
			? text
			: $"<{options.LabelTextHtmlWrapper}>{text}</{options.LabelTextHtmlWrapper}>";
	}

	/// <summary>
	/// Gets the set of HTML attributes to pass to the &lt;input&gt;
	/// </summary>
	/// <param name="attributes">The full list of attributes passed to the tag helper</param>
	/// <returns>The attributes intended for the &lt;input&gt; tag</returns>
	public static TagHelperAttributeList GetInputAttributes(TagHelperAttributeList attributes)
	{
		var classAttribute = attributes.FirstOrDefault(a => a.Name == "class");
		var inputAttributes = attributes.Where(a => a.Name != "class").ToArray();
		attributes.Clear();

		if (classAttribute is not null)
		{
			attributes.Add(classAttribute);
		}

		return new TagHelperAttributeList(inputAttributes);
	}

	/// <summary>
	/// Merges two CSS strings while accounting that either or both may be null
	/// </summary>
	/// <param name="a">The first string to merge</param>
	/// <param name="b">The second string to merge</param>
	/// <returns></returns>
	public static string MergeCssStrings(string? a, string? b)
	{
		var aIsEmpty = string.IsNullOrWhiteSpace(a);
		var bIsEmpty = string.IsNullOrWhiteSpace(b);

		if (aIsEmpty)
		{
			return bIsEmpty ? string.Empty : b!;
		}

		if (bIsEmpty)
		{
			return aIsEmpty ? string.Empty : a!;
		}

		return $"{a} {b}";
	}
}