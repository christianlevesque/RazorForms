using System.ComponentModel;

namespace RazorForms;

public enum ButtonType
{
	[Description("button")] Default,
	[Description("submit")] Submit,
	[Description("reset")] Reset
}