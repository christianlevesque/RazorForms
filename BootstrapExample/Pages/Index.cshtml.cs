using BootstrapExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BootstrapExample.Pages;

public class IndexModel : PageModel
{
	[BindProperty]
	public User UserData { get; set; } = new();

	public IActionResult OnGet()
	{
		return Page();
	}

	public IActionResult OnPost()
	{
		return Page();
	}
}