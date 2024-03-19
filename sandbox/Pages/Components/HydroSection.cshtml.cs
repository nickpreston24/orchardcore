using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orchard.Sandbox.Pages.Components;

[HtmlTargetElement("hydrosection")]
public class HydroSection : HydroView
{
    public bool Hidden { get; set; }
}