using System;
using System.Text;

namespace RazorForms.Templates.Elements;

public abstract class ButtonRawBase : TemplateBase<FormButton>
{
	protected string? GenerateClasses()
	{
		return Model.Type switch
		{
			ButtonType.Default => Model.Options.DefaultButtonClasses,
			ButtonType.Submit => Model.Options.SubmitButtonClasses,
			ButtonType.Reset => Model.Options.ResetButtonClasses,
			_ => throw new NotSupportedException($"Invalid {nameof(ButtonType)}")
		};
	}

	protected void GenerateAdditionalAttributes(StringBuilder sb)
	{
		sb.AppendWithLeadingSpace($"type=\"{Model.Type.GetDescription()}\"");
	}
}