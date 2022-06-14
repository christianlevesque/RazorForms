using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorForms.TagHelpers;

namespace RazorForms.Generators;

public abstract class ValidityAwareOutputGenerator<TOptions> : OutputGeneratorBase<TOptions>, IValidityAwareOutputGenerator<TOptions>
{
	protected bool IsValid { get; private set; }
	protected bool IsInvalid { get; private set; }

	public void Init(TOptions options, bool isValid, bool isInvalid)
	{
		if (IsInitialized)
		{
			return;
		}

		IsValid = isValid;
		IsInvalid = isInvalid;

		Init(options);
	}

	/// <inheritdoc />
	public abstract Task<TagHelperOutput> GenerateOutput(TagHelperContext context,
	                                                     RazorFormsTagHelperBase helper,
	                                                     TagHelperAttributeList? attributes = null,
	                                                     TagHelperContent? childContent = null);

	protected virtual void ApplyClasses(TagHelperOutput o,
	                                    string? className,
	                                    string? validClassName,
	                                    string? invalidClassName)
	{
		ApplyClasses(o, className);

		if (IsValid)
		{
			AddClass(o, validClassName);
		}

		if (IsInvalid)
		{
			AddClass(o, invalidClassName);
		}
	}
}