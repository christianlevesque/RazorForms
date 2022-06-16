namespace RazorForms.Options;

public abstract class FormComponentWithValidationOptions : FormComponentOptions, IFormComponentWithValidationOptions
{
	/// <inheritdoc />
	public string? ComponentWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? ComponentWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? LabelValidClasses { get; set; }

	/// <inheritdoc />
	public string? LabelErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputErrorClasses { get; set; }

	/// <inheritdoc />
	public string? ErrorWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? ErrorClasses { get; set; }
}