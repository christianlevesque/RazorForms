﻿using RazorForms.Options.Elements;
using RazorForms.Options.Inputs;

namespace RazorForms;

public class RazorFormsOptions
{
	public IInputOptions InputOptions { get; set; } = new InputOptions();
	public ICheckInputOptions CheckInputOptions { get; set; } = new CheckInputOptions();
	public ICheckInputGroupOptions CheckInputGroupOptions { get; set; } = new CheckInputGroupOptions();
	public ITextAreaOptions TextAreaOptions { get; set; } = new TextAreaOptions();
	public ISelectOptions SelectOptions { get; set; } = new SelectOptions();
	public IButtonOptions ButtonOptions { get; set; } = new ButtonOptions();
}