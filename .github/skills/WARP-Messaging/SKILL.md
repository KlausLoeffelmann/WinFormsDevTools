---
name: warptoolkit-messaging
description: >-
  Use this skill when UI code should show a DarkMode- and app-font-aware themed
  message or confirmation with WarpToolkit.WinForms FluentMessageBox or directly
  customize its FluentMessageBoxForm, instead of using MessageBox. For dialogs
  originating in a ViewModel, use warp-app-services and IDialogService instead.
---

# WarpToolkit.WinForms Messaging

## Package Dependencies

The following NuGet packages are necessary to add to the project.
* Make sure, that existing packages use at least the NuGet versions stated below. 
* Check references projects, for contradicting NuGet package definitions or versions, which need to be updated.

```Markdown
# NuGet Packages info

* minimum Version: 0.9.324-preview.ge962db2903
* Package(s) required for this skill:
  - WarpToolkit.WinForms
```

`FluentMessageBox` is a drop-in replacement for `System.Windows.Forms.MessageBox`
that respects DarkMode and the application font. Both types live in namespace
`WarpToolkit.WinForms`.

> **Source of truth:** verified against
> `src/WarpToolkit.WinForms/FluentMessageBox/`.

## FluentMessageBox (static)

`public static class FluentMessageBox`. Overloads (note: **no `owner`
parameter** — unlike `MessageBox`):

```csharp
DialogResult Show(string text, string caption);
DialogResult Show(string text, string caption, MessageBoxButtons buttons);
DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton);
```

```csharp
using WarpToolkit.WinForms;

DialogResult dr = FluentMessageBox.Show(
    "Discard your unsaved edits?",
    "Unsaved changes",
    MessageBoxButtons.YesNoCancel,
    MessageBoxIcon.Question);

if (dr == DialogResult.Yes)
{
    DiscardEdits();
}
```

Prefer `FluentMessageBox` over `MessageBox` in WARP UIs. When the call originates
in a ViewModel, route it through `IDialogService` (see the `warp-app-services` /
`winforms-mvvm` skills) instead of calling the static directly.

## FluentMessageBoxForm

`public class FluentMessageBoxForm : Form` — the dialog window behind
`FluentMessageBox`. You rarely need it directly; construct one only if you must
host or customize the message dialog yourself.

```csharp
FluentMessageBoxForm(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
FluentMessageBoxForm(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton);
```

```csharp
using FluentMessageBoxForm dialog = new(
    "File saved.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
dialog.ShowDialog(this);
```

It is a normal `Form`, so it works with WARP's async dialog helpers
(`ShowDialogAsync`) and standard `ShowDialog(owner)`.

## DarkMode & High-DPI notes

- The dialog automatically honors `Application.IsDarkModeEnabled` and the
  application default font — do not theme it manually.
- Because it is built as a regular `Form`, it scales under Per-Monitor-V2 DPI
  like any WARP form; no special handling needed.

## Common gotchas

- The **argument order is `(text, caption, …)`** and there is **no owner
  overload**. Don't copy a `MessageBox.Show(owner, text, …)` call shape — drop
  the owner and keep text first.
- For ViewModel code, don't call the static `FluentMessageBox` directly; depend
  on a dialog service so the logic stays testable.
