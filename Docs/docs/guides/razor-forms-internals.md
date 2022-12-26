# Razor Forms internals

To give you a better idea of how Razor Forms actually does what it does, let's take a look under the hood at the classes that make RazorForms work.

## `RazorForms.TagHelpers.TagHelperBase`

This class is inherited indirectly by every single tag helper in RazorForms, so it's the perfect place to start. You can view the source code [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/TagHelperBase.cs).

### Properties

- `IHtmlGenerator HtmlGenerator` An `IHtmlGenerator` used to create new tag helpers in code
- `IHtmlHelper HtmlHelper` An `IHtmlHelper` used to render the template file
- `TOptions Options` The configuration options for this specific type of tag helper
- `Func<bool, HtmlEncoder, Task<TagHelperContent>> DefaultTagHelperContent` A function that returns an empty `TagHelperContent`, used to create `TagHelperOutput` instances with empty output
- `bool LabelReceivesChildContent` Whether the `<label>` should receive the child content of the tag helper or not
- `string ContainerTag = string.Empty` The HTML element to wrap the entire tag helper output with
- `TagMode ContainerTagMode = TagMode.StartTagAndEndTag` The `TagMode` to use for the tag helper output
- `string InputTag = "input"` The HTML element to wrap the input tag helper output with
- `TagMode InputTagMode = TagMode.SelfClosing` The `TagMode` to use for the input tag helper output
- `ViewContext ViewContext` The Razor `ViewContext`
- `ModelExpression For` (usable in markup as `asp-for`) The `ModelExpression` that returns the model member this tag helper represents
- `string? TemplatePath` (usable in markup as `template-path`) A path string indicating which `.cshtml` template to use for this tag helper. If not specified, defaults to the value of `FormComponentOptions.TemplatePath`

### `ProcessAsync()`

Like all tag helpers, everything starts at `ProcessAsync()`. None of the RazorForms tag helpers override `ProcessAsync()`, so the implementation in `TagHelperBase` is what runs in every tag helper. It only has 7 lines of code; these 7 statements hook into extensible parts of RazorForms to generate all the different tag helpers available. `ProcessAsync()` is broken down into 3 logical tasks: configure the tag helper output, set up the model for the `.cshtml` template, and render the `.cshtml` template to the tag helper output.

#### Configure the tag helper output

Configuring the tag helper output is simple. All we do is set the `TagName` and `TagMode` based on `TagHelperBase.ContainerTag` and `TagHelperBase.ContainerTagMode`. We use protected properties because these are implementation details of the tag helpers themselves, and don't change based on your RazorForms configuration - so it's not appropriate to give end users control over these properties or store them in configuration.

`ContainerTag` defaults to an empty string, so by default, there is no HTML wrapper around the tag helper output.

`ContainerTagMode` defaults to `TagMode.StartTagAndEndTag`, which is what you want if you're going to render child input inside.

#### Set up the template model

All RazorForms `.cshtml` templates have a POCO model, so we need to set that up here.

The `GenerateHtmlModel()` method creates the model. This method is one of only two methods on TagHelperBase that aren't `virtual`, but this method calls several other methods that *are* virtual, so you can still customize this process.

The `ProcessModel()` method allows you to arbitrarily modify the model in whatever way you want. So this would be a prime opportunity to do custom processing for your model (for example, deciding what icon to render based on the validity of the data model).

#### Render the template to the tag helper output

Now that the model is created and processed, we can render the template and write its output.

First, we need to prepare the `HtmlHelper`, which is a class instance of `IHtmlHelper`. It's not ready to use out of the box - it needs to be contextualized first, which gives it access to the `ViewContext` that it needs to do its job. It doesn't come pre-contextualized because the underlying class, `HtmlHelper`, is registered in DI as a transient service, so since you're setting this one up yourself, you have to manually prepare it for use.

