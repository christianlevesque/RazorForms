using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public abstract class OutputGeneratorBase<TOptions> : IOutputGenerator<TOptions>
{
	protected bool IsInitialized { get; private set; }
	protected bool IsValid { get; private set; }
	protected bool IsInvalid { get; private set; }
	protected TOptions Options { get; private set; }

	protected Func<bool, HtmlEncoder, Task<TagHelperContent>> DefaultTagHelperContent { get; set; } = (a,b) => Task.Factory.StartNew<TagHelperContent>(() => new DefaultTagHelperContent());

	public void Init(TOptions options, bool isValid, bool isInvalid)
	{
		if (IsInitialized)
		{
			return;
		}

		Options = options;
		IsValid = isValid;
		IsInvalid = isInvalid;
		IsInitialized = true;
	}

	/// <inheritdoc />
	public abstract Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                     RazorFormsTagHelperBase helper,
	                                                     TagHelperAttributeList? attributes = null,
	                                                     TagHelperContent? childContent = null);

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