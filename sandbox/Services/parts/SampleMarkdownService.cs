using CodeMechanic.Advanced.Regex;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.FileSystem;
using CodeMechanic.RazorHAT.Services;

namespace Orchard.Sandbox.Services;

/// <summary>
/// This is a sample service to demonstrate my parsing and library-creating abilites
/// </summary>
public class SampleMarkdownService
{
    private readonly EmbeddedResourceService embedsvc;

    public SampleMarkdownService(IEmbeddedResourceQuery embeds)
    {
        string local_readme = "README.md";
        embedsvc =
            (EmbeddedResourceService?)embeds; // the embedded resource service magically reads our embedded files defind in our .csproj.

        var all_markdown_headers = new Grepper() { FileSearchMask = "*.md", FileSearchLinePattern = "^#*" }
                .GetMatchingFiles() // returns IEnumerable<GrepResult>
                .Dump("debug") //  dumps as JSON to console, then returns T as-is. great for debugging in prod.
                .Select(file => File.ReadAllText(file.FilePath)) // Read all text (duh)
                .Select(text => text.Extract<MarkdownHeader>(MarkdownHeader.HeaderPattern))
            ;
    }
}