using System.Diagnostics;

namespace WinFormsToolLib
{
    public class PrintableTextBox : TextBox
    {
        public PrintableTextBox() 
        {
            ReadOnly = true;
            Multiline = true;
            ScrollBars = ScrollBars.Both;
        }

        public void Print(string? message)
        {
            Text += message;
            SelectionStart=Text.Length-1;
        }

        public void PrintLine(string? message)
        {
            if (message is null)
            {
                message = "\r\n";
            }
            else
            {
                message += "\r\n";
            }

            Print(message);
        }
    }
}
