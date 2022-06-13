using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public abstract class OutputGeneratorBase<TOptions> : IOutputGenerator<TOptions>
{
	protected TOptions Options { get; private set; }
	protected bool IsInitialized { get; private set; }

	protected Func<bool, HtmlEncoder, Task<TagHelperContent>> DefaultTagHelperContent { get; set; } = (a,b) => Task.Factory.StartNew<TagHelperContent>(() => new DefaultTagHelperContent());

	public void Init(TOptions options)
	{
		if (IsInitialized)
		{
			return;
		}

		Options = options;
		IsInitialized = true;
	}

	/// <inheritdoc />
	public abstract Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                     RazorFormsTagHelperBase helper,
	                                                     TagHelperAttributeList? attributes = null,
	                                                     TagHelperContent? childContent = null);

	protected abstract void ApplyWrapperClasses(TagHelperOutput output);

	protected virtual TagHelperOutput GenerateWrapper(TagHelperOutput content)
	{
		var output = new TagHelperOutput("div", 
		                                 new TagHelperAttributeList(), 
		                                 DefaultTagHelperContent)
		{
			TagMode = TagMode.StartTagAndEndTag
		};
		ApplyWrapperClasses(output);
		output.Content.AppendHtml(content);
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

	protected virtual void ApplyClasses(TagHelperOutput o, string? className)
	{
		AddClass(o, className);
	}

	protected virtual void ThrowIfNotInitialized()
	{
		if (!IsInitialized)
		{
			throw new InvalidOperationException("The output generator has not yet been initialized");
		}
	}

	protected virtual void AddClass(TagHelperOutput o, string? className)
	{
		if (string.IsNullOrWhiteSpace(className))
		{
			return;
		}

		var classes = className.Split(' ');

		foreach (var c in classes)
		{
			if (string.IsNullOrEmpty(c))
			{
				continue;
			}

			o.AddClass(c, HtmlEncoder.Default);
		}
	}
}