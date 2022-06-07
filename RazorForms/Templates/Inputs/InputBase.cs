using Microsoft.AspNetCore.Mvc.Razor;

namespace RazorForms.Templates.Inputs;

public abstract class InputBase<TModel> : TemplateBase<TModel>
	where TModel : FormInput
{
	protected string GenerateInputWrapperClasses()
		=> GenerateClasses(Model.Options.InputWrapperClasses,
		                   Model.Options.InputWrapperValidClasses,
		                   Model.Options.InputWrapperErrorClasses);
}