Next, we render the template using our contextualized `HtmlHelper`. `TagHelperBase` will use the `TemplatePath` value if provided, or else it will fall back to the `TemplatePath` on the configuration options for that tag helper (see the [custom templates guide](/docs/guides/custom-templates#use-a-custom-template-for-a-single-instance-of-a-tag-helper) for more information).

Then, all we need to do is set the content of the tag helper output to our rendered template!

#### `GenerateHtmlModel()`

It seems like we got a lot for just a little in `ProcessAsync()`, doesn't it? Sure, a lot of the markup comes from the template, but most of the logic happens in the tag helper, so where is it?

Well, as it turns out, most of this happens in `GenerateHtmlModel()` and the methods it calls. Each line of the source is a different logical step, so we'll go over it sequentially.

#### Create `<label>` output

The `<label>` and the `<input>` tags aren't created in the Razor template. Instead, they're created here, in C# code. But why?

The `<label>` and `<input>` tag helpers use the `asp-for` parameter to decide which model member they're rendering for. `asp-for` is a [ModelExpression](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.viewfeatures.modelexpression?view=aspnetcore-7.0), which is a little different from most other expressions. For starters, it isn't representable as a lambda: when you use a `ModelExpression` in a Razor template, it infers that the member you reference is coming off your template's model. There's no straightforward way to deconstruct a `ModelExpression` into its underlying model reference, so there's also no straightforward way to create a reusable Razor template when form inputs are involved. If you try, what happens is your `ModelExpression` will no longer represent your Razor Page model member - instead, in your reusable Razor template, your `ModelExpression` will point to the `ModelExpression` property on your ViewComponent, and none of it will work as expected.

RazorForms sidesteps this problem by creating the `<label>` and `<input>` tag helpers manually in C# code. By working in C# instead of Razor, the `ModelExpression` resolves as expected, and everything just works. So for this reason, the rendered `<label>` and `<input>` are stored on the model passed to the Razor template, and the Razor template just injects those values directly into the template from the model properties.

#### Create new `TOptions`

The `TOptions` here represents the type of the configuration options for this tag helper. For the built-in tag helpers, validity-aware tag helpers use [ValidityAwareFormComponentOptions](/docs/api/ValidityAwareFormComponentOptions) and validity-unaware tag helpers use [FormComponentOptions](/docs/api/FormComponentOptions). Instead of passing the global configuration options to the template model, however, we're going to copy the options so that we can modify them if we need to (more on that later).

#### Create `<input>` output

We manually create and render the `<input>` for the same reason as the `<label>`. However, if the `<input>` is supposed to render inside the `<label>`, it was created when we created the `<label>` so we don't need to create it here.

#### Set up model options

We created a new `TOptions` a couple lines ago because we may want to modify the options (again, more on that later). Here, we need to copy over any options our template needs that we don't want to modify.

#### Set up CSS classes

We don't apply CSS classes directly to the output. Rather, we pass CSS classes to the `TOptions` we created a few lines ago, and then let the template render those CSS classes as necessary.

### `ProcessModel()`

Here, we can do any additional processing on the model that needs to happen. However, the base implementation doesn't do anything.

Custom tag helpers that extend TagHelperBase can do additional processing here if they use a subclass of `MarkupModel` that adds new properties. For example, `ValidityAwareTagHelperBase` overrides this method to add validity-related information to the model.

### `CreateLabel()`

As discussed previously, we create the `<label>` in code. We could just hand-render a `<label>` in our template, but the `LabelTagHelper` built into ASP.NET does some handy work for us (like setting the `for` attribute), so we decided to just use the built-in tag helper.

The source code can be broken down into 6 logical tasks: create the `LabelTagHelper`, create a `TagHelperOutput` to store the generated output, add custom attributes to the `<label>`, render the child content, apply CSS classes to the `<label>`, and process the `LabelTaghelper`.

#### Create the `LabelTagHelper`

Since we're doing everything manually, there's a lot of work we need to do to generate a `<label>`. We start by creating the `LabelTagHelper` and setting up its `ViewContext` and `For` properties. We already have those on the `TagHelperBase` instance, so this is simple.

#### Create the `TagHelperOutput`

Tag helpers require a `TagHelperOutput` to actually render anything. This is typically handled by the framework when you call tag helpers from a Razor template, but when you're manually rendering a tag helper in C# code, you need to manually create a `TagHelperOutput` for it to render to.

#### Add custom attributes to the `<label>`

Some tag helpers need to add additional attributes to the label. We do that here, once the `TagHelperOutput` has been created.

#### Render the child content

The `<label>` has several different possible configurations of its child content, so this logical portion is more complex than the others.

The `<label>` can either render the `<input>` or not. Sometimes the `<label>` receives the child content of the tag helper, and sometimes it doesn't. If it does, the `<label>` can either render the `<input>` or the child content first. The `<label>` text can either be surrounded by an HTML tag or not. The next 60 or so lines represent the logic of figuring out how to render the `<label>`'s child content, given those possible configurations.

#### Apply CSS classes to the `<label>`

Now that the `<label>`'s child content has been generated, we need to apply CSS classes to the `<label>` itself.

#### Process the `LabelTaghelper`

Finally, the `<label>` is ready to go! The framework will typically call `labelHelper.ProcessAsync()` itself - but again, we're doing this all manually, so we need to call `labelHelper.ProcessAsync()` manually too. Once the `LabelTagHelper` has been processed, the `labelOutput` is populated with content, so we can return it.

### `CreateInput()`

Just like the `<label>`, we create the input in code. The built-in `InputTagHelper`, `TextareaTagHelper`, and `SelectTagHelper` add HTML attributes used by JavaScript error handling on the client.

The source code can be broken down into 7 logical tasks: create the tag helper, create a `TagHelperOutput` to store the generated output, add custom attributes to the output, render the child content, initialize the tag helper, apply CSS classes to the output, and process the output.

#### Create tag helper

To create the tag helper, we're going to call a different method, `CreateInputTagHelper()`. `CreateInput()` is how RazorForms defines the logic of creating the input's rendered content, but `CreateInputTagHelper()` is how RazorForms creates the actual tag helper.

#### Create the `TagHelperOutput`

As with the `<label>`, we can't render anything if we don't have a `TagHelperOutput`. In this case, which tag to use is defined by the `TagHelperBase.InputTag` property. This defaults to `input`, but needs to be changed if a different tag is expected. For example, the `SelectInputTagHelper` changes this property to `select`.

#### Add custom attributes to the output

It's also possible to add custom attributes to the input tag helper output. We do that here, once the `TagHelperOutput` has been created.

#### Render the child content

If the label *doesn't* receive the child content, the input *does*. For example, the `<select-input>` allows you to pass your `<option>`s as child content, so the underlying `<select>` will need to receive the child content.

#### Initialize the tag helper

Some tag helpers need initialized. Since we don't know what kind of tag helper we're dealing with, we go ahead and initialize the tag helper here because initializing a tag helper that doesn't need it won't hurt anything. (We didn't initialize the `LabelTagHelper` in `CreateLabel()` because the `LabelTagHelper` doesn't have any initialization - its `Init()` method is empty. But some tag helpers, such as `SelectTagHelper`, *do* have required initialization, so we need to make sure to call it just in case.)

#### Apply CSS classes to the output

Now that the output's child content has been generated, we can apply CSS classes to the underlying input.

#### Process the output

As with the `<label>`, we have to call `tagHelper.ProccessAsync()` manually because we're calling the tag helper from code. We do this conditionally here because it's perfectly valid for the tag helper to be `null`. Once the tag helper has been processed, the output is populated with its final content, so we can return it.

### `SetupModelOptions()`

Instead of passing the configuration object from dependency injection into the model, we decided to create a new `TOptions` and pass it into the model. In order to do this, we need to copy configuration settings that won't get re-calculated during tag helper execution.

If we already have the options object passed into the tag helper, why are we creating a new one here?

In order to keep the Razor template logic as minimal as possible, we want as much processing as possible to happen in the C# code of the tag helper. Later, we're going to calculate the final CSS classes to send to the model, but for now, we need to copy over options that aren't calculated.

### `AddCssClasses()`

Here, we calculate the CSS classes that need to be applied to the output in the tag helper, then pass those to the base `TOptions` CSS-related properties.

The `TagHelperAttributeList` comes from the base `TagHelperOutput` that contains the entire tag helper output. We want to strip the `class` attribute off, if it exists, and pass its value to the component wrapper. Then we simply copy over the input wrapper and label wrapper classes.

Overrides of this method can do even more work. For example, in `ValidityAwareTagHelperBase`, this method adds CSS classes based on the validation state of the model.

### `AddCustomLabelAttributes()`

Some tag helpers need to add custom attributes to the `<label>`. This method allows tag helpers to do that. However, the base implementation is empty because most tag helpers don't need to add anything special.

Some built-in tag helpers override this method. For example, `<check-input>` and `<radio-input>` don't use `asp-for` to create the `for` attribute on their `<label>`; instead, they create a `Guid` and pass its value to a manually-created `for` attribute to ensure that the `<label>` and `<input>` are linked in the HTML code.

### `ApplyCssClassesToLabel()`

This method is used to add CSS classes to the `<label>` tag. The `<label>` is represented by a `TagHelperOutput`, so it's fairly straightforward to add CSS classes to it.

There is an `AddClassesToOutput()` utility method that simplifies the process of adding multiple CSS classes at once (because `TagHelperOutputExtensions.AddClass()` explicitly disallows adding multiple CSS classes in a space-separated list, but RazorForms explicitly expects space-separated CSS class lists).

By default, only the `FormComponentOptions.LabelClasses` value is added to the output, but this method is `virtual` so more can be added by inheritors. For example, the `ValidityAwareTagHelperBase` overrides this method. In its implementation, the `ValidityAwareFormComponentOptions.LabelValidClasses` and `ValidityAwareFormComponentOptions.LabelInvalidClasses` are also added, depending on the validation state of the model.

### `CreateInputTagHelper()`

This method is called by `CreateInput()`. It should return the `TagHelper` that represents the tag helper's input, or null if the tag helper doesn't need an input.

This is a separate virtual method because different types of inputs may have different underlying tag helpers that they use. For example, the `<select-input>` is backed by a `SelectTagHelper`, while the `<text-area-input>` is backed by a `TextareaTagHelper`. The `<radio-input-group>` doesn't have an input, so it uses the base implementation that returns `null`.

### `AddCustomInputAttributes()`

Some tag helpers need to add custom attributes to the `<input>`. Like `AddCustomLabelAttributes()`, the base implementation of this method is empty.

Some built-in tag helpers override this method. For example, when binding multiple checkboxes to a list, each checkbox needs a unique ID to link it to its `<label>`, so we need to pass the same `Guid` from the `<label>` as the `id` attribute of the `<input>`.

### `ApplyCssClassesToInput()`

This method is used to add CSS classes to the `<input>` tag. The `<input>` is represented by a `TagHelperOutput`, so it works the same way as `ApplyCssClassesToLabel()`.

## Validity-unaware tag helpers

Now that we've seen the base class that makes the magic happen, let's take a look at the simplest tag helper implementations: the validity-unaware tag helpers.

The two validity-unaware tag helpers (`<check-input>` and `<radio-input>`) inherit from `RazorForms.TagHelpers.CheckRadioTagHelperBase`, which inherits from `TagHelperBase`. There's very little custom functionality added on top of `TagHelperBase`, so the validity-unaware tag helpers are a good next step.

### `RazorForms.TagHelpers.CheckRadioTagHelperBase`

The source code for `CheckRadioTagHelperBase` is available [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/CheckRadioTagHelperBase.cs).

#### Custom properties

- `string Type` The HTML `type` attribute to use for the `<input>`

#### `ctor()`

The constructor only makes one change over the base constructor: the `LabelReceivesChildContent` property is set to `true` because `<check-input>` and `<radio-input>` use the inner HTML as the label content.

#### `override CreateInputTagHelper()`

This override returns an `InputTagHelper` because `<check-input>` and `<radio-input>` are both represented by an `<input>`.

#### `override AddCustomLabelAttributes()`

This override adds the HTML `for` attribute to link the `<label>` to its `<input>`.

#### `override AddCustomInputAttributes()`

This override does 3 things: Adds the HTML `id` attribute to the `<input>` to link it to its `<label>`, adds the `Type` property as the value of the HTML `type` attribute, and conditionally adds the HTML `checked` attribute.

#### `AddCheckedAttribute()`

Checkboxes and radio buttons both use the HTML `checked` attribute to determine if they should be ticked on page load. However, the logic of whether to add the `checked` attribute is different for `<check-input>` and `<radio-input>`, so `AddCheckedAttribute()` is an abstract method.

### `RazorForms.TagHelpers.Inputs.CheckInputTagHelper`

This tag helper represents the `<check-input>` used in markup. Its source code is available [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/Inputs/CheckInputTagHelper.cs).

#### `ctor()`

This constructor only sets the `Type` property to `checkbox`, ensuring that the created input will have its `type` property set to `checkbox`.

#### `override AddCheckedAttribute()`

This override only needs to manually add the `checked` attribute if the model member is a `List` because ASP.NET still correctly adds the `checked` attribute if the model member is a `bool`.

In this override, the `checked` attribute is added only if all of the following checks pass:

- the `TagHelperAttributeList` passed into `AddcheckedAttribute()` has a `value` attribute
- the model member backing this `<check-input>` is not `null`
- the model member backing this `<check-input>` is a `List<T>`
- the model member backing this `<check-input>` can be cast to `IList`
- that `IList` contains a value equal to the `value` attribute of this `<check-input>`

### `RazorForms.TagHelpers.Inputs.RadioInputTagHelper`

This tag helper represents the `<radio-input>` used in markup. Its source code is available [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/Inputs/RadioInputTagHelper.cs).

#### `ctor()`

This constructor only sets the `Type` property to `radio`, ensuring that the created input will have its `type` property set to `radio`.

#### `override AddCheckedAttribute()`

This override is simpler than `CheckInputTagHelper`'s implementation because it only needs to compare the values directly, since radio buttons can't bind to lists.

## Validity-aware tag helpers

Three of the validity-aware tag helpers(`<text-input>`, `<text-area-input>`, and `<select-input>`) inherit from `RazorForms.TagHelpers.ValidityAwareTagHelperBase`, which inherits from `TagHelperBase`. The other two validity-aware tag helpers (`<check-input-group>` and `<radio-input-group>`) inherit from `RazorForms.TagHelpers.InputGroupTagHelperBase`, which inherits from `ValidityAwareTagHelperBase`.

### `RazorForms.TagHelpers.ValidityAwareTagHelperBase`

This class is the direct base for `<text-input>`, `<text-area-input>`, and `<select-input>`. It's also the indirect base class for `<check-input-group>` and `<radio-input-group>`. This class has validation-related functionality added on top of `TagHelperBase`.

#### Custom properties

- `bool IsValid` represents whether the model member this tag helper represents is *explicitly valid*, i.e. not simply "not invalid"
- `bool IsInvalid` represents whether the model member this tag helper represents is *explicitly invalid*, i.e. not simply "not valid"
- `IEnumerable<string> Errors` represents the validation error messages for the model member this tag helper represents

#### `override ProcessModel()`

This override adds the `IsValid`, `IsInvalid`, and `Errors` properties to the markup model. This override doesn't call `base.ProcessModel()` since the base implementation doesn't do anything.

#### `override SetupModelOptions()`

This override calls `base.SetupModelOptions()` to transfer the default model options to the `TOptions` instance for the markup model. It also copies the `ValidityAwareFormComponentOptions.AlwaysRenderErrorContainer` option.

#### `override AddCssClasses()`

This override adds CSS classes to the `TOptions` instance on the markup model based on the validity of the model member this tag helper represents.

The markup sections added here are the component wrapper, input block wrapper, input wrapper, label wrapper, error wrapper, and error. The explicitly valid and explicitly invalid CSS classes are also added if the model member is explicitly valid or explicitly invalid, respectively.

The error wrapper classes and error classes are added as-is without respect to validation state, since those sections of the markup are intended only to convey error information.

Because the CSS classes are pre-calculated and added to the `TOptions` instance on the `_____WrapperClasses` property, the template doesn't need to calculate whether or not to include CSS classes in the markup. This simplifies the template and keeps logic in C# code where it belongs.

#### `override ApplyCssClassesToLabel()`

This override adds CSS classes to the underlying `<label>`, with explicitly valid and explicitly invalid CSS classes added based on the validity of the model member this tag helper represents.

#### `override ApplyCssClassesToInput()`

This override adds CSS classes to the underlying `<input>`, with explicitly valid and explicitly invalid CSS classes added based on the validity of the model member this tag helper represents.

#### `CreateValidityAwareClasses()`

This new method returns a single `string` containing all CSS classes for a single section of the markup. This method appends the explicitly valid or explicitly invalid CSS class names as appropriate.

This method is used to create the CSS classes for markup sections, such as component wrappers, label wrappers, etc. Its only usages are in `AddCssClasses()` overrides.

#### `AddValidityAwareClasses()`

This new method applies validity-aware CSS classes to a `TagHelperOutput`. Its only usages are in overrides of `ApplyCssClassesToInput()` and `ApplyCssClassesToLabel()`.

### `RazorForms.TagHelpers.Inputs.TextInputTagHelper`

This tag helper represents the `<text-input>` used in markup. Its source code is available [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/Inputs/TextInputTagHelper.cs).

#### Custom properties

- `string? Format` (usable in markup as `asp-format`) A pass-through property for the `asp-format` value used by `InputTagHelper`

#### `ctor()`

This constructor sets `LabelReceivesChildContent` to `true`.

#### `override CreateInputTagHelper()`

This override creates a new `InputTagHelper`, setting the `ViewContext`, `For`, and `Format` properties to their respective values.

### `RazorForms.TagHelpers.Input.TextAreaInputTagHelper`

This tag helper represents the `<text-area-input>` used in markup. Its source code is available [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/Inputs/TextAreaInputTagHelper.cs).

#### `ctor()`

This constructor sets three properties:

- `InputTag` is set to `textarea`
- `InputTagMode` is set to `TagMode.StartTagAndEndTag`
- `LabelReceivesChildContent` is set to `true`

#### `override CreateInputTagHelper()`

This override creates a new `TextAreaTagHelper`, setting the `ViewContext` and `For` properties to their respective values.

### `RazorForms.TagHelpers.Input.SelectInputTagHelper`

This tag helper represents the `<select-input>` used in markup. Its source code is available [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/Inputs/SelectInputTagHelper.cs).

#### Custom properties

- `IEnumerable<SelectListItem>? Items` (usable in markup as `asp-items`) Allows passing an array of `SelectListItem`s to automatically render your `<option>`s

#### `ctor()`

This constructor sets two properties:

- `InputTag` is set to `select`
- `InputTagMode` is set to `TagMode.StartTagAndEndTag`

#### `override CreateInputTagHelper()`

This override creates a new `SelectTagHelper`, setting the `ViewContext`, `For`, and `Items` properties to their respective values.

### `RazorForms.TagHelpers.InputGroupTagHelperBase`

This tag helper is the base class for the `<check-input-group>` and `<radio-input-group>` used in markup. (The `CheckInputGroupTagHelper` and `RadioInputGroupTagHelper` inherit directly from this tag helper with no modificiations, so they are identical to this class.) Its source is available [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/InputGroupTagHelperBase.cs).

#### `ctor()`

This constructor sets two properties:

- `InputTag` is set to an empty string (this allows the tag helper to render its child content in place of an `<input>` tag)
- `InputTagMode` is set to `TagMode.StartTagAndEndTag` (this is a just-in-case setting, as it has no effect on the generated markup; however, if an inheritor changes `InputTag` to something else, such as a `<div>`, this property will need to be `TagMode.StartTagAndEndTag` in order to render child content)

### `RazorForms.TagHelpers.Elements.CheckInputGroupTagHelper`

This tag helper represents the `<check-input-group>` used in markup. It is an unmodified subclass of `InputGroupTagHelperBase`, so it is identical to that tag helper. This tag helper exists as a separate tag helper in the event that a developer wants to customize `<check-input-group>` separately from `<radio-input-group>`. Its source is available [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/Elements/CheckInputGroupTagHelper.cs).

### `RazorForms.TagHelpers.Elements.RadioInputGroupTagHelper`

This tag helper represents the `<radio-input-group>` used in markup. It is an unmodified subclass of `InputGroupTagHelperBase`, so it is identical to that tag helper. This tag helper exists as a separate tag helper in the event that a developer wants to customize `<radio-input-group>` separately from `<check-input-group>`. Its source is available [on GitHub](https://github.com/christianlevesque/RazorForms/blob/main/RazorForms/TagHelpers/Elements/RadioInputGroupTagHelper.cs).