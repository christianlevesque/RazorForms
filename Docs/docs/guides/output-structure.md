# Tag Helper Output Structure

All RazorForms tag helpers use two main template files to create their output. Validity-aware tag helpers use `~/RazorFormsTemplates/Partials/ValidityAwareContent.cshtml`, while validity-unaware tag helpers use `~/RazorFormsTemplates/Partials/Content.cshtml`.

Parts of the markup can be modified or disabled via configuration. For more information on configuring RazorForms, see [configuration](/docs/introduction/configuration) If you want to make changes to the markup that *aren't* supported by configuration, you can [create custom templates](/docs/guides/custom-templates).

## Validity-unaware tag helper output

```html
<div> <!-- component wrapper -->
    <div> <!-- label wrapper, disable in configuration -->
        <label>...</label>
    </div>
    <div> <!-- input wrapper, disable in configuration -->
        <input/>
    </div>
</div>
```

### Component wrapper

The entire component is wrapped with a `<div>`. This cannot be disabled in configuration. The component wrapper classes can be set with **FormComponentOptions.ComponentWrapperClasses**.

### Label wrapper

The `<label>` is wrapped with its own `<div>`. This can be disabled in configuration. The label wrapper classes can be set with **FormComponentOptions.LabelWrapperClasses**.

### Label

The `<label>` classes can be set with **FormComponentOptions.LabelClasses**.

### Input wrapper

The `<input>` is wrapped with its own `<div>`. This can be disabled in configuration. The input wrapper classes can be set with **FormComponentOptions.InputWrapperClasses**.

### Input

The `<input>` classes can be set with **FormComponentOptions.InputClasses**.

### Additional configuration options

- **FormComponentOptions.RemoveWrappers** If `true`, the `<label>` and `<input>` are rendered directly inside the component wrapper, instead of inside their own `<div>` wrappers
- **FormComponentOptions.InputFirst** If `true`, the `<input>` and its wrapper `<div>` are rendered first inside the component wrapper
- **FormComponentOptions.RenderInputInsideLabel** If `true`, the `<input>` and its wrapper `<div>` are rendered inside the `<label>` instead of adjacent to it

## Validity-aware tag helper output

```html
<div> <!-- component wrapper -->
    <div> <!-- input block wrapper -->
        <div> <!-- label wrapper, disable in configuration -->
            <label>...</label>
        </div>

        <div> <!-- input wrapper, disable in configuration -->
            <!-- type varies based on model -->
            <input/>
        </div>
    </div>

    <ul> <!-- error wrapper, only rendered if errors are present -->
        <li>...</li> <!-- error -->
    </ul>
</div>
```

### Component wrapper

The entire component is wrapped with a `<div>`. This cannot be disabled in configuration. The component wrapper supports the following configuration options:

- **ValidityAwareFormComponentOptions.ComponentWrapperClasses** CSS classes that always apply regardless of validation state
- **ValidityAwareFormComponentOptions.ComponentWrapperValidClasses** CSS classes that apply when the form element is explicitly valid
- **ValidityAwareFormComponentOptions.ComponentWrapperInvalidClasses** CSS classes that apply when the form element is explicitly invalid

### Input block wrapper

The `<input>` and `<label>` are wrapped with a `<div>`. This cannot be disabled in configuration. The input block wrapper supports the following configuration options:

- **ValidityAwareFormComponentOptions.InputBlockWrapperClasses** CSS classes that always apply regardless of validation state
- **ValidityAwareFormComponentOptions.InputBlockWrapperValidClasses** CSS classes that apply when the form element is explicitly valid
- **ValidityAwareFormComponentOptions.InputBlockWrapperInvalidClasses** CSS classes that apply when the form element is explicitly invalid

### Label wrapper

The `<label>` is wrapped with its own `<div>`. This can be disabled in configuration. The label wrapper supports the following configuration options:

- **ValidityAwareFormComponentOptions.LabelWrapperClasses** CSS classes that always apply regardless of validation state
- **ValidityAwareFormComponentOptions.LabelWrapperValidClasses** CSS classes that apply when the form element is explicitly valid
- **ValidityAwareFormComponentOptions.LabelWrapperInvalidClasses** CSS classes that apply when the form element is explicitly invalid

### Label

The `<label>` supports the following configuration options:

- **ValidityAwareFormComponentOptions.LabelClasses** CSS classes that always apply regardless of validation state
- **ValidityAwareFormComponentOptions.LabelValidClasses** CSS classes that apply when the form element is explicitly valid
- **ValidityAwareFormComponentOptions.LabelInvalidClasses** CSS classes that apply when the form element is explicitly invalid

### Input wrapper

The `<input>` is wrapped with its own `<div>`. This can be disabled in configuration. The input wrapper supports the following configuration options:

- **ValidityAwareFormComponentOptions.InputWrapperClasses** CSS classes that always apply regardless of validation state
- **ValidityAwareFormComponentOptions.InputWrapperValidClasses** CSS classes that apply when the form element is explicitly valid
- **ValidityAwareFormComponentOptions.InputWrapperInvalidClasses** CSS classes that apply when the form element is explicitly invalid

### Input

The `<input>` supports the following configuration options:

- **ValidityAwareFormComponentOptions.InputClasses** CSS classes that always apply regardless of validation state
- **ValidityAwareFormComponentOptions.InputValidClasses** CSS classes that apply when the form element is explicitly valid
- **ValidityAwareFormComponentOptions.InputInvalidClasses** CSS classes that apply when the form element is explicitly invalid

### Error wrapper

The `<ul>` wraps around error messages. This cannot be disabled in configuration. Because the error wrapper is intended only to convey error information, it doesn't support validity-based classes. The error wrapper supports the following configuration options:

- **ValidityAwareFormComponentOptions.ErrorWrapperClasses** CSS classes that always apply regardless of validation state

### Error 

The `<li>` contains error messages. This cannot be disabled in configuration. Because the error is intended only to convey error information, it doesn't support validity-based classes. The error supports the following configuration options:

- **ValidityAwareFormComponentOptions.ErrorClasses** CSS classes that always apply regardless of validation state

### Additional configuration options

- **ValididtyAwareFormComponentOptions.RemoveWrappers** If `true`, the `<label>` and `<input>` are rendered directly inside the input block wrapper, instead of inside their own `<div>` wrappers
- **ValidityAwareFormComponentOptions.InputFirst** If `true`, the `<input>` and its wrapper `<div>` are rendered first inside the input block wrapper
- **ValidityAwareFormComponentOptions.RenderInputInsideLabel** If `true`, the `<input>` and its wrapper `<div>` are rendered inside the `<label>` instead of adjacent to it
- **ValidityAwareFormComponentOptions.AlwaysRenderErrorContainer** If `true`, the `<ul>` that contains errors is always rendered, even if empty