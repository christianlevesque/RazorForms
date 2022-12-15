using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleProject.Pages;

public class IndexModel : PageModel
{
	public IActionResult OnGet()
	{
		return Page();
	}
}