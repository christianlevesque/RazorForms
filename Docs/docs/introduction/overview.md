# Overview

RazorForms is a library of ASP.NET tag helpers that takes the work out of building large forms in Razor Pages and MVC. 

## Start using RazorForms

### Installation

You can install RazorForms using your favorite Nuget package manager. If you prefer the CLI, run:

```bash
dotnet add package RazorForms --version 1.0.0
```

### Register services

RazorForms service extension methods are namespaced to `Microsoft.Extensions.DependencyInjection`, so import that namespace and add RazorForms services using the extension method. The primary `UseRazorForms()` overload accepts an `Action<RazorFormsOptions>`, so you can set any options directly on an instance of `RazorFormsOptions`. Alternatively, you can pass a new instance of `RazorForms.Options.RazorFormsOptions`:

```csharp
using Microsoft.Extensions.DependencyInjection;
using RazorForms.Options;

// Set up options the traditional ASP.NET way
builder.Services.UseRazorForms(o => {});

// Provide a new options object
var options = new RazorFormsOptions();
builder.Services.UseRazorForms(options);
```

For more information on customizing RazorForms, see [Configuration](/docs/introduction/configuration)

### Start building

That's all you need to get started! Now, you can look through our list of available tag helpers and see how they're used.

## Additional reading

For more information on how to customize RazorForms, see [Configuration](/docs/introduction/configuration).

For information on how to pass custom classes, attributes, or HTML content, see [Arbitrary Content](/docs/introduction/arbitrary-content) 