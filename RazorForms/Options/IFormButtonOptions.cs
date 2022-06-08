namespace RazorForms.Options;

public interface IFormButtonOptions : IComponentOptions
{
	/// <summary>
	/// CSS classes applied to the submit button
	/// </summary>
	string? SubmitButtonClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the 
	/// </summary>
	string? ResetButtonClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the generic button (<c>&lt;button type="button"&gt;</c>)
	/// </summary>
	string? DefaultButtonClasses { get; set; }
}