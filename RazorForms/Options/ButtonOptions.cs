namespace RazorForms.Options;

public class ButtonOptions : OptionsBase, IButtonOptions
{
	/// <inheritdoc />
	public string? ComponentWrapperClasses { get; set; }

	/// <inheritdoc />
	public bool? RemoveWrappers { get; set; }

	/// <inheritdoc />
	public string? SubmitButtonClasses { get; set; }

	/// <inheritdoc />
	public string? ResetButtonClasses { get; set; }

	/// <inheritdoc />
	public string? DefaultButtonClasses { get; set; }

	/// <inheritdoc />
	public IFormButtonOptions Merge(IFormButtonOptions existingOptions)
	{
		var newOptions = new ButtonOptions
		{
			ComponentWrapperClasses = MergeText(ComponentWrapperClasses, existingOptions.ComponentWrapperClasses),
			SubmitButtonClasses = MergeText(SubmitButtonClasses, existingOptions.SubmitButtonClasses),
			ResetButtonClasses = MergeText(ResetButtonClasses, existingOptions.ResetButtonClasses),
			DefaultButtonClasses = MergeText(DefaultButtonClasses, existingOptions.DefaultButtonClasses)
		};

		if (existingOptions.RemoveWrappers.HasValue)
		{
			newOptions.RemoveWrappers = existingOptions.RemoveWrappers;
		}

		return newOptions;
	}
}