using System.ComponentModel;

namespace RazorForms;

public enum InputType
{
	[Description("text")] Text,
	[Description("textarea")] TextArea,
	[Description("password")] Password,
	[Description("url")] Url,
	[Description("email")] EmailAddress,
	[Description("tel")] PhoneNumber,
	[Description("number")] Number
}