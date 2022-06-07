using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorForms.TagHelpers;

public class TextInputTagHelper : TagHelperBase
{
	/// <summary>
	/// The type of the input. If supplied, this type will take precedent. If not supplied, an appropriate value will be generated
	/// </summary>
	public InputType? Type { get; set; }

	/// <inheritdoc />
	public TextInputTagHelper(IHtmlHelper html, RazorFormsOptions options) : base(html, options)
	{
	}

	/// <inheritdoc />
	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var model = await ProcessBase<FormInput>(output);
		model.Type = Type ?? GetInputType();

		var content = await Html.PartialAsync("~/Templates/TextInput.cshtml", model);
		output.Content.SetHtmlContent(content);
	}

	/// <summary>
	/// Determines the appropriate <see cref="InputType"/> for the current &lt;input&gt;
	/// </summary>
	/// <returns></returns>
	private InputType GetInputType()
	{
		var dataTypeName = For.ModelExplorer.Metadata.DataTypeName;
		return string.IsNullOrEmpty(dataTypeName)
			       ? ConvertNativeTypeToInputType(For.ModelExplorer.ModelType)
			       : ConvertDataTypeToInputType(dataTypeName);
	}

	/// <summary>
	/// Determines an appropriate <see cref="InputType"/> for the current &lt;input&gt; based on the <see cref="DataType"/> supplied on the model property
	/// </summary>
	/// <param name="type">The string representation of the <see cref="DataType"/> enum</param>
	/// <returns>the corresponding <see cref="InputType"/></returns>
	/// <exception cref="NotSupportedException">if a <see cref="DataType"/> is supplied that is not supported by a regular &lt;input&gt; element</exception>
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

	/// <summary>
	/// Determines an appropriate <see cref="InputType"/> for the current &lt;input&gt; based on the <see cref="Type"/> of the model property
	/// </summary>
	/// <param name="t">the <see cref="Type"/> of the model property</param>
	/// <returns>the corresponding <see cref="InputType"/></returns>
	/// <exception cref="NotSupportedException">if a <see cref="Type"/> is supplied that is not supported by a regular &lt;input&gt; element</exception>
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