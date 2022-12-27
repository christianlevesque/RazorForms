# Custom templates

RazorForms templates are designed to be highly customizable through the options API. However, sometimes you need more granular control over the markup that is generated. In those cases, you can create your own `.cshtml` template files and point RazorForms to those files.

## Background on templates

There are three templates used directly by RazorForms:

### `~/RazorFormsTemplates/Partials/WrappedContent.cshtml`

This template is used to render `<label>` and `<input>` tag helpers with an optional `<div>` wrapper. The source code of this template can be found [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/RazorFormsTemplates/Partials/WrappedContent.cshtml).

This template expects a model of [RazorForms.Models.WrappedContentModel](/docs/api/WrappedContentModel).

This template is extremely simple because all it does is render either a `<label>` or an `<input>` (it receives content as `IHtmlContent`, so it doesn't care which), and optionally surrounds it with a `<div>`. The `<label>` and `<input>` are created programmatically by [RazorForms.TagHelpers.TagHelperBase](/docs/guides/razor-forms-internals) and stored on the [WrappedContentModel](/docs/api/WrappedContentModel) by the other template files.

**NOTE**: This template is not called directly by a tag helper. Rather, this template is called by the other two templates, its result is stored in a local variable, and then the final output is rendered by the other two templates. You can use this template as a partial in your own template files, but you can't change the path used to render wrapped content in configuration without creating your own overrides for the other two templates.

### `~/RazorFormsTemplates/Partials/Content.cshtml`

This template is used to render the output of tag helpers **without** validation information (`<check-input>` and `<radio-input>`). By default, it creates a `<div>` wrapper around the output and renders the `<input>` and `<label>` inside. The source code of this template can be found [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/RazorFormsTemplates/Partials/Content.cshtml).

This template expects a model of [RazorForms.Models.MarkupModel<RazorForms.Options.FormComponentOptions>(/docs/api/MarkupModel).

### `~/RazorFormsTemplates/Partials/ValidityAwareContent.cshtml`

This template is used to render the output of tag helpers **with** validation information (`<text-input>`, `<text-area-input>`, `<select-input>`, `<check-input-group>`, and `<radio-input-group>`). By default, it creates a `<div>` wrapper around the entire output, creates a second `<div>` wrapper around the `<label>` and `<input>` tags, then optionally renders error messages to the user. The source can be found [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/RazorFormsTemplates/Partials/ValidityAwareContent.cshtml).

This template expects a model of [RazorForms.Models.ValidityAwareMarkupModel](/docs/api/ValidityAwareMarkupModel), which extends [MarkupModel<ValidityAwareFormComponentOptions>](/docs/api/MarkupModel).

## GUIDE Setting up custom templates

There are several ways to handle creating custom templates for your tag helpers. Depending on your needs, you may want to:

- create a custom template for a single instance of a tag helper
- create a custom template for every instance of a tag helper
- create a custom template for every kind of tag helper (i.e., validity-aware tag helpers like `<text-input>` or `<select-input>`)

### Initial setup

You can create your own custom template and follow along if you want, but we're also going to make one for you. Here, we created a custom template that is almost identical to the main template, with one small change: the component wrapper `<div>` has the `custom-template` class applied to make it easier to identify. Feel free to use your own custom template, or copy the one below and use it as described in each section.

```cshtml
@using RazorForms.Models
@model RazorForms.Models.ValidityAwareMarkupModel

@{
    const string partialPath = "~/RazorFormsTemplates/Partials/WrappedContent.cshtml";

    var inputContent = await Html.PartialAsync(
        partialPath,
        new WrappedContentModel(
            Model.InputHtml,
            Model.ElementOptions.InputWrapperClasses,
            Model.ElementOptions.RemoveWrappers));

    var labelContent = await Html.PartialAsync(
        partialPath,
        new WrappedContentModel(
            Model.LabelHtml,
            Model.ElementOptions.LabelWrapperClasses,
            Model.ElementOptions.RemoveWrappers));
}

<div class="custom-template @Model.ElementOptions.ComponentWrapperClasses">
    <div class="@Model.ElementOptions.InputBlockWrapperClasses">
        @if (Model.ElementOptions.InputFirst)
        {
            @inputContent
            @labelContent
        }
        else
        {
            @labelContent
            @inputContent
        }
    </div>

    @if (Model.ElementOptions.AlwaysRenderErrorContainer || Model.IsInvalid)
    {
        <ul class="@Model.ElementOptions.ErrorWrapperClasses">
            @foreach (var e in Model.Errors)
            {
                <li class="@Model.ElementOptions.ErrorClasses">@e</li>
            }
        </ul>
    }
</div>
```

Save your custom template wherever you like in your ASP.NET Core project (try to avoid saving templates to `~/RazorFormsTemplates`, to prevent naming conflicts). We're going to assume that you saved the template at `~/CustomTemplates/MyTemplate.cshtml`, but you can save it wherever you like - just remember to change our path to yours when you use it!

### Use a custom template for a single instance of a tag helper

To apply your custom template to an individual tag helper, pass the `template-path` argument to the tag helper:

```cshtml
<text-input asp-for="SomeTextField"
            template-path="~/CustomTemplates/MyTemplate.cshtml"/>
```

**NOTE**: `template-path` is part of the base tag helper used by all RazorForms tag helpers, so this example works for any RazorForms tag helper, not just `<text-input>`.

### Use a custom template for every instance of a single type of tag helper

Say you want to use your custom template for every instance of `<text-input>`. To do that, instead of passing the template path into the tag helper itself, you can change the template path when you configure RazorForms in `Program.cs`:

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.UseRazorForms(o =>
{
	o.TextInputOptions.TemplatePath = "~/CustomTemplates/MyTemplate.cshtml";
});
```

**NOTE**: `TemplatePath` is part of the [FormComponentOptions](/docs/api/FormComponentOptions) class, which is the base class for every tag helper configuration object. This example works for any RazorForms tag helper, not just `<text-input>`.

### Use a custom template for every tag helper that is either validity-aware or validity-unaware

If you want to fundamentally change the way all tag helpers render, you can! All you need to do in that case is change the name of your template file to match the RazorForms template you want to override.

- If you want to change the rendering of all validity-aware tag helpers (`<text-input>`, `<text-area-input>`, `<select-input>`, `<check-input-group>`, and `<radio-input-group>`), create a new template in your project root at `~/RazorFormsTemplates/Partials/ValidityAwareContent.cshtml`. Make sure that its model is of type [RazorForms.Models.ValidityAwareMarkupModel](/docs/api/ValidityAwareMarkupModel).
- If you want to change the rendering of all validity-unaware tag helpers (`<check-input>`, `<radio-input>`), create a new template in your project root at `~/RazorFormsTemplates/Partials/Content.cshtml`. Make sure that its model is of type [RazorForms.Models.MarkupModel<RazorForms.Options.FormComponentOptions>](/docs/api/MarkupModel).
- If you want to change the rendering of all wrappers around `<label>` and `<input>` tags, create a new template in your project root at `~/RazorFormsTemplates/Partials/WrappedContent.cshtml`. Make sure that its model is of type [RazorForms.Models.WrappedContentModel](/docs/api/WrappedContentModel).