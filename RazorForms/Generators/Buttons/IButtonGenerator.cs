using RazorForms.Options;
using RazorForms.TagHelpers;

namespace RazorForms.Generators.Buttons;

public interface IButtonGenerator : IOutputGenerator<IButtonOptions>
{
	void Init(IButtonOptions options, ButtonType type);
}