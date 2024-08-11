namespace WinFormsToolLib;

internal static class FileInfoExtension
{
    public static async Task<string> CopyToAsync(this FileInfo fileInfo, string destinationFilename, bool overwrite = true)
    {
        FileInfo destinationFile = new(destinationFilename);
        FileStream sourceStream = null!;
        FileStream destinationStream=null!;

        try
        {
            if (destinationFile.Exists)
            {
                destinationFile.Delete();
            }

            sourceStream = new FileStream(
                fileInfo.FullName,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                options: FileOptions.Asynchronous | FileOptions.SequentialScan);

            destinationStream = new FileStream(
                destinationFilename,
                overwrite ? FileMode.Create : FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                FileOptions.Asynchronous | FileOptions.SequentialScan);

            await sourceStream.CopyToAsync(destinationStream);
            await destinationStream.FlushAsync();

            return "OK.";
        }
        catch (Exception ex)
        {
            return $"\n\nThe attempt to copy the file {fileInfo.FullName} caused an exception:\n{ex.Message}.\n\n";
        }
        finally
        {
            sourceStream?.Close();
            destinationStream?.Close();
            sourceStream?.Dispose();
            destinationStream?.Dispose();
        }
    }
}
