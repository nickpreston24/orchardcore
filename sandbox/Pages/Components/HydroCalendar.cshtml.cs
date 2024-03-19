using Hydro;

namespace Orchard.Sandbox.Pages.Components;

public class HydroCalendar : HydroComponent
{
    public DateTime InitialDate { get; set; } = DateTime.UtcNow;
}