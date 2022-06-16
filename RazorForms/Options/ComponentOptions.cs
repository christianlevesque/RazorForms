namespace RazorForms.Options;

public class ComponentOptions : IComponentOptions
{
	/// <inheritdoc />
	public string? ComponentWrapperClasses { get; set; }

	/// <inheritdoc />
	public bool? RemoveWrappers { get; set; }
}