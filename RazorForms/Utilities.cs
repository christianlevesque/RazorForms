using System;
using System.ComponentModel;
using System.Linq;

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
}