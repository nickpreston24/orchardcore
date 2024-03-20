using Hydro;

namespace Orchard.Sandbox.Pages.Shared;

public class HydroOptionsPanel : HydroComponent
{
    public DateTime InitialDate { get; set; } = DateTime.UtcNow;
    public int Count { get; set; }

    
    public void Update(string updates)
    {
        // ...
    }

}