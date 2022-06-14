using RazorForms.Options.Elements;

namespace RazorForms.Generators.Elements;

public interface IButtonGenerator : IOutputGenerator<IButtonOptions>
{
	void Init(IButtonOptions options, ButtonType type);
}