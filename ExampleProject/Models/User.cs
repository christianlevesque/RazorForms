using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootstrapExample.Models;

public class User
{
	[Required]
	[MinLength(5, ErrorMessage = "Your first name must be at least 5 characters long")]
	[Display(Name = "First name")]
	public string? FirstName { get; set; }

	[Required]
	[MinLength(5, ErrorMessage = "Your last name must be at least 5 characters long")]
	[Display(Name = "Last name")]
	public string? LastName { get; set; }

	[Required]
	[MinLength(8, ErrorMessage = "Your password must be at least 8 characters long")]
	[DataType(DataType.Password)]
	[Display(Name = "Password")]
	public string? Password { get; set; }

	[Required]
	[DataType(DataType.Password)]
	[Compare(nameof(Password), ErrorMessage = "The passwords do not match!")]
	[Display(Name = "Re-enter password")]
	public string? PasswordMatch { get; set; }

	[Required]
	[DataType(DataType.Url)]
	[Display(Name = "Preferred social media platform")]
	public string? PageUrl { get; set; }

	[Range(21, 119, ErrorMessage = "You must be between 21 and 119 to join!")]
	[Display(Name = "Age")]
	public int Age { get; set; }

	[Required]
	[Display(Name = "Biographical info")]
	[DataType(DataType.MultilineText)]
	[MaxLength(500, ErrorMessage = "Your biography must be shorter than 500 characters")]
	public string? Biography { get; set; }

	[Required]
	[Display(Name = "Favorite book series")]
	public string? Series { get; set; }

	[Required]
	[Display(Name = "Favorite number")]
	public int? Number { get; set; }

	[Display(Name = "Your ethnicity(ies)")]
	public List<string> Ethnicities { get; set; } = new ();

	[Display(Name = "Lucky number(s)")]
	public List<int> Numbers { get; set; } = new ();

	[Display(Name = "Sign me up for marketing emails")]
	public bool WantsMarketingEmails { get; set; }

	public bool AcceptTos { get; set; }
}