using DevTools.RuntimeDeploy.Cli;
using DevTools.RuntimeDeploy.Engine.Packaging;

namespace DevTools.RuntimeDeploy.Tests;

public sealed class CliArgumentParserTests
{
    [Theory]
    [InlineData("not-a-number")]
    [InlineData("-1")]
    [InlineData("999999999999999999999")]
    public void Parse_RejectsInvalidMaxDepth(string value)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => CliArgumentParser.Parse(["--max-depth", value]));

        Assert.Contains("--max-depth", exception.Message);
    }

    [Fact]
    public void Parse_ConvertsMalformedSettingsJsonToArgumentError()
    {
        string settingsPath = Path.Combine(
            AppContext.BaseDirectory,
            RuntimePatcherSettings.DefaultFileName);
        byte[]? originalContents = File.Exists(settingsPath)
            ? File.ReadAllBytes(settingsPath)
            : null;

        try
        {
            File.WriteAllText(settingsPath, "{ not-valid-json");

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => CliArgumentParser.Parse([]));

            Assert.Contains(RuntimePatcherSettings.DefaultFileName, exception.Message);
            Assert.IsType<System.Text.Json.JsonException>(exception.InnerException);
        }
        finally
        {
            if (originalContents is null)
            {
                File.Delete(settingsPath);
            }
            else
            {
                File.WriteAllBytes(settingsPath, originalContents);
            }
        }
    }
}
