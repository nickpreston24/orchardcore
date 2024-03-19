using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orchard.Sandbox.Pages.Components;

[HtmlTargetElement("hydro-nav")]
public class HydroNav : HydroView
{
    public object [] Links { get; set; }
}