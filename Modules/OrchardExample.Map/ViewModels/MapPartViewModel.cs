using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardExample.Map.Models;

namespace OrchardExample.Map.ViewModels
{
    public class MapPartViewModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [BindNever] public ContentItem ContentItem { get; set; }
        [BindNever] public MapPart MapPart { get; set; }

        public bool Show { get; set; }
    }


    // public class MapPartViewModel
    // {
    //     public string MySetting { get; set; }
    //     [BindNever]
    //     public ContentItem ContentItem { get; set; }
    //
    //     [BindNever]
    //     public MapPart MapPart { get; set; }
    //
    //     [BindNever]
    //     public MapPartSettings Settings { get; set; }
    // }
}