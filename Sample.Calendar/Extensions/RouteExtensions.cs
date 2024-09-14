using System.Linq;
using System.Text.RegularExpressions;
using CodeMechanic.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace OrchardCore.Models;

public static class RouteExtensions
{
    // https://regex101.com/r/N1D2a9/1
    public static string GetPageFromRoute(this string url) => url
        .Extract<MisterRoute>(@"\/(?<page_name>\w+)\/?", options: RegexOptions.Singleline)
        .Select(x => x.page_name)
        .FirstOrDefault();


    public static string GetPageFromRoute(this HttpRequest route) => route.Path
        .ToString()
        .Extract<MisterRoute>(@"\/(?<page_name>\w+)\/?", options: RegexOptions.Singleline)
        .Select(x => x.page_name)
        .FirstOrDefault();
}