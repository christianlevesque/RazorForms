namespace RazorForms;

public class RazorFormsOptions : IFormClasses, IFormMarkupSettings
{
	/// <inheritdoc/>
	public string? ComponentWrapperClasses { get; set; }

	/// <inheritdoc/>
	public string? ComponentWrapperValidClasses { get; set; }

	/// <inheritdoc/>
	public string? ComponentWrapperErrorClasses { get; set; }

	/// <inheritdoc/>
	public string? InputBlockWrapperClasses { get; set; }

	/// <inheritdoc/>
	public string? InputBlockWrapperValidClasses { get; set; }

	/// <inheritdoc/>
	public string? InputBlockWrapperErrorClasses { get; set; }

	/// <inheritdoc/>
	public string? LabelWrapperClasses { get; set; }

	/// <inheritdoc/>
	public string? LabelWrapperValidClasses { get; set; }

	/// <inheritdoc/>
	public string? LabelWrapperErrorClasses { get; set; }

	/// <inheritdoc/>
	public string? LabelClasses { get; set; }

	/// <inheritdoc/>
	public string? LabelValidClasses { get; set; }

	/// <inheritdoc/>
	public string? LabelErrorClasses { get; set; }

	/// <inheritdoc/>
	public string? InputWrapperClasses { get; set; }

	/// <inheritdoc/>
	public string? InputWrapperValidClasses { get; set; }

	/// <inheritdoc/>
	public string? InputWrapperErrorClasses { get; set; }

	/// <inheritdoc/>
	public string? InputClasses { get; set; }

	/// <inheritdoc/>
	public string? InputValidClasses { get; set; }

	/// <inheritdoc/>
	public string? InputErrorClasses { get; set; }

	/// <inheritdoc/>
	public string? ErrorWrapperClasses { get; set; }

	/// <inheritdoc/>
	public string? ErrorClasses { get; set; }

	/// <inheritdoc/>
	public bool? RemoveWrappers { get; set; }

	/// <inheritdoc/>
	public bool? InputFirst { get; set; }

	/// <summary>
	/// Creates a new <see cref="RazorFormsOptions"/> using values from existing <see cref="IFormClasses"/> and/or <see cref="IFormMarkupSettings"/>
	/// </summary>
	/// <remarks>
	///
	/// </remarks>
	/// <param name="classes"></param>
	/// <param name="markup"></param>
	/// <returns></returns>
	public virtual RazorFormsOptions Merge(IFormClasses? classes, IFormMarkupSettings? markup)
	{
		var newOptions = new RazorFormsOptions();
		if (classes != null) 
		{
			newOptions.ComponentWrapperClasses = MergeCssClassNames(ComponentWrapperClasses, classes.ComponentWrapperClasses);
			newOptions.ComponentWrapperValidClasses = MergeCssClassNames(ComponentWrapperValidClasses, classes.ComponentWrapperValidClasses);
			newOptions.ComponentWrapperErrorClasses = MergeCssClassNames(ComponentWrapperErrorClasses, classes.ComponentWrapperErrorClasses);
			newOptions.InputBlockWrapperClasses = MergeCssClassNames(InputBlockWrapperClasses, classes.InputBlockWrapperClasses);
			newOptions.InputBlockWrapperValidClasses = MergeCssClassNames(InputBlockWrapperValidClasses, classes.InputBlockWrapperValidClasses);
			newOptions.InputBlockWrapperErrorClasses = MergeCssClassNames(InputBlockWrapperErrorClasses, classes.InputBlockWrapperErrorClasses);
			newOptions.LabelWrapperClasses = MergeCssClassNames(LabelWrapperClasses, classes.LabelWrapperClasses);
			newOptions.LabelWrapperValidClasses = MergeCssClassNames(LabelWrapperValidClasses, classes.LabelWrapperValidClasses);
			newOptions.LabelWrapperErrorClasses = MergeCssClassNames(LabelWrapperErrorClasses, classes.LabelWrapperErrorClasses);
			newOptions.LabelClasses = MergeCssClassNames(LabelClasses, classes.LabelClasses);
			newOptions.LabelValidClasses = MergeCssClassNames(LabelValidClasses, classes.LabelValidClasses);
			newOptions.LabelErrorClasses = MergeCssClassNames(LabelErrorClasses, classes.LabelErrorClasses);
			newOptions.InputWrapperClasses = MergeCssClassNames(InputWrapperClasses, classes.InputWrapperClasses);
			newOptions.InputWrapperValidClasses = MergeCssClassNames(InputWrapperValidClasses, classes.InputWrapperValidClasses);
			newOptions.InputWrapperErrorClasses = MergeCssClassNames(InputWrapperErrorClasses, classes.InputWrapperErrorClasses);
			newOptions.InputClasses = MergeCssClassNames(InputClasses, classes.InputClasses);
			newOptions.InputValidClasses = MergeCssClassNames(InputValidClasses, classes.InputValidClasses);
			newOptions.InputErrorClasses = MergeCssClassNames(InputErrorClasses, classes.InputErrorClasses);
			newOptions.ErrorWrapperClasses = MergeCssClassNames(ErrorWrapperClasses, classes.ErrorWrapperClasses);
			newOptions.ErrorClasses = MergeCssClassNames(ErrorClasses, classes.ErrorClasses);
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

	/// <summary>
	/// Merges two sets of CSS class names into a single set
	/// </summary>
	/// <remarks>
	/// This method does not perform any verification, such as removing duplicate entries. It simply returns the combined result of two inputs with no leading or trailing whitespace.
	/// </remarks>
	/// <param name="input1">The first set of CSS classes to concatenate</param>
	/// <param name="input2">The second set of CSS classes to concatenate</param>
	/// <returns></returns>
	protected virtual string? MergeCssClassNames(string? input1, string? input2)
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