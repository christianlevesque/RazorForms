using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RazorForms;

public static class Utilities
{
	public static string GetDescription(this Enum e)
	{
		var attr = e.GetType()
		            .GetField(e.ToString())?
		            .GetCustomAttributes(typeof (DescriptionAttribute), false)
		            .FirstOrDefault() as DescriptionAttribute;
		return attr?.Description ?? e.ToString();
	}

	/// <summary>
	/// Appends a value to the given <see cref="StringBuilder"/> instance, including a leading space if the instance is not currently empty
	/// </summary>
	/// <param name="self">the <see cref="StringBuilder"/> instance</param>
	/// <param name="input">the value to append</param>
	public static void AppendClass(this StringBuilder self, string? input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return;
		}

		var value = self.Length == 0 ? input : $" {input}";
		self.Append(value);
	}
}