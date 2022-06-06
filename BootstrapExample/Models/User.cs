using System.ComponentModel.DataAnnotations;

namespace BootstrapExample.Models;

public class User
{
	[Required]
	[MinLength(5, ErrorMessage = "Your first name must be at least 5 characters long")]
	[Display(Name = "First name")]
	public string FirstName { get; set; } = string.Empty;

	[Required]
	[MinLength(5, ErrorMessage = "Your last name must be at least 5 characters long")]
	[Display(Name = "Last name")]
	public string LastName { get; set; } = string.Empty;

	[Required]
	[MinLength(8, ErrorMessage = "Your password must be at least 8 characters long")]
	[DataType(DataType.Password)]
	[Display(Name = "Password")]
	public string Password { get; set; } = string.Empty;

	[Required]
	[DataType(DataType.Password)]
	[Compare(nameof(Password), ErrorMessage = "The passwords do not match!")]
	[Display(Name = "Re-enter password")]
	public string PasswordMatch { get; set; } = string.Empty;

	[Required]
	[DataType(DataType.Url)]
	[Display(Name = "Website URL")]
	public string PageUrl { get; set; } = string.Empty;

	[Range(21, 119, ErrorMessage = "You must be between 21 and 119 to join!")]
	[Display(Name = "Age")]
	public int Age { get; set; }
}