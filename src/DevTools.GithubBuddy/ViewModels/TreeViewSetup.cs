namespace DevTools.GitHubBuddy.ViewModels;

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

internal class TreeViewSetup
{
    public List<Organization> Organizations { get; set; } = [];

    public string ToJson(string filePath)
    {
        var json = JsonSerializer.Serialize(this, GetJsonSerializerOptions());
        File.WriteAllText(filePath, json);
        return json;
    }    
    
    public string ToJson(Stream stream)
    {
        var json = JsonSerializer.Serialize(this, GetJsonSerializerOptions());
        using var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
        writer.Write(json);
        writer.Flush();
        return json;
    }

    public static TreeViewSetup FromJson(FileInfo filePath)
    {
        string json = File.ReadAllText(filePath.FullName);
        return FromJson(json);
    }

    public static TreeViewSetup FromJson(Stream stream)
    {
        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        string json = reader.ReadToEnd();
        return FromJson(json);
    }

    public static TreeViewSetup FromJson(string json)
    {
        return JsonSerializer.Deserialize<TreeViewSetup>(json, GetJsonSerializerOptions()) 
            ?? new TreeViewSetup();
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            AllowTrailingCommas = true
        };
    }
}
