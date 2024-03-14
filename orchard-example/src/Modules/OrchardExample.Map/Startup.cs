using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardExample.Map.Drivers;
using OrchardExample.Map.Handlers;
using OrchardExample.Map.Models;
using OrchardExample.Map.Settings;
using OrchardExample.Map.ViewModels;
using OrchardCore.Modules;

namespace OrchardExample.Map
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TemplateOptions>(o =>
            {
                o.MemberAccessStrategy.Register<MapPartViewModel>();
            });

            services.AddContentPart<MapPart>()
                .UseDisplayDriver<MapPartDisplayDriver>()
                .AddHandler<MapPartHandler>();

            services.AddScoped<IContentTypePartDefinitionDisplayDriver, MapPartSettingsDisplayDriver>();
            services.AddDataMigration<Migrations>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Home",
                areaName: "OrchardExample.Map",
                pattern: "Home/Index",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
