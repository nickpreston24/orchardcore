using System;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardExample.Map.Models;

namespace OrchardExample.Map.Settings
{
    public class MapPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!string.Equals(nameof(MapPart), contentTypePartDefinition.PartDefinition.Name))
            {
                return null;
            }

            return Initialize<MapPartSettingsViewModel>("MapPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.GetSettings<MapPartSettings>();

                model.MySetting = settings.MySetting;
                model.MapPartSettings = settings;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!string.Equals(nameof(MapPart), contentTypePartDefinition.PartDefinition.Name))
            {
                return null;
            }

            var model = new MapPartSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.MySetting))
            {
                context.Builder.WithSettings(new MapPartSettings { MySetting = model.MySetting });
            }

            return Edit(contentTypePartDefinition, context.Updater);
        }
    }
}
