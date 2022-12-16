using System.Collections.Generic;

namespace RazorForms;

public class ValidityMarkupModel : MarkupModel
{
	public IEnumerable<string> Errors { get; set; } = default!;
	public bool IsValid { get; set; }
	public bool IsInvalid { get; set; }
}