using System.Runtime.InteropServices;

namespace DevTools.GitHubBuddy.GitHub;

public static class GcmGitHubCredentialHelper
{
    public static string? GetToken()
    {
        const string target = "git:https://github.com";

        if (CredRead(target, CredType.Generic, 0, out var credPtr))
        {
            var credential = (CREDENTIAL)Marshal.PtrToStructure(credPtr, typeof(CREDENTIAL))!;
            string password = Marshal.PtrToStringUni(credential.CredentialBlob, credential.CredentialBlobSize / 2)!;
            CredFree(credPtr);

            return password;
        }

        return null;
    }

    #region Native Interop

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CredRead(string target, CredType type, int reservedFlag, out nint credentialPtr);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern void CredFree(nint buffer);

    private enum CredType
    {
        Generic = 1
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct CREDENTIAL
    {
        public int Flags;
        public int Type;
        public nint TargetName;
        public nint Comment;
        public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public int CredentialBlobSize;
        public nint CredentialBlob;
        public int Persist;
        public int AttributeCount;
        public nint Attributes;
        public nint TargetAlias;
        public nint UserName;
    }

    #endregion
}