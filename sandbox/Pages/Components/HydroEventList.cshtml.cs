using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orchard.Sandbox.Services;

namespace Orchard.Sandbox.Pages.Components;

public class HydroEventList : HydroComponent
{
    public List<CalendarEvent> CalendarEvents { get; set; } = new List<CalendarEvent>();

    public void Add()
    {
    }

    public void Delete()
    {
    }
}