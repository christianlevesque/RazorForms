namespace RazorForms;

public class RazorFormsOptions : IFormClasses, IFormMarkupSettings
{
	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component
	/// </summary>
	public string? ComponentWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component when model validation succeeds
	/// </summary>
	public string? ComponentWrapperValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire component when model validation fails
	/// </summary>
	public string? ComponentWrapperErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block
	/// </summary>
	public string? InputBlockWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block when model validation succeeds
	/// </summary>
	public string? InputBlockWrapperValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the entire input block when model validation fails
	/// </summary>
	public string? InputBlockWrapperErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt;
	/// </summary>
	public string? LabelWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt; when model validation succeeds
	/// </summary>
	public string? LabelWrapperValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;label&gt; when model validation fails
	/// </summary>
	public string? LabelWrapperErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;label&gt;
	/// </summary>
	public string? LabelClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;label&gt; when model validation succeeds
	/// </summary>
	public string? LabelValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;label&gt; when model validation fails
	/// </summary>
	public string? LabelErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt;
	/// </summary>
	public string? InputWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt; when model validation succeeds
	/// </summary>
	public string? InputWrapperValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;div&gt; surrounding the &lt;input&gt; when model validation fails
	/// </summary>
	public string? InputWrapperErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;input&gt;
	/// </summary>
	public string? InputClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;input&gt; when model validation succeeds
	/// </summary>
	public string? InputValidClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;input&gt; when model validation fails
	/// </summary>
	public string? InputErrorClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;ul&gt; containing the input validation errors
	/// </summary>
	public string? ErrorWrapperClasses { get; set; }

	/// <summary>
	/// CSS classes applied to the &lt;li&gt; containing each input validation error
	/// </summary>
	public string? ErrorClasses { get; set; }

	/// <summary>
	/// Determines whether or not to remove the &lt;div&gt; surrounding &lt;label&gt; and &lt;input&gt; elements
	/// </summary>
	public bool? RemoveWrappers { get; set; }

	/// <summary>
	/// Determines whether the &lt;input&gt; should come first in the markup or not
	/// </summary>
	public bool? InputFirst { get; set; }

	public virtual RazorFormsOptions Merge(IFormClasses? classes, IFormMarkupSettings? markup)
	{
		var newOptions = new RazorFormsOptions();
		if (classes != null) 
		{
			newOptions.ComponentWrapperClasses = MergeText(ComponentWrapperClasses, classes.ComponentWrapperClasses);
			newOptions.ComponentWrapperValidClasses = MergeText(ComponentWrapperValidClasses, classes.ComponentWrapperValidClasses);
			newOptions.ComponentWrapperErrorClasses = MergeText(ComponentWrapperErrorClasses, classes.ComponentWrapperErrorClasses);
			newOptions.InputBlockWrapperClasses = MergeText(InputBlockWrapperClasses, classes.InputBlockWrapperClasses);
			newOptions.InputBlockWrapperValidClasses = MergeText(InputBlockWrapperValidClasses, classes.InputBlockWrapperValidClasses);
			newOptions.InputBlockWrapperErrorClasses = MergeText(InputBlockWrapperErrorClasses, classes.InputBlockWrapperErrorClasses);
			newOptions.LabelWrapperClasses = MergeText(LabelWrapperClasses, classes.LabelWrapperClasses);
			newOptions.LabelWrapperValidClasses = MergeText(LabelWrapperValidClasses, classes.LabelWrapperValidClasses);
			newOptions.LabelWrapperErrorClasses = MergeText(LabelWrapperErrorClasses, classes.LabelWrapperErrorClasses);
			newOptions.LabelClasses = MergeText(LabelClasses, classes.LabelClasses);
			newOptions.LabelValidClasses = MergeText(LabelValidClasses, classes.LabelValidClasses);
			newOptions.LabelErrorClasses = MergeText(LabelErrorClasses, classes.LabelErrorClasses);
			newOptions.InputWrapperClasses = MergeText(InputWrapperClasses, classes.InputWrapperClasses);
			newOptions.InputWrapperValidClasses = MergeText(InputWrapperValidClasses, classes.InputWrapperValidClasses);
			newOptions.InputWrapperErrorClasses = MergeText(InputWrapperErrorClasses, classes.InputWrapperErrorClasses);
			newOptions.InputClasses = MergeText(InputClasses, classes.InputClasses);
			newOptions.InputValidClasses = MergeText(InputValidClasses, classes.InputValidClasses);
			newOptions.InputErrorClasses = MergeText(InputErrorClasses, classes.InputErrorClasses);
			newOptions.ErrorWrapperClasses = MergeText(ErrorWrapperClasses, classes.ErrorWrapperClasses);
			newOptions.ErrorClasses = MergeText(ErrorClasses, classes.ErrorClasses);
		}

		if (markup != null)
		{
			if (markup.RemoveWrappers.HasValue)
			{
				newOptions.RemoveWrappers = markup.RemoveWrappers;
			}
			else
			{
				newOptions.RemoveWrappers = RemoveWrappers;
			}
    
            if (markup.InputFirst.HasValue)
            {
            	newOptions.InputFirst = markup.InputFirst;
            }
            else
            {
	            newOptions.InputFirst = InputFirst;
            }
		}

		return newOptions;
	}

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