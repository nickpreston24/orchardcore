using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CodeMechanic.Advanced.Regex;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.FileSystem;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;

namespace OrchardCore.Services;

/// <summary>
/// This is a sample service to demonstrate my parsing and library-creating abilites
/// </summary>
public class SampleMarkdownService : ISampleMarkdownService
{
    private readonly EmbeddedResourceService embedsvc;

    public SampleMarkdownService(IEmbeddedResourceQuery embeds)
    {
        string local_readme = "README.md";
        embedsvc =
            (EmbeddedResourceService?)embeds; // the embedded resource service magically reads our embedded files defind in our .csproj.
    }

    public MarkdownHeader[] GetHeadersFromFile()
    {
        var all_markdown_headers = new Grepper() { FileSearchMask = "*.md", FileSearchLinePattern = "^#*" }
                .GetMatchingFiles() // returns IEnumerable<GrepResult>
                // .Dump("debug") //  dumps as JSON to console, then returns T as-is. great for debugging in prod.
                .Select(file => File.ReadAllText(file.FilePath)) // Read all text (duh)
                .Select(text => text.Extract<MarkdownHeader>(MarkdownHeader.HeaderPattern))
                .Flatten()
            ;
        return all_markdown_headers.ToArray();
    }
}

public class MarkdownHeader
{
    public string Pounds { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public static string HeaderPattern = @"(?<=(?<pounds>^#{1,6})\\s)(?<text>.*) # https://regex101.com/r/S8sluj/1";
    public static Regex regexp = new Regex(MarkdownHeader.HeaderPattern);
}