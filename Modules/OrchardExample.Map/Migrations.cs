using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardExample.Map.Models;

namespace OrchardExample.Map
{
    public class Migrations : DataMigration
    {
        
        IContentDefinitionManager _contentDefinitionManager;
        public Migrations(IContentDefinitionManager contentDefinitionManager) =>
            _contentDefinitionManager = contentDefinitionManager;
        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition(
                nameof(MapPart),
                part => part
                    .Attachable()
                    .WithDescription("Provide a map part for a content item")
            );
            return 1;
        }
        
        
        // IContentDefinitionManager _contentDefinitionManager;
        //
        // public Migrations(IContentDefinitionManager contentDefinitionManager)
        // {
        //     _contentDefinitionManager = contentDefinitionManager;
        // }
        //
        // public int Create()
        // {
        //     _contentDefinitionManager.AlterPartDefinition("MapPart", builder => builder
        //         .Attachable()
        //         .WithDescription("Provides a Map part for your content item."));
        //
        //     return 1;
        // }
    }
}
