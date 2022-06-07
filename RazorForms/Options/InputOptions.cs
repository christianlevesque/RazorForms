namespace RazorForms.Options;

public class InputOptions : OptionsBase, IInputOptions
{
	/// <inheritdoc />
	public string? ComponentWrapperClasses { get; set; }

	/// <inheritdoc />
	public bool? RemoveWrappers { get; set; }

	/// <inheritdoc />
	public string? ComponentWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? ComponentWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? LabelClasses { get; set; }

	/// <inheritdoc />
	public string? LabelValidClasses { get; set; }

	/// <inheritdoc />
	public string? LabelErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputClasses { get; set; }

	/// <inheritdoc />
	public string? InputValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputErrorClasses { get; set; }

	/// <inheritdoc />
	public string? ErrorWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? ErrorClasses { get; set; }

	/// <inheritdoc />
	public bool? InputFirst { get; set; }

	/// <inheritdoc />
	public virtual IFormComponentOptions Merge(IFormComponentOptions existing)
	{
		var newOptions = new InputOptions {
			ComponentWrapperClasses = MergeText(ComponentWrapperClasses, existing.ComponentWrapperClasses),
			ComponentWrapperValidClasses = MergeText(ComponentWrapperValidClasses, existing.ComponentWrapperValidClasses),
			ComponentWrapperErrorClasses = MergeText(ComponentWrapperErrorClasses, existing.ComponentWrapperErrorClasses),
			InputBlockWrapperClasses = MergeText(InputBlockWrapperClasses, existing.InputBlockWrapperClasses),
			InputBlockWrapperValidClasses = MergeText(InputBlockWrapperValidClasses, existing.InputBlockWrapperValidClasses),
			InputBlockWrapperErrorClasses = MergeText(InputBlockWrapperErrorClasses, existing.InputBlockWrapperErrorClasses),
			LabelWrapperClasses = MergeText(LabelWrapperClasses, existing.LabelWrapperClasses),
			LabelWrapperValidClasses = MergeText(LabelWrapperValidClasses, existing.LabelWrapperValidClasses),
			LabelWrapperErrorClasses = MergeText(LabelWrapperErrorClasses, existing.LabelWrapperErrorClasses),
			LabelClasses = MergeText(LabelClasses, existing.LabelClasses),
			LabelValidClasses = MergeText(LabelValidClasses, existing.LabelValidClasses),
			LabelErrorClasses = MergeText(LabelErrorClasses, existing.LabelErrorClasses),
			InputWrapperClasses = MergeText(InputWrapperClasses, existing.InputWrapperClasses),
			InputWrapperValidClasses = MergeText(InputWrapperValidClasses, existing.InputWrapperValidClasses),
			InputWrapperErrorClasses = MergeText(InputWrapperErrorClasses, existing.InputWrapperErrorClasses),
			InputClasses = MergeText(InputClasses, existing.InputClasses),
			InputValidClasses = MergeText(InputValidClasses, existing.InputValidClasses),
			InputErrorClasses = MergeText(InputErrorClasses, existing.InputErrorClasses),
			ErrorWrapperClasses = MergeText(ErrorWrapperClasses, existing.ErrorWrapperClasses),
			ErrorClasses = MergeText(ErrorClasses, existing.ErrorClasses)
		};

		if (existing.RemoveWrappers.HasValue)
		{
			newOptions.RemoveWrappers = existing.RemoveWrappers;
		}
		else
		{
			newOptions.RemoveWrappers = RemoveWrappers;
		}

        if (existing.InputFirst.HasValue)
        {
            newOptions.InputFirst = existing.InputFirst;
        }
        else
        {
            newOptions.InputFirst = InputFirst;
        }

		return newOptions;
	}
}