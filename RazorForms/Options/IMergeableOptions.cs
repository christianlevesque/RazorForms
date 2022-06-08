namespace RazorForms.Options;

public interface IMergeableOptions<TOptions>
{
	/// <summary>
	/// Merges a separate set of <c>TOptions</c> into the current options
	/// </summary>
	/// <param name="existingOptions"></param>
	/// <returns></returns>
	TOptions Merge(TOptions existingOptions);
}