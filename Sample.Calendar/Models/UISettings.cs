using System.Collections;

namespace OrchardCore;

public class UISettings
{
    public string CurrentTheme { get; set; } = "dark";
    public string[] Themes { get; set; } = { "dark", "cupcake", "emerald", "forest", "corporate" };
}