using System.Reflection;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;


namespace Sample.Calendar;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IPermissionProvider, Permissions>();
        services.AddScoped<INavigationProvider, AdminMenu>();


        var main_assembly = Assembly.GetExecutingAssembly();
        services.AddSingleton<IEmbeddedResourceQuery>(
            new EmbeddedResourceService(
                    new Assembly[]
                    {
                        main_assembly
                    },
                    debugMode: true
                )
                .CacheAllEmbeddedFileContents());
    }
}