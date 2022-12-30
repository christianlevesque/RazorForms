# Tag Helper Output Structure

All RazorForms tag helpers use two main template files to create their output. Validity-aware tag helpers use `~/RazorFormsTemplates/Partials/ValidityAwareContent.cshtml`, while validity-unaware tag helpers use `~/RazorFormsTemplates/Partials/Content.cshtml`.

Parts of the markup can be modified or disabled via configuration. For more information on configuring RazorForms, see [configuration](/docs/introduction/configuration). If you want to make changes to the markup that *aren't* supported by configuration, you can [create custom templates](/docs/guides/custom-templates).

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

The entire component is wrapped with a `<div>`. This cannot be disabled in configuration. The component wrapper classes can be set with [FormComponentOptions.ComponentWrapperClasses](/docs/api/FormComponentOptions#componentwrapperclasses).

### Label wrapper

The `<label>` is wrapped with its own `<div>`. This can be disabled in configuration. The label wrapper classes can be set with [FormComponentOptions.LabelWrapperClasses](/docs/api/FormComponentOptions#labelwrapperclasses).

### Label

The `<label>` classes can be set with [FormComponentOptions.LabelClasses](/docs/api/FormComponentOptions#labelclasses).

### Input wrapper

The `<input>` is wrapped with its own `<div>`. This can be disabled in configuration. The input wrapper classes can be set with [FormComponentOptions.InputWrapperClasses](/docs/api/FormComponentOptions#inputwrapperclasses).

### Input

The `<input>` classes can be set with [FormComponentOptions.InputClasses](/docs/api/FormComponentOptions#inputclasses).

### Additional configuration options

- [FormComponentOptions.RemoveWrappers](/docs/api/FormComponentOptions#removewrappers) If `true`, the `<label>` and `<input>` are rendered directly inside the component wrapper, instead of inside their own `<div>` wrappers
- [FormComponentOptions.InputFirst](/docs/api/FormComponentOptions#inputfirst) If `true`, the `<input>` and its wrapper `<div>` are rendered first inside the component wrapper
- [FormComponentOptions.RenderInputInsideLabel](/docs/api/FormComponentOptions#renderinputinsidelabel) If `true`, the `<input>` and its wrapper `<div>` are rendered inside the `<label>` instead of adjacent to it

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

- [FormComponentOptions.ComponentWrapperClasses](/docs/api/FormComponentOptions#componentwrapperclasses) CSS classes that always apply regardless of validation state
- [ValidityAwareFormComponentOptions.ComponentWrapperValidClasses](/docs/api/ValidityAwareFormComponentOptions#componentwrappervalidclasses) CSS classes that apply when the form element is explicitly valid
- [ValidityAwareFormComponentOptions.ComponentWrapperInvalidClasses](/docs/api/ValidityAwareFormComponentOptions#componentwrapperinvalidclasses) CSS classes that apply when the form element is explicitly invalid

### Input block wrapper

The `<input>` and `<label>` are wrapped with a `<div>`. This cannot be disabled in configuration. The input block wrapper supports the following configuration options:

- [FormComponentOptions.InputBlockWrapperClasses](/docs/api/FormComponentOptions#inputblockwrapperclasses) CSS classes that always apply regardless of validation state
- [ValidityAwareFormComponentOptions.InputBlockWrapperValidClasses](/docs/api/ValidityAwareFormComponentOptions#inputblockwrappervalidclasses) CSS classes that apply when the form element is explicitly valid
- [ValidityAwareFormComponentOptions.InputBlockWrapperInvalidClasses](/docs/api/ValidityAwareFormComponentOptions#inputblockwrapperinvalidclasses) CSS classes that apply when the form element is explicitly invalid

### Label wrapper

The `<label>` is wrapped with its own `<div>`. This can be disabled in configuration. The label wrapper supports the following configuration options:

- [FormComponentOptions.LabelWrapperClasses](/docs/api/FormComponentOptions#labelwrapperclasses) CSS classes that always apply regardless of validation state
- [ValidityAwareFormComponentOptions.LabelWrapperValidClasses](/docs/api/ValidityAwareFormComponentOptions#labelwrappervalidclasses) CSS classes that apply when the form element is explicitly valid
- [ValidityAwareFormComponentOptions.LabelWrapperInvalidClasses](/docs/api/ValidityAwareFormComponentOptions#labelwrapperinvalidclasses) CSS classes that apply when the form element is explicitly invalid

### Label

The `<label>` supports the following configuration options:

- [FormComponentOptions.LabelClasses](/docs/api/FormComponentOptions#labelclasses) CSS classes that always apply regardless of validation state
- [ValidityAwareFormComponentOptions.LabelValidClasses](/docs/api/ValidityAwareFormComponentOptions#labelvalidclasses) CSS classes that apply when the form element is explicitly valid
- [ValidityAwareFormComponentOptions.LabelInvalidClasses](/docs/api/ValidityAwareFormComponentOptions#labelinvalidclasses) CSS classes that apply when the form element is explicitly invalid

### Input wrapper

The `<input>` is wrapped with its own `<div>`. This can be disabled in configuration. The input wrapper supports the following configuration options:

- [FormComponentOptions.InputWrapperClasses](/docs/api/FormComponentOptions#inputwrapperclasses) CSS classes that always apply regardless of validation state
- [ValidityAwareFormComponentOptions.InputWrapperValidClasses](/docs/api/ValidityAwareFormComponentOptions#inputwrappervalidclasses) CSS classes that apply when the form element is explicitly valid
- [ValidityAwareFormComponentOptions.InputWrapperInvalidClasses](/docs/api/ValidityAwareFormComponentOptions#inputwrapperinvalidclasses) CSS classes that apply when the form element is explicitly invalid

### Input

The `<input>` supports the following configuration options:

- [FormComponentOptions.InputClasses](/docs/api/FormComponentOptions#inputclasses) CSS classes that always apply regardless of validation state
- [ValidityAwareFormComponentOptions.InputValidClasses](/docs/api/ValidityAwareFormComponentOptions#inputvalidclasses) CSS classes that apply when the form element is explicitly valid
- [ValidityAwareFormComponentOptions.InputInvalidClasses](/docs/api/ValidityAwareFormComponentOptions#inputinvalidclasses) CSS classes that apply when the form element is explicitly invalid

### Error wrapper

The `<ul>` wraps around error messages. This cannot be disabled in configuration. Because the error wrapper is intended only to convey error information, it doesn't support validity-based classes. The error wrapper supports the following configuration options:

- [ValidityAwareFormComponentOptions.ErrorWrapperClasses](/docs/api/ValidityAwareFormComponentOptions#errorwrapperclasses) CSS classes that always apply regardless of validation state

### Error 

The `<li>` contains error messages. This cannot be disabled in configuration. Because the error is intended only to convey error information, it doesn't support validity-based classes. The error supports the following configuration options:

- [ValidityAwareFormComponentOptions.ErrorClasses](/docs/api/ValidityAwareFormComponentOptions#errorclasses) CSS classes that always apply regardless of validation state

### Additional configuration options

- [FormComponentOptions.RemoveWrappers](/docs/api/FormComponentOptions#removewrappers) If `true`, the `<label>` and `<input>` are rendered directly inside the input block wrapper, instead of inside their own `<div>` wrappers
- [FormComponentOptions.InputFirst](/docs/api/FormComponentOptions#inputfirst) If `true`, the `<input>` and its wrapper `<div>` are rendered first inside the input block wrapper
- [FormComponentOptions.RenderInputInsideLabel](/docs/api/FormComponentOptions#renderinputinsidelabel) If `true`, the `<input>` and its wrapper `<div>` are rendered inside the `<label>` instead of adjacent to it
- [ValidityAwareFormComponentOptions.AlwaysRenderErrorContainer](/docs/api/ValidityAwareFormComponentOptions#alwaysrendererrorcontainer) If `true`, the `<ul>` that contains errors is always rendered, even if empty