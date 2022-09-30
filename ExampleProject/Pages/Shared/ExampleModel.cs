using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExampleProject.Pages.Shared;

public class ExampleModel : PageModel
{
	[BindProperty]
	[Required]
	[MinLength(5, ErrorMessage = "Your first name must be at least 5 characters long")]
	public string? FirstName { get; set; }

	[BindProperty]
	[Required]
	[MinLength(5, ErrorMessage = "Your last name must be at least 5 characters long")]
	public string? LastName { get; set; }

	[BindProperty]
	[Required]
	[MinLength(8, ErrorMessage = "Your password must be at least 8 characters long")]
	[DataType(DataType.Password)]
	public string? Password { get; set; }

	[BindProperty]
	[Required]
	[Compare(nameof(Password), ErrorMessage = "The passwords do not match!")]
	public string? PasswordMatch { get; set; }

	[BindProperty]
	[Required]
	[DataType(DataType.Url)]
	public string? PageUrl { get; set; }

	[BindProperty]
	[Required]
	[Range(48, 96, ErrorMessage = "Only people between 4' and 8' in height may join!")]
	public int? Height { get; set; }

	[BindProperty]
	[Range(21, 119, ErrorMessage = "You must be between 21 and 119 to join!")]
	public int Age { get; set; }

	[BindProperty]
	[Required]
	[DataType(DataType.MultilineText)]
	[MaxLength(500, ErrorMessage = "Your biography must be shorter than 500 characters")]
	public string? Biography { get; set; }

	[BindProperty]
	[Required]
	public string? Series { get; set; }

	[BindProperty]
	[Required]
	public int? Number { get; set; }

	[BindProperty]
	public List<string> Interests { get; set; } = new ();

	[BindProperty]
	public List<int> Numbers { get; set; } = new ();

	[BindProperty]
	public bool WantsMarketingEmails { get; set; }

	[BindProperty]
	public bool AcceptTos { get; set; }

	public void OnGet() {}
	public void OnPost() {}

	public SelectListItem[] SocialMediaPlatforms =
	{
		new ()
		{
			Text = "Please select a social media platform",
			Value = string.Empty
		},
		new ()
		{
			Value = "www.facebook.com",
			Text = "Facebook"
		},
		new ()
		{
			Value = "www.linkedin.com",
			Text = "LinkedIn"
		},
		new ()
		{
			Value = "www.twitter.com",
			Text = "Twitter"
		}
	};
}