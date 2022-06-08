using RazorForms.Options;

namespace RazorForms;

public class FormButton : FormElementBase<IButtonOptions>
{
	public ButtonType Type { get; set; }
}