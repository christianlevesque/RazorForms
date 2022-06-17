# RazorForms
## A simple ASP.NET Core library to simplify composing reusable forms in Razor Pages

For most websites, a solution like Razor Pages works really well. It's fairly straightforward to use, and it offers power and flexibility. However, composing forms with Razor Pages is _hot garbage_.

Creating reusable UI elements is a breeze using partials and view components, and the built-in tag helpers make working with forms pretty straightforward. But it's not immediately obvious how to create reusable form UI while taking advantage of Razor Pages features like automatic value binding. Creating a simple registration form in Razor Pages using Bootstrap 5 looks something like this:

```cshtml
<form asp-page="/Registration"
      method="post">
    <div class="mb-3">
        <label asp-for="Username"
               class="form-label">
            Username
        </label>
        <input asp-for="Username"
               class="form-control"/>
        <span asp-validation-for="Username" class="text-danger"></span>
    </div>
    <div class="mb-3">
        <label asp-for="Email"
               class="form-label">
            Email address
        </label>
        <input asp-for="Email"
               class="form-control"/>
        <span asp-validation-for="Email" class="text-danger"></span>
    </div>
    <div class="mb-3">
        <label asp-for="Password"
               class="form-label">
            Password
        </label>
        <input asp-for="Password"
               class="form-control"/>
        <span asp-validation-for="Password" class="text-danger"></span>
    </div>
    <div class="mb-3">
        <label asp-for="PasswordConfirm"
               class="form-label">
            Confirm password
        </label>
        <input asp-for="PasswordConfirm"
               class="form-control"/>
        <span asp-validation-for="PasswordConfirm" class="text-danger"></span>
    </div>
    <button type="submit"
            class="btn btn-primary">
        Register
    </button>
    <button type="reset"
            class="btn btn-outline-secondary">
        Register
    </button>
</form>
```

As I said..._hot. verbose. garbage_.

You may be thinking of reaching for the trusty old SPA frameworks, because this is one of the problems they were designed to solve. But do you really need to create an entire SPA frontend just because your forms are a pain in the butt to compose?

## Enter RazorForms

It doesn't have to be this way! You absolutely can create simple, straightforward websites that take advantage of all that Razor Pages has to offer without resorting to writing your entire frontend in JavaScript _just because forms get repetitive_.

The form shown above can be refactored using RazorForms:

```cshtml
<form asp-page="/Registration"
      method="post">
    <text-input asp-for="Username"/>
    <text-input asp-for="Email"/>
    <text-input asp-for="Password"/>
    <text-input asp-for="PasswordConfirm"/>
    <submit-button>
        Register
    </submit-button>
    <reset-button>
        Register
    </reset-button>
</form>
```

That's a reduction of _almost 75%_ (47 lines vs. 13 lines with RazorForms). The correct input type is automatically detected in most cases, so all you usually need to do is pass the `asp-for` attribute with a reference to the model member the form field is for, just like with the built-in label and input tag helpers.

For more information on using RazorForms in a project, see the [docs](https://www.razorforms.com).