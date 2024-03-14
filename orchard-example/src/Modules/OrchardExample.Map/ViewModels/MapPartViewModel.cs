using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardExample.Map.Models;
using OrchardExample.Map.Settings;

namespace OrchardExample.Map.ViewModels
{
    public class MapPartViewModel
    {
        public string MySetting { get; set; }

        public bool Show { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public MapPart MapPart { get; set; }

        [BindNever]
        public MapPartSettings Settings { get; set; }
    }
}
