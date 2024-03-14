using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OrchardExample.Map.Settings
{
    public class MapPartSettingsViewModel
    {
        public string MySetting { get; set; }

        [BindNever]
        public MapPartSettings MapPartSettings { get; set; }
    }
}
