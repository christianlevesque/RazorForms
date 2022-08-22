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
		new SelectListItem
		{
			Value = "www.facebook.com",
			Text = "Facebook"
		},
		new SelectListItem
		{
			Value = "www.linkedin.com",
			Text = "LinkedIn"
		},
		new SelectListItem
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