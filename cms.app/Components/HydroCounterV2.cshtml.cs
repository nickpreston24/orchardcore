using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sample.Calendar.Pages.Components;

public class HydroCounterV2 : HydroComponent
{
    public int Count { get; set; }

    public void Add()
    {
        Count++;
    }
}