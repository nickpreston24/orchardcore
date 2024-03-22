using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardExample.Map.Models;
// 
using OrchardExample.Map.ViewModels;

namespace OrchardExample.Map.Drivers
{
    
    
    public class MapPartDisplayDriver : ContentPartDisplayDriver<MapPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        public MapPartDisplayDriver(IContentDefinitionManager contentDefinitionManager) =>
            _contentDefinitionManager = contentDefinitionManager;
        public override IDisplayResult Display(MapPart part, BuildPartDisplayContext context)
        {
            return Initialize<MapPartViewModel>(
                    GetDisplayShapeType(context),
                    m => BuildViewModel(m, part)
                )
                .Location("Detail", "Content:10")
                .Location("Summary", "Content:10");
        }
        public override IDisplayResult Edit(MapPart part, BuildPartEditorContext context)
        {
            return Initialize<MapPartViewModel>(
                GetEditorShapeType(context),
                m => BuildViewModel(m, part)
            );
        }
        public override async Task<IDisplayResult> UpdateAsync(MapPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(
                model,
                Prefix,
                t => t.Latitude,
                t => t.Longitude
            );
            return Edit(model);
        }
        private void BuildViewModel(MapPartViewModel model, MapPart part)
        {
            model.Latitude = part.Latitude;
            model.Longitude = part.Longitude;
            model.MapPart = part;
            model.ContentItem = part.ContentItem;
        }
    }
    
    // public class MapPartDisplayDriver : ContentPartDisplayDriver<MapPart>
    // {
    //     private readonly IContentDefinitionManager _contentDefinitionManager;
    //
    //     public MapPartDisplayDriver(IContentDefinitionManager contentDefinitionManager)
    //     {
    //         _contentDefinitionManager = contentDefinitionManager;
    //     }
    //
    //     public override IDisplayResult Display(MapPart part, BuildPartDisplayContext context)
    //     {
    //         return Initialize<MapPartViewModel>(
    //                 GetDisplayShapeType(context),
    //                 m => BuildViewModel(m, part)
    //             )
    //             .Location("Detail", "Content:10")
    //             .Location("Summary", "Content:10");
    //     }
    //
    //     public override IDisplayResult Edit(MapPart part, BuildPartEditorContext context)
    //     {
    //         return Initialize<MapPartViewModel>(
    //             GetEditorShapeType(context),
    //             m => BuildViewModel(m, part)
    //         );
    //     }
    //
    //     // public override IDisplayResult Edit(MapPart part, BuildPartEditorContext context)
    //     // {
    //     //     return Initialize<MapPartViewModel>(GetEditorShapeType(context), model =>
    //     //     {
    //     //         model.Show = part.Show;
    //     //         model.ContentItem = part.ContentItem;
    //     //         model.MapPart = part;
    //     //     });
    //     // }
    //
    //
    //     public override async Task<IDisplayResult> UpdateAsync(MapPart model, IUpdateModel updater)
    //     {
    //         await updater.TryUpdateModelAsync(
    //             model,
    //             Prefix,
    //             t => t.Latitude,
    //             t => t.Longitude
    //         );
    //         return Edit(model);
    //     }
    //
    //     // public override async Task<IDisplayResult> UpdateAsync(MapPart model, IUpdateModel updater)
    //     // {
    //     //     await updater.TryUpdateModelAsync(model, Prefix, t => t.Show);
    //     //
    //     //     return Edit(model);
    //     // }
    //
    //     private void BuildViewModel(MapPartViewModel model, MapPart part)
    //     {
    //         model.Latitude = part.Latitude;
    //         model.Longitude = part.Longitude;
    //         model.MapPart = part;
    //         model.ContentItem = part.ContentItem;
    //         model.Show =
    //             part.Show; // does this need to be here?  Example doesn't say: https://www.dotnetthailand.com/web-frameworks/orchard-core-cms/create-a-custom-orchard-core-cms-module
    //     }
    // }
}