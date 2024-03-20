using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Orchard.Sandbox.Pages.Shared;

[HtmlTargetElement("htmx-button")]
public class HtmxButton : HydroView
{
    public string Text { get; set; } = "Submit";
    public string HxEvent { get; set; } = "hx-get";
    public string HxTarget { get; set; } = "";
    public string HxPage { get; set; } = "Index";
    public string HxPageHandler { get; set; } = "";
    public string HxSwap { get; set; } = "outerHTML";
    public string classname { get; set; } = "btn btn-ghost";
}