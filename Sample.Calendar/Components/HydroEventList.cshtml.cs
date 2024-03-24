using System.Collections.Generic;
using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.Services;
using OrchardCore.Services;

namespace OrchardCore.Components;

public class HydroEventList : HydroComponent
{
    public List<CalendarEvent> CalendarEvents { get; set; }// = new List<CalendarEvent>();

    public void Add()
    {
    }

    public void Delete()
    {
    }
}