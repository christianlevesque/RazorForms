using BootstrapExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BootstrapExample.Pages;

public class IndexModel : PageModel
{
	[BindProperty]
	public User UserData { get; set; } = new();

	public SelectListItem[] Items =
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

	public IActionResult OnGet()
	{
		return Page();
	}

	public IActionResult OnPost()
	{
		return Page();
	}
}