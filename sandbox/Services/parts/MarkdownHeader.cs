using System.Text.RegularExpressions;
using CodeMechanic.Types;

namespace Orchard.Sandbox.Services;

public record MarkdownHeader
{
    public string RawLine { get; set; } = string.Empty; // the entire matched line
    public string Header { get; set; } = string.Empty; // entire matched header

    public int HeaderLength =>
        Header.NotEmpty() ? Header.Length : 0; // easy computed property for UI or whatever we want.

    // We can refactor this into a flyweight pattern, or whatever.   :)
    public static Regex HeaderPattern = new Regex("", RegexOptions.Compiled | RegexOptions.Multiline);
}