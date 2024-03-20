using Hydro;

namespace Orchard.Sandbox.Pages.Components;

public class HydroCalendar : HydroComponent
{
    public DateTime InitialDate { get; set; } = DateTime.UtcNow;
    public int Count { get; set; }
    public bool edits_enabled { get; set; }

    public void Update(string newValue)
    {
        // Console.WriteLine(newValue);
    }
    
    public void Set(int newValue)
    {
        Count = newValue;
    }

    public void Add()
    {
        Console.WriteLine(nameof(Add));
    }
}