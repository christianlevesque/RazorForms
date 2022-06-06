using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.TagHelpers;

public class TextInputTagHelper : TagHelperBase
{
	public InputType? Type { get; set; }

	/// <inheritdoc />
	public TextInputTagHelper(IHtmlHelper html, RazorFormsOptions options) : base(html, options)
	{
	}

	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var model = await ProcessBase<FormInput>(output);
		model.Type = Type ?? GetInputType();

		var content = await Html.PartialAsync("~/Templates/TextInput.cshtml", model);
		output.Content.SetHtmlContent(content);
	}

	private InputType GetInputType()
	{
		var dataTypeName = For.ModelExplorer.Metadata.DataTypeName;
		return string.IsNullOrEmpty(dataTypeName)
			       ? ConvertNativeTypeToInputType(For.ModelExplorer.ModelType)
			       : ConvertDataTypeToInputType(dataTypeName);
	}

	private static InputType ConvertDataTypeToInputType(string type)
	{
		switch (type)
		{
			case nameof(DataType.Password):
				return InputType.Password;
			case nameof(DataType.Url):
			case nameof(DataType.ImageUrl):
				return InputType.Url;
			case nameof(DataType.EmailAddress):
				return InputType.EmailAddress;
			case nameof(DataType.PhoneNumber):
				return InputType.PhoneNumber;
			case nameof(DataType.Currency):
				return InputType.Number;
			case nameof(DataType.MultilineText):
				return InputType.TextArea;
			case nameof(DataType.PostalCode):
			case nameof(DataType.Text):
				return InputType.Text;
			default:
				throw new NotSupportedException($"Data type {type} is not supported for text inputs");
		}
	}

	private static InputType ConvertNativeTypeToInputType(Type t)
	{
		if (t == typeof(string))
		{
			return InputType.Text;
		}

		if (t == typeof(byte)
		 || t == typeof(sbyte)
		 || t == typeof(short)
		 || t == typeof(ushort)
		 || t == typeof(int)
		 || t == typeof(uint)
		 || t == typeof(long)
		 || t == typeof(ulong)
		 || t == typeof(float)
		 || t == typeof(double))
		{
			return InputType.Number;
		}

		throw new NotSupportedException($"Data type {t} is not supported on the {nameof(TextInputTagHelper)}.");
	}
}