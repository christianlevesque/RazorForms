namespace RazorForms.Options;

public abstract class FormComponentOptions : ComponentOptions, IFormComponentOptions
{
	/// <inheritdoc />
	public string? InputBlockWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? LabelClasses { get; set; }

	/// <inheritdoc />
	public string? LabelTextHtmlWrapper { get; set; }

	/// <inheritdoc />
	public string? InputWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? InputClasses { get; set; }

	/// <inheritdoc />
	public bool? InputFirst { get; set; }

	/// <inheritdoc />
	public bool? RenderInputInsideLabel { get; set; }
}