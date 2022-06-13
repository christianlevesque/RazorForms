using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.Generators;

public abstract class OutputGeneratorWithValidity<TOptions> : OutputGeneratorBase<TOptions>, IOutputGeneratorWithValidity<TOptions>
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