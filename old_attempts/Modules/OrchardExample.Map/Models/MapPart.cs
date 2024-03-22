using OrchardCore.ContentManagement;

namespace OrchardExample.Map.Models
{
    public class MapPart : ContentPart
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        // does this need to be here?  Example doesn't say: https://www.dotnetthailand.com/web-frameworks/orchard-core-cms/create-a-custom-orchard-core-cms-module
        public bool Show { get; set; }
    }
}