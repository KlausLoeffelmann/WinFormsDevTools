namespace WinFormsToolLib;

public class PrintableTextBox : TextBox
{
    public PrintableTextBox() 
    {
        ReadOnly = true;
        Multiline = true;
        ScrollBars = ScrollBars.Both;
    }

    public async Task PrintAsync(string? message)
    {
        if (!IsHandleCreated)
        {
            return;
        }

        TaskCompletionSource taskCompletionSource = new();

        BeginInvoke(() =>
        {
            try
            {
                AppendText(message);
                SelectionStart = Text.Length - 1;
                ScrollToCaret();

                taskCompletionSource.SetResult();
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
        });

        await taskCompletionSource.Task;
    }

    public Task PrintLineAsync(string? message)
    {
        if (message is null)
        {
            message = "\r\n";
        }
        else
        {
            message += "\r\n";
        }

        return PrintAsync(message);
    }
}
