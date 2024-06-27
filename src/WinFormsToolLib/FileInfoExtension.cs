namespace WinFormsToolLib;

internal static class FileInfoExtension
{
    public static async Task<string> CopyToAsync(this FileInfo fileInfo, string destinationFilename, bool overwrite = true)
    {
        try
        {
            FileInfo destinationFile = new(destinationFilename);

            if (destinationFile.Exists)
            {
                destinationFile.Delete();
            }

            using var sourceStream = new FileStream(
                fileInfo.FullName,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                options: FileOptions.Asynchronous | FileOptions.SequentialScan);

            using var destinationStream = new FileStream(
                destinationFilename,
                overwrite ? FileMode.Create : FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                FileOptions.Asynchronous | FileOptions.SequentialScan);

            await sourceStream.CopyToAsync(destinationStream);

            sourceStream.Close();
            destinationStream.Flush();
            destinationStream.Close();

            return "OK.";
        }
        catch (Exception ex)
        {
            return $"\n\nThe attempt to copy the file {fileInfo.FullName} caused an exception:\n{ex.Message}.\n\n";
        }
    }
}
