using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Hydro;

namespace Orchard.Sandbox.Pages.Components;

[HtmlTargetElement("flex")]
public class HydroFlex : HydroView
{
    public string classname => Orientation.ToString();
    public Flexies Orientation { get; set; } = Flexies.Col;
}

public class Flexies : Enumeration
{
    public static Flexies Col =
        new Flexies(1, nameof(Col), "flex flex-col items-center justify-center");

    public static Flexies Row =
        new Flexies(2, nameof(Row), "flex flex-row items-center justify-center");

    public static Flexies ColReversed =
        new Flexies(3, nameof(ColReversed), "flex flex-col-reverse items-center justify-center");

    public static Flexies RowReversed =
        new Flexies(4, nameof(RowReversed), "flex flex-row-reverse items-center justify-center");

    // TODO: needs fixing ,things are not centered 

    // public static Flexies Center =
    //     new Flexies(5, nameof(Center), "flex items-center justify-center");

    private readonly string classname;

    public Flexies(int id, string name, string classname = "flex") : base(id, name)
    {
        this.classname = classname;
    }

    public override string ToString()
    {
        return this.classname;
    }
}