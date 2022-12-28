using RazorForms.Options;

namespace RazorForms.Materialize.Options;

public class FormattableOptions : ValidityAwareFormComponentOptions
{
	/// <summary>
	/// A default value to use for the <c>asp-format</c> attribute if none is specified
	/// </summary>
	public string? Format { get; set; }
}