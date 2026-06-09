# HighDPI scaling regression — DevTools.RuntimeDeploy

## Summary

After the modernization work on `DevTools.RuntimeDeploy`, the main window renders
incorrectly on HighDPI displays: header text is clipped, the path-shortcut labels
overlap their textboxes, textbox contents are cut off, and controls overflow their
containers.

The root cause is **not** the DPI mode or the runtime font. The app set
**`AutoScaleMode.Font` on every container** — the form *and* each hosted UserControl
(`OverView`, `DeployRuntimeView`, `PathShortCutControl`). Under `Font` autoscaling,
each container **independently** computes its own scale factor against its own
serialized `AutoScaleDimensions` baseline. That independence is the whole bug
surface; it manifested in two ways:

1. A **design-time baseline mismatch** between `MainForm` `(14F, 36F)` and the
   UserControls it hosts `(12F, 30F)`, introduced by a designer re-serialization
   commit.
2. The hosted views being **parented to the tab control in the constructor** (before
   the form's handle/scaling context exists), so they auto-scaled once, too early,
   and desynced from the form's own scaling pass.

**Final fix:** set the nested UserControls to **`AutoScaleMode.Inherit`** so they no
longer self-scale but are scaled *as one unit* by the form's single cascade. This
dissolves both problems at the source — only the form's baseline matters, and the
add-timing becomes irrelevant. See "Final fix" below.

## Background: how `AutoScaleMode.Font` works

Every container in this app uses `AutoScaleMode.Font`. With font autoscaling, WinForms
records the font dimensions present **at design time** in `AutoScaleDimensions`. At
runtime it measures the *actual* ambient font dimensions and scales each container's
children by:

```
scaleFactor = runtimeFontDimensions / AutoScaleDimensions
```

For a form that hosts UserControls, the controls are scaled relative to **their own**
`AutoScaleDimensions`. This produces correct layout **only when the form and all hosted
controls were authored against the same design-time baseline** (same font, same
design-time DPI). If the form's baseline differs from its children's, the reparented
children get an extra, unintended scale step.

## Root cause

Commit `b63704c` ("Designer cleanup: drop project-wide ApplicationDefaultFont")
removed the `<ApplicationDefaultFont>Segoe UI, 11pt</ApplicationDefaultFont>` project
property and re-serialized `MainForm.Designer.cs`. That re-serialization happened on a
machine at a **different design-time DPI**, so the designer recaptured `MainForm` with:

- `AutoScaleDimensions = (14F, 36F)` (was `(12F, 30F)`), and
- a pinned `Font = new Font("Segoe UI", 11.1428576F, ...)` (instead of `11F`).

Every child control, however, still carries the original `(12F, 30F)` baseline:

| Component             | AutoScaleDimensions | Font                  |
|-----------------------|---------------------|-----------------------|
| `MainForm` (regressed)| `(14F, 36F)`        | Segoe UI 11.1428576pt |
| `OverView`            | `(12F, 30F)`        | Segoe UI 11pt         |
| `DeployRuntimeView`   | `(12F, 30F)`        | ambient 11pt          |
| `PathShortCutControl` | `(12F, 30F)`        | ambient 11pt          |

### Why this clips the UI

When `OverView` / `DeployRuntimeView` are reparented into `MainForm`'s tab control,
the `(14, 36)` vs `(12, 30)` mismatch applies an extra factor of roughly
`14/12 ≈ 1.17` horizontally and `36/30 = 1.2` vertically to the hosted content. The
labels, textboxes and list views grow larger than the space allotted, so text is
clipped and the absolutely-positioned `PathShortCutControl` labels collide with their
adjacent textboxes — exactly the symptoms observed.

The fact that the horizontal and vertical ratios differ (`1.17` vs `1.20`) is itself
proof that `(14, 36)` is a DPI round-trip artifact rather than a genuine font
measurement: a real font baseline scales both dimensions by the same factor from
`(12, 30)`. The pinned `11.1428576pt` font size is the same artifact expressed as a
font size.

### Pre-regression state

At `91661b0~1` (immediately before the cleanup commit), `MainForm` was internally
consistent with its children:

```
AutoScaleDimensions = new SizeF(12F, 30F);
ClientSize          = new Size(1429, 893);
MinimumSize         = new Size(1339, 722);
```

## Final fix — `AutoScaleMode.Inherit` on the hosted UserControls

The decisive fix is to stop the nested UserControls from scaling themselves. Set:

```csharp
// OverView.Designer.cs, DeployRuntimeView.Designer.cs
AutoScaleMode = AutoScaleMode.Inherit;   // was AutoScaleMode.Font
```

> Note: a third UserControl, `PathShortCutControl`, was also originally switched to
> `Inherit`. It has since been **replaced entirely** by WarpToolkit's
> `FilePathPicker` (`WarpToolkit.WinForms.Controls`), a `Control`-derived picker that
> does its own internal DPI-aware layout (it overrides `OnFontChanged`/`OnLayout`), so
> it does not participate in `AutoScaleMode`-based scaling at all.

With `Inherit`, a UserControl does **not** compute its own factor from its own
`AutoScaleDimensions`; it is scaled **as one unit** by its parent's scale pass. Only
the root `MainForm` (still `AutoScaleMode.Font`) drives scaling, so:

- the per-control baseline values become irrelevant — the `(14,36)` vs `(12,30)`
  mismatch can no longer desync anything, and
- the moment the views are parented no longer matters — there is no early one-shot
  self-scale to mistime.

This is the correct WinForms design for composed UserControls and resolves the
regression at its source.

### Supporting cleanup on `MainForm`

`MainForm` is the scaling root and stays `AutoScaleMode.Font`, so its own baseline is
restored to be internally consistent with its design font (`Segoe UI, 11pt`, matching
the runtime default set in `Program.cs` via `UseDefaultFont`). The desirable
`DockStyle.Fill` change for the tab control from the cleanup commit is kept.

Edits to `MainForm.Designer.cs`:

| Property                       | Before                         | After                |
|--------------------------------|--------------------------------|----------------------|
| `AutoScaleDimensions`          | `(14F, 36F)`                   | `(12F, 30F)`         |
| Form `Font`                    | `Segoe UI, 11.1428576F`        | `Segoe UI, 11F`      |
| `ClientSize`                   | `(1256, 736)`                  | `(1429, 893)`        |
| `MinimumSize`                  | `(1280, 800)`                  | `(1339, 722)`        |
| Form `Margin`                  | `(4, 5, 4, 5)`                 | `(3, 4, 3, 4)`       |
| Form `Padding`                 | `(5)`                          | removed              |
| `_tabControl.Margin`           | `(4, 4, 4, 4)`                 | `(3, 4, 3, 4)`       |

### Note on add-timing

An interim experiment moved the `AddTab` calls from the constructor into `OnLoad` to
avoid an early one-shot auto-scale of the (then `Font`-mode) views. Once the views use
`Inherit`, that early self-scale no longer exists, so the workaround is unnecessary
and was reverted — the `AddTab` calls remain in the constructor.

The application is configured as `SystemAware` (both `<ApplicationHighDpiMode>` in the
`.csproj` and `UseHighDpiMode(HighDpiMode.SystemAware)` in `Program.cs`). This is left
unchanged: the regression is independent of the DPI mode. Migrating to `PerMonitorV2`
is a possible future modernization step but is out of scope for this fix.

## Validation

- `dotnet build src\DevTools.RuntimeDeploy\DevTools.RuntimeDeploy.csproj` succeeds.

### Empirical real-DPI measurement

The fix was validated against **real** DPI on a 150%-scaled display by spinning up a
throwaway WinForms harness that mirrors the app's structure (root form
`AutoScaleMode.Font`, baseline `(12,30)`/Segoe UI 11pt, hosting a nested UserControl
with a `TableLayoutPanel` of label/textbox/button — the `PathShortCutControl` shape).

