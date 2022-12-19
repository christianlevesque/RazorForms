# Overview

RazorForms is a library of ASP.NET tag helpers that takes the work out of building large forms in Razor Pages and MVC. 

## Start using RazorForms

### Installation

You can install RazorForms using your favorite Nuget package manager. RazorForms is currently in preview, so you will need to specify a version if you install it from the command line. In a terminal, navigate to the directory containing your ASP.NET Razor Pages project and run:

```bash
dotnet add package RazorForms --version 1.0.0-beta4
```

### Register services

RazorForms service extension methods are namespaced to `Microsoft.Extensions.DependencyInjection`, so all you need to do is add services using the extension method. The primary `UseRazorForms()` overload accepts an `Action<RazorFormsOptions>`, so you can set any options directly on an instance of `RazorFormsOptions`:

```csharp
builder.Services.UseRazorForms(o => {});
```

For more information on customizing RazorForms, see [Configuration](/docs/introduction/configuration)

### Start building

That's all you need to get started! Now, you can look through our list of available tag helpers and see how they're used.

## Additional reading

For more information on how to customize RazorForms, see [Configuration](/docs/introduction/configuration).

For information on how to pass custom classes, attributes, or HTML content, see [Arbitrary Content](/docs/introduction/arbitrary-content) 