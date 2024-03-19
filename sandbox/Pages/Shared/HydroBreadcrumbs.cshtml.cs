using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orchard.Sandbox.Pages.Shared;

[HtmlTargetElement("breadcrumbs")]
public class HydroBreadcrumbs : HydroView
{
    public string RazorRoute { get; set; } = "";
    public string CurrentBreadcrumbPath { get; } // Todo : render, given the current page
}