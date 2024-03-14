using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orchard.Sandbox.Services;

namespace Orchard.Sandbox.Pages.Sandbox;

public class PartEditForm : HydroComponent
{
    public Part EditedPart { get; set; } = new();
}