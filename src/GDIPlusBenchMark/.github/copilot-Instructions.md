## 1. WinForms-Specific Guidelines

### 1.1 Changing Designer (Code-Behind) files.

When generally asked to change the Design of a Form or a UserControl without any specifics, we need to make the changes in the Code-Behind file, so it exists.
Usually, when I have a Form named `MyForm.cs`, the Designer will create a code-behind file named `MyForm.Designer.cs` and put the designer code in there in a method which is called `InitializeComponent`.
If it is a designable UserControl named `MyUserControl.cs`, the Designer will create a code-behind file named `MyUserControl.Designer.cs` and put the designer code in there in a method which is called `InitializeComponent`.

Here are the rules for adding new controls to such Forms/UserControls or ask for refactorings:
- When a new control is added, usually also a new field is created in the code-behind file.
- New fields are inserted _at the end_ of the existing fields at the very bottom of the code-behind file.
- In Visual Basic, those fields are usually declared as `Friend`, in C# as `private`.

- Never add other code than instantiation, initialization, field assignments (without arithmetic or other complex operator logic), since the Designer needs to parse the code and is very limitted in what it can process.
- Do not use Lambda expressions or any other complex code in the Designer code.
- Do not add any code that is not related to setup controls or fields.
- Do not use AddHandler for VB in the Designer code. Rather, if you need to wire up a new EventHandler, use the Visual Basic `Handles` keyword.
- If asked for tasks, which would include calculation like "Place a button at the center of the form", do not try to do that by using a calculation in the Designer code. Rather try to find a solution using a combination of controls or do it with an approximation. Example approaches would be:
  - If the Form/UserControl already has a series of controls, and helper controls like a new Layout Panel to achive the task would collide, just try to place the button approximately in the middle of the Form, by taken the current Form's size and the desired Button's size into account, and just hard code the Position and Size.
  - Otherwise, try to improvise to a good solution by utilizing other controls, but within reason. For example:
    - Use a TableLayoutPanel with a single cell to center the button, by not anchoring the Button at all.
  
### 1.2 Event Handling

- **Make sure to use/generate/refactor nullable event handlers in C# consistently**
  ```csharp
  public event EventHandler<EventArgs>? Click;
  
  protected virtual void OnClick(EventArgs e)
  {
      Click?.Invoke(this, e);
  }
  ```

- **Use `EventArgs.Empty` for empty event arguments**
  ```csharp
  protected virtual void OnPaint(PaintEventArgs e)
  {
      Paint?.Invoke(this, EventArgs.Empty);
  }
  ```

- Take into account, that the typical Event handler signature has been modified for NRT:
    ```csharp
    private void OnClick(object? sender, EventArgs e) { ... }
    ```
