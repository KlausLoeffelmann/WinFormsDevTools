using System.Text;

namespace WinFormsToolLib
{
    public class CommandBatch
    {
        private bool _showCommandBatchWindow;
        private bool _dryRun;
        private bool _batchStarted;

        private CommandBatchForm? _commandBatchWindow;
        private StringBuilder? _protocolStorage;
        private bool _newline = true;

        public void StartBatch(
            bool showCommandBatchWindow = true, 
            bool dryRun = false, 
            string? windowTitle=null)
        {
            if (_batchStarted)
            {
                throw new ArgumentException("Batch has already started and cannot be started twice.");
            }

            _batchStarted = true;
            _showCommandBatchWindow = showCommandBatchWindow;
            _dryRun = dryRun;

            if (_showCommandBatchWindow)
            {
                _commandBatchWindow = new CommandBatchForm(windowTitle);
                _commandBatchWindow.StartBatch();
            }

            _protocolStorage = new();
        }

        public string EndBatch(string? endOfBatchComment)
        {
            if (!string.IsNullOrEmpty(endOfBatchComment))
            {
                InfoPrintLine(endOfBatchComment);
            }

            _batchStarted = false;
            _commandBatchWindow?.EndBatch();
            return _protocolStorage!.ToString();
        }

        private void CheckBatchStarted()
        {
            if (!_batchStarted)
            {
                throw new ArgumentException("Cannot execute commands when the batch has not started!");
            }
        }

        public void CopyFileCommand(FileInfo sourceFile, DirectoryInfo destinationDirectory, bool overrideIfExist = false)
        {
            CopyFileCommand(
                sourceFile,
                new FileInfo($"{destinationDirectory.FullName}\\{sourceFile.Name}"),
                overrideIfExist);
        }

        public void CopyFileCommand(FileInfo sourceFile, FileInfo destinationFile, bool overrideIfExist = false)
        {
            CheckBatchStarted();
            if (sourceFile.Exists)
            {
                if (destinationFile.Exists)
                {
                    InfoPrint($"Copying [{sourceFile}] - destination file [{destinationFile}] exists! ");
                    if (overrideIfExist)
                    {
                        InfoPrint($"--> Overwriting... ");
                        if (!_dryRun)
                        {
                            sourceFile.CopyTo(destinationFile.FullName,overwrite: true);
                        }

                        InfoPrintLine($"DONE.");
                    }
                    else
                    {
                        InfoPrintLine($"--> SKIPPING.");
                    }
                }
                else
                {
                    InfoPrint($"Copying [{sourceFile}] to [{destinationFile}] ... ");
                    if (!_dryRun)
                    {
                        sourceFile.CopyTo(destinationFile.FullName);
                    }

                    InfoPrintLine($"DONE.");
                }
            }
            else
            {
                InfoPrintLine($"Source file [{sourceFile}] does NOT exists. --> SKIPPING.");
            }
        }

        private string MessageHeader(string? message)
        {
            message ??= String.Empty;

            if (_newline)
            {
                message = $"[{DateTime.Now:(mm/ddd) HH:mm:ss ff}]: " + message;
                _newline = false;
            }

            return message;
        }

        public void InfoPrint(string? message)
        {
            message = MessageHeader(message);

            _commandBatchWindow?.Print(message);

            _protocolStorage!.Append(message);
        }

        public void InfoPrintLine(string? message)
        {
            message = MessageHeader(message);

            _commandBatchWindow?.PrintLine(message);

            _protocolStorage!.Append(message + "\r\n");
            _newline = true;
        }
    }
}
