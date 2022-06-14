﻿namespace RazorForms.Options.Inputs;

public class InputOptions : IInputOptions
{
	/// <inheritdoc />
	public string? ComponentWrapperClasses { get; set; }

	/// <inheritdoc />
	public bool? RemoveWrappers { get; set; }

	/// <inheritdoc />
	public string? ComponentWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? ComponentWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputBlockWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? LabelWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? LabelClasses { get; set; }

	/// <inheritdoc />
	public string? LabelValidClasses { get; set; }

	/// <inheritdoc />
	public string? LabelErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputWrapperErrorClasses { get; set; }

	/// <inheritdoc />
	public string? InputClasses { get; set; }

	/// <inheritdoc />
	public string? InputValidClasses { get; set; }

	/// <inheritdoc />
	public string? InputErrorClasses { get; set; }

	/// <inheritdoc />
	public string? ErrorWrapperClasses { get; set; }

	/// <inheritdoc />
	public string? ErrorClasses { get; set; }

	/// <inheritdoc />
	public bool? InputFirst { get; set; }
}