namespace RazorForms.Options.Elements;

public class ButtonOptions : ComponentOptions, IButtonOptions
{
	/// <inheritdoc />
	public string? SubmitButtonClasses { get; set; }

	/// <inheritdoc />
	public string? ResetButtonClasses { get; set; }

	/// <inheritdoc />
	public string? DefaultButtonClasses { get; set; }
}