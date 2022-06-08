namespace RazorForms.Options;

public abstract class OptionsBase
{
	protected virtual string? MergeText(string? input1, string? input2)
	{
		if (string.IsNullOrEmpty(input1) && string.IsNullOrEmpty(input2))
		{
			return null;
		}

		if (!string.IsNullOrEmpty(input1) && string.IsNullOrEmpty(input2))
		{
			return input1;
		}

		if (!string.IsNullOrEmpty(input2) && string.IsNullOrEmpty(input1))
		{
			return input2;
		}

		return $"{input1} {input2}";
	}
}