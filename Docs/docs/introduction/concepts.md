# Concepts

RazorForms was designed to be usable by any size of Razor Pages or MVC app. It's possible to completely customize the output of all of the RazorForms tag helpers, or just make tweaks to small parts. You can even use RazorForms tag helper classes to create your own tag helpers, such as custom date pickers.

## Terminology

RazorForms has a couple terms that we use that don't necessarily fit into the ASP.NET vocabulary, so we thought it would be helpful to explain those terms here.

### `Validity-aware tag helper`

Most of our tag helpers are "validity-aware", meaning they know about the validation state of the model member they represent. Say you have this model for logging in a user:

```csharp
public class LoginModel
{
    [BindProperty]
    public LoginDto Login { get; set; } = new();

    public void OnGet() {}

    public Task OnPost()
    {
        // authenticate the user
    }
}

public class LoginDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
```

Your login form will look something like this:

```cshtml
<form method="post">
    <text-input asp-for="Login.Username"/>
    <text-input asp-for="Login.Password"/>

    <button type="submit">Log in</button>
</form>
```

If the user submits the form empty, there will be validation messages in the `ModelState`. When `LoginModel.OnPost()` re-renders the login form, the `<text-input>` tag helpers will automatically retrieve the validation messages for `Login.Username` and `Login.Password` respectively, and render those messages to the screen. In this way, the `<text-input>` tag helper is **validation-aware**, i.e., the tag helper can render different content based on whether the text field is valid or not. The most common use case for this is rendering different CSS classes so the label and input visually indicate errors. If you [create a custom template](/docs/guides/custom-templates), you can do a lot more - for example, adding a checkmark if a field is valid, or adding a well-placed "Oh noes!" if not.

The validity-aware tag helpers are `<text-input>`, `<text-area-input>`, `<select-input>`, `<check-input-group>`, and `<radio-input-group>`.

### `Validity-unaware tag helper`

Some of our tag helpers are "validity-unaware", meaning they *don't* know about the validation state of the model member they represent. Say you have this model for registering a user:

```csharp
public class RegisterModel
{
    [BindProperty]
    public RegisterDto NewUser { get; set; } = new();

    public void OnGet() {}

    public Task OnPost()
    {
        // register the user
    }
}

public class RegisterDto
{
    [Required]
    public string Gender { get; set; }
    // ...other properties
}
```

Your registration form will look something like this:

```cshtml
<form method="post">
    <radio-input-group asp-for="NewUser.Gender">
        <radio-input asp-for="NewUser.Gender"
                     value="f">
            Female
        </radio-input>
        <radio-input asp-for="NewUser.Gender"
                     value="m">
            Male
        </radio-input>
        <radio-input asp-for="NewUser.Gender"
                     value="nb">
            Non-binary
        </radio-input>
        <radio-input asp-for="NewUser.Gender"
                     value="other">
            Prefer not to say
        </radio-input>
    </radio-input-group>

    <button type="submit">Register</button>
</form>
```

If the user submits the form empty, there will be validation messages in the `ModelState`. However, when this page re-renders, you don't want the `<radio-input>` to render error messages because if it does, the same error message will be rendered *four times* - once for each input. If you still want error messages to print for a validity-unaware input, we created validity-aware `<...-group>` tag helpers to wrap around those inputs. This way, you only get one set of error messages. A drawback of this approach is that you can't add CSS classes to your inputs if their model member is invalid, but you can still use a descendant selector to target inputs inside a `<...-group>` with an error class.

The validity-unaware tag helpers are `<check-input>` and `<radio-input>`. There are corresponding `<check-input-group>` and `<radio-input-group>` tag helpers, which are validity-aware. (These tag helpers are actually identical and interchangeable, but they are both available in case developers decide to create different custom templates for checkbox groups and radio groups.)