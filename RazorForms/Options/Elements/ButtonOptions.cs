namespace RazorForms.Options.Elements;

public class ButtonOptions : IButtonOptions
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
}