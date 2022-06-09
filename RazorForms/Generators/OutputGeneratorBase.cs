using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public abstract class OutputGeneratorBase : IOutputGenerator
{
	protected bool IsValid { get; }
	protected bool IsInvalid { get; }
	protected Func<bool, HtmlEncoder, Task<TagHelperContent>> DefaultTagHelperContent { get; set; } = (a,b) => Task.Factory.StartNew<TagHelperContent>(() => new DefaultTagHelperContent());

	protected OutputGeneratorBase(bool isValid, bool isInvalid)
	{
		IsValid = isValid;
		IsInvalid = isInvalid;
	}

	/// <inheritdoc />
	public abstract Task<TagHelperOutput> GenerateOutput(TagHelperContext context, TextInputTagHelper helper, TagHelperAttributeList? attributes = null);

	protected abstract void ApplyWrapperClasses(TagHelperOutput output);

	protected virtual TagHelperOutput GenerateWrapper(TagHelperContent content)
	{
		var output = new TagHelperOutput("div", 
		                                 new TagHelperAttributeList(), 
		                                 (a, b) => Task.FromResult(content));
		ApplyWrapperClasses(output);
		return output;
	}

	/// <inheritdoc />
	public virtual string Render(TagHelperOutput output)
	{
		using var stream = new MemoryStream();
		using var writer = new StreamWriter(stream);
		output.WriteTo(writer, HtmlEncoder.Default);
		writer.Flush();
		stream.Position = 0;

		using var reader = new StreamReader(stream);
		return reader.ReadToEnd();
	}

	protected virtual void ApplyClasses(TagHelperOutput o,
	                                    string? className,
	                                    string? validClassName,
	                                    string? invalidClassName)
	{
		o.AddClass(className, HtmlEncoder.Default);
		if (IsValid)
		{
			o.AddClass(validClassName, HtmlEncoder.Default);
		}

		if (IsInvalid)
		{
			o.AddClass(invalidClassName, HtmlEncoder.Default);
		}
	}
}