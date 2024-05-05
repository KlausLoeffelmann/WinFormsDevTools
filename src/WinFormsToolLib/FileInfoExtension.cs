namespace WinFormsToolLib;

internal static class FileInfoExtension
{
    public static async Task CopyToAsync(this FileInfo fileinfo, string destinationFile, bool overwrite = true)
    {
        using var sourceStream = new FileStream(
            fileinfo.FullName,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            options: FileOptions.Asynchronous | FileOptions.SequentialScan);

        using var destinationStream = new FileStream(
            destinationFile, 
            overwrite ? FileMode.CreateNew : FileMode.CreateNew,
            FileAccess.Write, 
            FileShare.None,
            bufferSize: 4096, 
            FileOptions.Asynchronous | FileOptions.SequentialScan);

        await sourceStream.CopyToAsync(destinationStream);
    }
}
