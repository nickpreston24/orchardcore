using Hydro;
using Orchard.Sandbox.Services;

namespace Orchard.Sandbox.Pages.Components;

public class HydroCalendar : HydroComponent
{
    public CalendarEvent[] Events { get; set; } // = new List<CalendarEvent>().ToArray();

// public DateTime InitialDate { get; set; } = DateTime.UtcNow;
// public int Count { get; set; }
//
// public void Update(string newValue)
// {
//     // Console.WriteLine(newValue);
// }
//
// public void Set(int newValue)
// {
//     Count = newValue;
// }
//
// public void Add()
// {
//     Console.WriteLine(nameof(Add));
// }
}