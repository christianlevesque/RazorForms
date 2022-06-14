namespace RazorForms.Options.Inputs;

public class CheckInputOptions : ICheckInputOptions
{
	/// <inheritdoc />
	public string? ComponentWrapperClasses { get; set; }

	/// <inheritdoc />
	public bool? RemoveWrappers { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? LabelClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? InputClasses { get; set; }

	/// <inheritdoc />
	public bool? InputFirst { get; set; }
}