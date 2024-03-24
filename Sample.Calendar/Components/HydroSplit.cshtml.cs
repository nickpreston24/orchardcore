using CodeMechanic.Types;
using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OrchardCore.Components;

[HtmlTargetElement("split")]
public class HydroSplit : HydroView
{
    public SplitOrientation Orientation { get; set; } = SplitOrientation.Horizontal;
    public uint gap { get; set; }
}

public class SplitOrientation : Enumeration
{
    public static SplitOrientation Vertical = new SplitOrientation(1, nameof(Vertical));
    public static SplitOrientation Horizontal = new SplitOrientation(2, nameof(Horizontal));

    public SplitOrientation(int id, string name) : base(id, name)
    {
    }
}