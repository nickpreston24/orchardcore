using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardExample.Map.Models;
using OrchardExample.Map.Settings;
using OrchardExample.Map.ViewModels;

namespace OrchardExample.Map.Drivers
{
    public class MapPartDisplayDriver : ContentPartDisplayDriver<MapPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public MapPartDisplayDriver(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public override IDisplayResult Display(MapPart part, BuildPartDisplayContext context)
        {
            return Initialize<MapPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content:10")
                .Location("Summary", "Content:10")
                ;
        }

        public override IDisplayResult Edit(MapPart part, BuildPartEditorContext context)
        {
            return Initialize<MapPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.Show = part.Show;
                model.ContentItem = part.ContentItem;
                model.MapPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(MapPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.Show);

            return Edit(model);
        }

        private static void BuildViewModel(MapPartViewModel model, MapPart part, BuildPartDisplayContext context)
        {
            var settings = context.TypePartDefinition.GetSettings<MapPartSettings>();

            model.ContentItem = part.ContentItem;
            model.MySetting = settings.MySetting;
            model.Show = part.Show;
            model.MapPart = part;
            model.Settings = settings;
        }
    }
}