The harness selects its DPI awareness *programmatically at process start* via
`SetProcessDpiAwarenessContext`, so both 100% and 150% are observable from one machine:

- **DPI-Unaware** → the process sees `DeviceDpi = 96` (DWM bitmap-stretches it).
- **System-Aware** (what the app uses) → the process sees the display's real
  `DeviceDpi = 144` (150%).

The nested child was measured in both `AutoScaleMode.Font` and `AutoScaleMode.Inherit`
(design height = 50px):

| DPI         | child mode | child.Height | resulting factor |
|-------------|------------|-------------:|-----------------:|
| 96 (100%)   | `Font`     | 25           | 0.50 ❌          |
| 96 (100%)   | `Inherit`  | 33           | 0.667 ✅         |
| 144 (150%)  | `Font`     | 42           | 0.84 ❌          |
| 144 (150%)  | `Inherit`  | 50           | 1.00 ✅          |

`Inherit` tracks the parent's factor exactly (clean `0.667` at 100%, native `1.0` at
150%). `Font` independently computes a **different, wrong** factor at every DPI — at the
app's real 150% it renders the child 42px tall instead of 50 (8px short), which is the
clipping/overlap seen in the original screenshot. This is direct empirical confirmation
that `Inherit` is the correct fix.

### Note on the `(12, 30)` baseline

The serialized baseline `(12, 30)` is Segoe UI 11pt measured at **144 DPI (150%)**, not
96 DPI — at 96 DPI the same font measures `(8, 20)` (`8×1.5 = 12`, `20×1.5 = 30`). In
other words the Designer last serialized this project at 150% scaling. That is valid for
`AutoScaleMode.Font` *as long as every container shares it*, which is exactly why the
MainForm drift to the non-uniform `(14, 36)` (`14/12 ≠ 36/30`) broke layout — and a
further reason to take the nested UserControls out of the baseline-matching game with
`Inherit`.

## Takeaway

For composed WinForms UIs, only the **root form** should use `AutoScaleMode.Font`;
hosted/nested UserControls should use `AutoScaleMode.Inherit` so the whole tree scales
as a single unit. Mixing `Font` at every level invites baseline drift and
ordering-dependent self-scaling bugs like this one.
