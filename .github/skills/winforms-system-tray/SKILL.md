---
name: winforms-system-tray
description: Guide for WinForms main forms that minimize or close into the Windows notification area (SysTray), including NotifyIcon menus, restore/quit behavior, ShowInTaskbar handle recreation order, and persisted bounds/state.
---

# WinForms System Tray Main Form Guide

Use this skill when a WinForms app should hide its main form in the Windows
notification area (SysTray) when the user minimizes or closes the form.

This is a plain WinForms skill, not a WARP skill.

## Planning rule

When planning a tray integration, always ask the user which behavior they want:

1. **Close to tray** - clicking the form close button hides the main form and keeps the app running.
2. **Minimize to tray** - minimizing the form hides the main form and keeps the app running.
3. **Both** - close and minimize both hide the main form.

The rest of the implementation depends on this choice. In particular, only add
a `File` / `Quit` menu item when the user agreed that closing the form should
go to the tray. Do not add it for a minimize-only tray behavior.

## Implementation approaches

### Close to tray

Override `OnFormClosing`, cancel only user-initiated closes, and route them to
the tray. Keep a private flag for real application shutdown:

```csharp
private bool _allowClose;

protected override void OnFormClosing(FormClosingEventArgs e)
{
    base.OnFormClosing(e);

    if (!_allowClose && e.CloseReason == CloseReason.UserClosing)
    {
        e.Cancel = true;
        InvokeAsync(() => MinimizeToTray());
        return;
    }

    SaveBounds();
    _notifyIcon.Visible = false;
}

private void Quit()
{
    _allowClose = true;
    Close();
}
```

Use this when the close button should mean "keep running in the tray" and a
separate Quit command should mean "exit the app".

### Minimize to tray

Handle `OnResize`, `OnSizeChanged`, or a `Resize` event and route only the
minimized state to the tray:

```csharp
protected override void OnResize(EventArgs e)
{
    base.OnResize(e);

    if (WindowState == FormWindowState.Minimized)
    {
        MinimizeToTray();
    }
}
```

Use this when the close button should still close the app normally.

### Both close and minimize to tray

Combine the two approaches, but keep one shared `MinimizeToTray` and one shared
`RestoreFromTray` method so the `NotifyIcon`, taskbar visibility, saved bounds,
and hidden-state persistence all stay consistent.

## Critical hide/restore order

`ShowInTaskbar` can recreate the form handle when its value changes. To avoid
taskbar pinning, focus, and flicker problems, hide the form first, then change
taskbar participation while the form is already hidden:

```csharp
private void MinimizeToTray()
{
    SaveBoundsBeforeTrayHide();

    Hide();
    _notifyIcon.Visible = true;

    // Changing ShowInTaskbar can recreate the handle; do it after Hide().
    ShowInTaskbar = false;

    WindowState = FormWindowState.Minimized;
}
```

Restore in the opposite direction: put the form back into the taskbar before
showing it, then restore the window state and activate it:

```csharp
private void RestoreFromTray()
{
    ShowInTaskbar = true;
    Show();

    WindowState = FormWindowState.Normal;
    _notifyIcon.Visible = false;
    Activate();
}
```

## NotifyIcon behavior

A close-to-tray main form should have:

- A `NotifyIcon` with `Visible = false` by default.
- A `ContextMenuStrip` for tray commands.
- A normal click handler that shows the context menu at the cursor position.
- A double-click handler that restores the main form.
- One shared Quit command used by the tray menu and any main-menu Quit item.

Recommended tray context menu:

```text
Restore {FormName}
Options...
----
Quit
```

Only include **Options...** when the form already has a command or service that
opens an options dialog modally. Wire it to the same command instead of creating
a separate options code path.

Example handlers:

```csharp
private void NotifyIcon_DoubleClick(object? sender, EventArgs e)
    => RestoreFromTray();

private void NotifyIcon_MouseClick(object? sender, MouseEventArgs e)
{
    if (e.Button == MouseButtons.Left)
    {
        _trayContextMenu.Show(Cursor.Position);
    }
}

private void RestoreMenuItem_Click(object? sender, EventArgs e)
    => RestoreFromTray();

private void QuitMenuItem_Click(object? sender, EventArgs e)
    => Quit();
```

## Persisting bounds and tray state

If the main form stores position settings, never save minimized tray bounds as
the restored form size. Save the normal bounds before hiding to the tray:

```csharp
private void SaveBoundsBeforeTrayHide()
{
    Rectangle bounds = WindowState == FormWindowState.Normal
        ? Bounds
        : RestoreBounds;

    SaveMainFormBounds(bounds);
}
```

Persist whether the app was hidden in the tray when it exited. On the next
startup, restore the saved normal bounds first. If the saved state says the app
was in the tray, then start hidden in the tray again using the same
`MinimizeToTray` ordering so taskbar and notify-icon state are correct.

## MenuStrip integration

When, and only when, close-to-tray behavior is selected:

- If the app has a `MenuStrip`, ensure a `File` menu exists.
- Add `File` / `Quit` if it is missing.
- Wire it to the same `Quit()` method as the tray menu's **Quit** item.

Do not add `File` / `Quit` for a minimize-only tray integration, because the
form close button still exits the app normally in that mode.

## Designer guidance

Put `NotifyIcon`, `ContextMenuStrip`, `ToolStripMenuItem`, and separator
component declarations in the Designer file when the form is Designer-managed.
Keep behavioral code (`MinimizeToTray`, `RestoreFromTray`, `Quit`, persistence,
and event-handler bodies) in the regular `.cs` file.
