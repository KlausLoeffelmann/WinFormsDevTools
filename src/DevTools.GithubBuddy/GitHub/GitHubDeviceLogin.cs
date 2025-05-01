using System.Diagnostics;
using System.Text.Json;

namespace DevTools.GitHubBuddy.GitHub;

public static class GitHubDeviceLogin
{
    private const string DeviceCodeUrl = "https://github.com/login/device/code";
    private const string AccessTokenUrl = "https://github.com/login/oauth/access_token";
    private static readonly HttpClient Http = new();

    public static async Task<string?> LoginAsync(string clientId, string scope = "repo read:user")
    {
        FormUrlEncodedContent request = new FormUrlEncodedContent(
        [
          new KeyValuePair<string, string>("client_id", clientId),
          new KeyValuePair<string, string>("scope", scope)
        ]);

        HttpResponseMessage response = await Http.PostAsync(DeviceCodeUrl, request);
        response.EnsureSuccessStatusCode();

        JsonDocument json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        JsonElement root = json.RootElement;

        string? deviceCode = root.GetProperty("device_code").GetString();
        string? userCode = root.GetProperty("user_code").GetString();
        string? verificationUri = root.GetProperty("verification_uri").GetString();
        int interval = root.GetProperty("interval").GetInt32();

        Debug.Assert(deviceCode != null, "deviceCode is null");
        Debug.Assert(userCode != null, "userCode is null");
        Debug.Assert(verificationUri != null, "verificationUri is null");

        // Launch browser
        _ = Process.Start(new ProcessStartInfo
        {
            FileName = verificationUri,
            UseShellExecute = true
        });

        MessageBox.Show($"Please go to the browser and enter the following code:\n\n{userCode}", "GitHub Login");

        FormUrlEncodedContent pollData = new FormUrlEncodedContent(
        [
          new KeyValuePair<string, string>("client_id", clientId),
          new KeyValuePair<string, string>("device_code", deviceCode),
          new KeyValuePair<string, string>("grant_type", "urn:ietf:params:oauth:grant-type:device_code")
        ]);

        while (true)
        {
            await Task.Delay(interval * 1000);

            HttpResponseMessage pollResponse = await Http.PostAsync(AccessTokenUrl, pollData);
            JsonDocument pollJson = JsonDocument.Parse(await pollResponse.Content.ReadAsStringAsync());
            JsonElement pollRoot = pollJson.RootElement;

            if (pollRoot.TryGetProperty("access_token", out JsonElement tokenProp))
                return tokenProp.GetString();

            if (pollRoot.TryGetProperty("error", out JsonElement errorProp))
            {
                string? error = errorProp.GetString();
                if (error is "access_denied" or "expired_token")
                    return null;

                // Otherwise: authorization_pending, slow_down, etc.
            }
        }
    }
}
