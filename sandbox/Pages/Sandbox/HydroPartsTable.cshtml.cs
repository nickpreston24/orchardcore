using System.Collections;
using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orchard.Sandbox.Services;

namespace Orchard.Sandbox.Pages.Sandbox;

public class HydroPartsTable : HydroComponent
{
    public List<Part> Parts { get; set; } = new(0);
    // public int Count { get; set; } = 0;
}