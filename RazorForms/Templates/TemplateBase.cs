using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;

namespace RazorForms.Templates;

public abstract class TemplateBase<TModel> : RazorPage<TModel>
{
	/// <summary>
	/// Generates HTML attributes
	/// </summary>
	/// <param name="attributes">The attributes provided from the tag helper</param>
	/// <param name="action">An optional function to provide additional attribute generation</param>
	/// <returns>the HTML attributes to include in the markup. This should be rendered using <c>@Html.Raw()</c></returns>
	protected string GenerateAttributes(TagHelperAttributeList attributes, Action<StringBuilder>? action = null)
	{
		var sb = new StringBuilder();
		foreach (var a in attributes)
		{
			if (a == null)
			{
				continue;
			}

			if (a.Value == null)
			{
				sb.AppendWithLeadingSpace(a.Name);
			}
			else
			{
				sb.AppendWithLeadingSpace($"{a.Name}=\"{a.Value}\"");
			}
		}

		action?.Invoke(sb);

		return sb.ToString();
	}
}