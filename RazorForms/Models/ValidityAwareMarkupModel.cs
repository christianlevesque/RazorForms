using System.Collections.Generic;
using RazorForms.Options;

namespace RazorForms.Models;

/// <summary>
/// The model used when rendering validity-aware content 
/// </summary>
public class ValidityAwareMarkupModel : MarkupModel
{
	/// <summary>
	/// Contains error messages to be rendered to the user
	/// </summary>
	public IEnumerable<string> Errors { get; set; } = default!;

	/// <summary>
	/// Whether the form element validation state is explicitly valid
	/// </summary>
	public bool IsValid { get; set; }

	/// <summary>
	/// Whether the form element validation state is explicitly invalid
	/// </summary>
	public bool IsInvalid { get; set; }
}