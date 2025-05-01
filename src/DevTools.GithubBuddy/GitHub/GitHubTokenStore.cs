using Windows.Security.Credentials;

namespace DevTools.GitHubBuddy.GitHub;

public sealed class GitHubTokenStore
{
    private const string ResourceName = "GitHub.WinFormsApp";
    private const string UserName = "OAuthAccessToken";

    public static void SaveToken(string token)
    {
        var vault = new PasswordVault();
        vault.Add(new PasswordCredential(ResourceName, UserName, token));
    }

    public static string? LoadToken()
    {
        try
        {
            var vault = new PasswordVault();
            var credential = vault.Retrieve(ResourceName, UserName);
            credential.RetrievePassword();

            return credential.Password;
        }
        catch
        {
            return null;
        }
    }

    public static void DeleteToken()
    {
        try
        {
            var vault = new PasswordVault();
            var credential = vault.Retrieve(ResourceName, UserName);
            vault.Remove(credential);
        }
        catch
        {
            // Already deleted or not found
        }
    }
}
