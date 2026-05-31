# Prompt — Generate WarpToolkit.WinForms control-usage skills

> **How to use this file:** Open it in GitHub Copilot (CLI or IDE) and run it as a
> prompt. It instructs Copilot to author a set of reusable **skills** under
> `.github/skills/` that document how to use every public control/type in the
> `WarpToolkit.WinForms` and `WarpToolkit.WinForms.Specialized` packages.
>
> **Why this exists:** These packages ship without in-repo usage docs, so the only
> reliable way to learn their API is to **decompile** the assemblies. We have had
> to do that repeatedly. These skills capture that knowledge permanently so nobody
> has to decompile them again.

---

## Your objective

Author a set of **focused, Designer-aware skills** under `.github/skills/` that
teach correct, idiomatic usage of the controls and supporting types in:

- `WarpToolkit.WinForms` (base package)
- `WarpToolkit.WinForms.Specialized`

Cover the **full public API surface**: visual controls, container/layout controls,
data grids, messaging/dialogs, console & diagnostics, commands & MVVM, data
binding / AutoView, and symbols/iconography.

Produce **several focused skills grouped by control family** (see
[Skill grouping](#skill-grouping)) — not one mega-skill, and not one skill per
package.

This task **creates documentation skills only**. Do not modify application code,
csproj files, or package versions.

---

## Source of truth — decompile, never guess

There is no XML-doc or usage guide in the repo for these packages, so the
authoritative API is whatever the **decompiled assemblies** show. Document only
members that actually exist; verify every property, enum, method, and event
signature against the decompiled output. **Never invent or assume API shape.**

### Tooling

Use `ilspycmd` (a global .NET tool):

```pwsh
# Install once if it isn't already present:
dotnet tool install -g ilspycmd
```

### Resolve the assemblies to decompile

The package versions are centrally pinned by the `WarpToolkitVersion` MSBuild
property in `src/Directory.Packages.props`. Resolve the restored DLLs from the
NuGet cache (pick the newest restored version, and the `net*-windows*` target):

```pwsh
$base = "$env:USERPROFILE\.nuget\packages"
foreach ($pkg in "warptoolkit.winforms","warptoolkit.winforms.specialized") {
    Get-ChildItem -Recurse -Filter "*.dll" "$base\$pkg" `
        | Where-Object FullName -match 'lib\\net.*windows' `
        | Sort-Object FullName -Descending `
        | Select-Object -First 1 -ExpandProperty FullName
}
```

### Decompile

```pwsh
# A single type (fast, targeted):
ilspycmd <path-to-dll> -t WarpToolkit.WinForms.Specialized.WarpDataGridView

# The whole assembly (then read the generated *.decompiled.cs):
ilspycmd <path-to-dll> --outputdir <output-dir>
```

### Re-enumerate the public types

The [inventory below](#baseline-type-inventory) is a **baseline captured from a
specific build** — treat it as a starting point, not a freeze. Re-list the public
types from the decompiled output and adjust the grouping if types were added,
removed, or renamed:

```pwsh
Select-String -Path <decompiled.cs> `
    -Pattern '(public)\s+(?:sealed\s+|abstract\s+|partial\s+|static\s+)*(class|enum|struct|interface)\s+\w+' `
    | ForEach-Object { $_.Line.Trim() } | Sort-Object -Unique
```

---

## Skill grouping

Create one skill per family as `.github/skills/<kebab-name>/SKILL.md`:

| Skill folder | Types to document |
|---|---|
| `warptoolkit-containers-layout` | `FluentTabControl`, `AdornerPanel`, `AdornerTableLayoutPanel`, `TransparentPanel` (+ `AdornerBorderStyle`, `TransparencyStyle`) |
| `warptoolkit-wizard` | `WizardContainer`, `WizardPage`, wizard event args (`WizardPageEventArgs`, `WizardPageLeavingEventArgs`, `WizardPageValidatingEventArgs`) and enums (`WizardContainerScalingMode`, `WizardDialogResult`, `WizardNavigationDirection`) |
| `warptoolkit-input-controls` | `FilePathPicker` (+ `FilePathPickerMode`), `BindableComboBox`, `FloatingPointSlider`, `FloatingPointTrackBar`, `EditFloatingPointSlider` (+ `VerticalContentAlignments`) |
| `warptoolkit-data-grids` | `GridView` (+ `GridViewExtension`, `GridViewItemTemplate` and its converters) and `WarpDataGridView` (+ `AlternatingRowColorMode`) — emphasize SystemColors theming, DarkMode, and alternating rows |
| `warptoolkit-messaging` | `FluentMessageBox` (static), `FluentMessageBoxForm` |
| `warptoolkit-console-diagnostics` | `ConsoleControl` (+ `ConsoleChannel`, `ConsoleMessageKind`), `HexAsciiDumper` (+ `DataFormat`, `ShowText`), `FilenameDisambiguator` (+ `GenerationStrategy`), `CustomFontStyle`, `FontSize` — **Specialized** package |
| `warptoolkit-commands-mvvm` | `UiCommandBase`, `FormCommand`, `MessageBoxCommand`, `CommandTypeProvider`, `IUiCommand` (+ command type converters) |
| `warptoolkit-binding-autoview` | `AutoViewBase<TControl>`, `AutoViewControlMappingBase`, `TypeToControlFactoryBase`, `StringToTextBoxFactory`, `BindableComboBox` binding usage (+ `AutoPropertyApplySettings`) |
| `warptoolkit-symbols` | `FluentSymbols` (static) + symbol enums (`AllSymbols`, `CommonToolStripSymbols`, `DevelopmentSymbols`, `TreeViewSymbols`) |

If re-enumeration reveals types not listed here, slot them into the most relevant
family (or add a new focused skill rather than overloading an existing one).

---

## Required content of each `SKILL.md`

Each skill must contain, in order:

1. **YAML frontmatter** with `name` and `description`. Match the style of the
   existing `winforms-*` skills: the `description` states what the skill is and
   includes a "Use this when …" clause. Example:

   ```yaml
   ---
   name: warptoolkit-data-grids
   description: Guide for WarpToolkit GridView and WarpDataGridView controls, including SystemColors theming, DarkMode-aware alternating rows, and data binding. Use this when displaying tabular data with WarpToolkit grids or theming a DataGridView for Light/Dark mode.
   ---
   ```

2. **"When to Use This Skill"** — a short bullet list of trigger scenarios.

3. **Per control**, document:
   - Purpose and **base type** (e.g. `WarpDataGridView : DataGridView`).
   - Key **properties, enums, events, and methods** — each verified against the
     decompiled source.
   - A **Designer-compatible** C# usage example: all configuration inside
     `InitializeComponent`, controls as backing fields, **no** helper methods, no
     object initializers (per the `winforms-designer-code` skill).

4. **DarkMode & High-DPI notes** where relevant (e.g. `WarpDataGridView` derives
   cell/header colors from `SystemColors` and exposes `AlternatingRowColorMode`;
   note `AutoScaleMode`/`Inherit` guidance for hosted UserControls).

5. **Common gotchas / caveats** — e.g. constructor execution order for inherited
   controls (`CreateParams`/`SetStyle`/`DefaultSize` run before field
   initializers), values that must not be set before the base theming runs, etc.

---

## Conventions to follow

- Mirror the tone, structure, and formatting of the existing skills in
  `.github/skills/`, especially:
  - `winforms-designer-code` — the authoritative rules for `InitializeComponent`
    and Designer compatibility (all examples MUST comply).
  - `winforms-high-dpi-fluent-layout` — DPI/layout guidance to reference.
  - `winforms-databinding` and `winforms-mvvm` — for the binding/commands skills.
  - `winforms-rendering` — for any owner-draw/painting notes.
- Reuse **real usages already in this repository** as worked examples:
  - `FluentTabControl` in `src/DevTools.RuntimeDeploy/MainForm.Designer.cs`.
  - `FilePathPicker` in `src/DevTools.RuntimeDeploy/Views/OverView.Designer.cs`.
  - `ConsoleControl` + `AdornerPanel` in
    `src/DevTools.RuntimeDeploy/Infrastructure/CommandBatchForm.Designer.cs`.
  - Any `WarpDataGridView` subclass present under
    `src/DevTools.RuntimeDeploy/Infrastructure/`.

---

## Verification checklist (before you finish)

- [ ] Every documented member exists in the decompiled output (no invented API).
- [ ] Code examples are mentally compilable against the real signatures.
- [ ] Each skill has valid YAML frontmatter (`name` + `description`).
- [ ] One skill per control family; folder names are kebab-case.
- [ ] All examples comply with the `winforms-designer-code` rules.
- [ ] DarkMode/High-DPI and gotcha notes included where applicable.
- [ ] The full public surface of both packages is covered across the skills.

---

## Baseline type inventory

Captured from a specific restored build (base `WarpToolkit.WinForms`
`0.9.86-preview.g8bc92e2168`, `WarpToolkit.WinForms.Specialized`
`0.9.123-preview.gddb5c178cc`). **Re-verify against the currently restored
version** — versions move via `WarpToolkitVersion`.

### `WarpToolkit.WinForms`

Namespaces: `WarpToolkit.WinForms`, `.Containers`, `.Containers.Adorners`,
`.Containers.Wizard`, `.Controls`, `.Experimental.Binding`,
`.Experimental.CommandTypes`, `.Extensions.AutoView`, `.Grid`, `.Symbols`,
`WinForms.PowerTools.Controls`.

Public types:

- Controls / containers: `FluentTabControl : Panel`, `AdornerPanel : Panel`,
  `AdornerTableLayoutPanel : TableLayoutPanel`, `TransparentPanel : Control`,
  `FilePathPicker : Control`, `BindableComboBox : ComboBox`,
  `FloatingPointSlider : TrackBar`, `FloatingPointTrackBar : TrackBar`,
  `EditFloatingPointSlider : ContainerControl`, `GridView : DataGridView`,
  `WizardContainer : ContainerControl`, `WizardPage`, `FluentMessageBoxForm : Form`.
- Grid support: `GridViewExtension` (static), `GridViewItemTemplate` (abstract),
  `GridViewItemTemplateConverter`, `GridViewItemTemplateWrapper`.
- Commands / MVVM: `UiCommandBase`, `FormCommand`, `MessageBoxCommand`,
  `CommandTypeProvider`, `IUiCommand`, `UiCommandInstanceConverter`,
  `UiCommandTypeConverter`, `RequestCanExecuteEventArgs`,
  `CreateDefaultStyleInstanceEventArgs`.
- Binding / AutoView: `AutoViewBase<TControl>`, `AutoViewControlMappingBase`,
  `TypeToControlFactoryBase` (abstract), `StringToTextBoxFactory`,
  `FormsTypeConverter`, `ValueNotFoundException`.
- Messaging: `FluentMessageBox` (static).
- Symbols: `FluentSymbols` (static).
- Wizard event args: `WizardPageEventArgs`, `WizardPageLeavingEventArgs`,
  `WizardPageValidatingEventArgs`.
- Enums: `FilePathPickerMode`, `AdornerBorderStyle`, `TransparencyStyle`,
  `WizardContainerScalingMode`, `WizardDialogResult`, `WizardNavigationDirection`,
  `VerticalContentAlignments`, `AutoPropertyApplySettings`, `AllSymbols`,
  `CommonToolStripSymbols`, `DevelopmentSymbols`, `TreeViewSymbols`.

### `WarpToolkit.WinForms.Specialized`

Public types:

- `ConsoleControl` (+ enums `ConsoleChannel`, `ConsoleMessageKind`).
- `WarpDataGridView : DataGridView` (+ enum `AlternatingRowColorMode`).
- `HexAsciiDumper` (+ enums `DataFormat`, `ShowText`).
- `FilenameDisambiguator` (+ enum `GenerationStrategy`).
- Enums: `CustomFontStyle`, `FontSize`.
