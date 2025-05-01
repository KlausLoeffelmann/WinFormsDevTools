using Octokit;
using System.Diagnostics;
using System.Text.Json;
using Windows.Security.Credentials;
using DevTools.GitHubBuddy.Properties;

namespace DevTools.GitHubBuddy.GitHub;

public sealed class GitHubLoginManager
{
    private const string UserName = "OAuthAccessToken";
    private const string DeviceCodeUrl = "https://github.com/login/device/code";
    private const string AccessTokenUrl = "https://github.com/login/oauth/access_token";
    private static readonly HttpClient Http = new();

    static GitHubLoginManager()
    {
        Http.DefaultRequestHeaders.Accept.Clear();
        Http.DefaultRequestHeaders.Accept.ParseAdd("application/json");
    }

    public static async Task<GitHubClient?> GetClientAsync()
    {
        var token = LoadToken();
        if (token is not null)
            return CreateClient(token);

        token = await LoginAsync();
        if (token is not null)
        {
            SaveToken(token);
            return CreateClient(token);
        }

        return null;
    }

    private static GitHubClient CreateClient(string token)
      => new(new ProductHeaderValue("WinFormsGitHubExplorer"))
      {
          Credentials = new Credentials(token)
      };

    private static string? LoadToken()
    {
        try
        {
            var vault = new PasswordVault();
            var credential = vault.Retrieve(Resources.ResourceName, UserName);
            credential.RetrievePassword();

            return credential.Password;
        }
        catch
        {
            return null;
        }
    }

    private static void SaveToken(string token)
    {
        var vault = new PasswordVault();
        vault.Add(new PasswordCredential(Resources.ResourceName, UserName, token));
    }

    public static void DeleteToken()
    {
        try
        {
            var vault = new PasswordVault();
            var credential = vault.Retrieve(Resources.ResourceName, UserName);
            vault.Remove(credential);
        }
        catch
        {
            // Token not found or already deleted
        }
    }

    private static async Task<string?> LoginAsync(string scope = "repo read:user")
    {
        var request = new FormUrlEncodedContent(
        [
          new KeyValuePair<string, string>("client_id", Resources.LicenceLevelID),
          new KeyValuePair<string, string>("scope", scope)
        ]);

        var response = await Http.PostAsync(DeviceCodeUrl, request);
        response.EnsureSuccessStatusCode();

        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
        var deviceCode = json.GetProperty("device_code").GetString();
        var userCode = json.GetProperty("user_code").GetString();
        var verificationUri = json.GetProperty("verification_uri").GetString();
        var interval = json.GetProperty("interval").GetInt32();

        Debug.Assert(deviceCode != null, "deviceCode is null");

        _ = Process.Start(new ProcessStartInfo
        {
            FileName = verificationUri,
            UseShellExecute = true
        });

        MessageBox.Show($"Please complete GitHub login in the browser.\n\nCode: {userCode}", "GitHub Login");

        var pollData = new FormUrlEncodedContent(
        [
          new KeyValuePair<string, string>("client_id", Resources.LicenceLevelID),
          new KeyValuePair<string, string>("device_code", deviceCode),
          new KeyValuePair<string, string>("grant_type", "urn:ietf:params:oauth:grant-type:device_code")
        ]);

        while (true)
        {
            await Task.Delay(interval * 1000);

            var pollResponse = await Http.PostAsync(AccessTokenUrl, pollData);
            var result = JsonDocument.Parse(await pollResponse.Content.ReadAsStringAsync()).RootElement;

            if (result.TryGetProperty("access_token", out var tokenProp))
                return tokenProp.GetString();

            if (result.TryGetProperty("error", out var errorProp))
            {
                var error = errorProp.GetString();
                if (error is "access_denied" or "expired_token")
                    return null;
                // Keep polling on "authorization_pending"
            }
        }
    }
}
