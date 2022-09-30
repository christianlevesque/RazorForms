using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExampleProject.Pages.Shared;

public class AttributeExampleModel : PageModel
{
	[BindProperty]
	[MinLength(5, ErrorMessage = "Your first name must be at least 5 characters long")]
	[Display(Name = "First name")]
	public string? FirstName { get; set; }

	[BindProperty]
	[Required]
	[MinLength(5, ErrorMessage = "Your last name must be at least 5 characters long")]
	[Display(Name = "Last name")]
	public string? LastName { get; set; }

	[BindProperty]
	[Required]
	[MinLength(8, ErrorMessage = "Your password must be at least 8 characters long")]
	[DataType(DataType.Password)]
	[Display(Name = "Your secure password")]
	public string? Password { get; set; }

	[BindProperty]
	[Required]
	[Compare(nameof(Password), ErrorMessage = "The passwords do not match!")]
	[Display(Name = "Re-enter your secure password")]
	public string? PasswordMatch { get; set; }

	[BindProperty]
	[Range(21, 119, ErrorMessage = "You must be between 21 and 119 to join!")]
	[Display(Name = "Your age")]
	public int Age { get; set; }

	[BindProperty]
	[Required]
	[DataType(DataType.Url)]
	[Display(Name = "Preferred social media platform")]
	public string? PageUrl { get; set; }

	[BindProperty]
	[Required]
	[Range(48, 96, ErrorMessage = "Only people between 4' and 8' in height may join!")]
	[Display(Name = "Your approximate height (in inches)")]
	public int? Height { get; set; }

	[BindProperty]
	[Required]
	[DataType(DataType.MultilineText)]
	[MaxLength(500, ErrorMessage = "Your biography must be shorter than 500 characters")]
	[Display(Name = "Your bio")]
	public string? Biography { get; set; }

	[BindProperty]
	[Required]
	[Display(Name = "Favorite book series")]
	public string? Series { get; set; }

	[BindProperty]
	[Required]
	[Display(Name = "Favorite number")]
	public int? Number { get; set; }

	[BindProperty]
	[Display(Name = "Your interest(s)")]
	public List<string> Interests { get; set; } = new ();

	[BindProperty]
	[Display(Name = "Your lucky number(s)")]
	public List<int> Numbers { get; set; } = new ();

	[BindProperty]
	[Display(Name = "Sign me up for marketing emails")]
	public bool WantsMarketingEmails { get; set; }

	[BindProperty]
	[Display(Name = "I agree to the Terms of Service")]
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