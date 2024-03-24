using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OrchardCore.Components;

[HtmlTargetElement("hydrosection")]
public class HydroSection : HydroView
{
    public bool Hidden { get; set; }
}