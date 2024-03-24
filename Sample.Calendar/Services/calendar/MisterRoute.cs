using System.Linq;
using System.Text.RegularExpressions;
using CodeMechanic.Advanced.Regex;

namespace OrchardCore.Models;

public record MisterRoute
{
    public string page_name { get; set; } = string.Empty;
}

public static class RouteExtensions
{
    // https://regex101.com/r/N1D2a9/1
    public static string GetPageFromRoute(this string url) => url
        .Extract<MisterRoute>(@"\/(?<page_name>\w+)\/?", options: RegexOptions.Singleline)
        .Select(x => x.page_name)
        .FirstOrDefault();
}