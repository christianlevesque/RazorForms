using System.Collections.Generic;
using RazorForms.Options;

namespace RazorForms.Models;

public class ValidityAwareMarkupModel : MarkupModel<FormComponentWithValidationOptions>
{
	public IEnumerable<string> Errors { get; set; } = default!;

	public bool IsValid { get; set; }

	public bool IsInvalid { get; set; }
}