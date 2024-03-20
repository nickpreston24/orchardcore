using System.Reflection;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using Hydro.Configuration;
using Orchard.Sandbox.Pages.Shared;
using Orchard.Sandbox.Services;
using Rizzy;
using Rizzy.Antiforgery;
using Rizzy.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHydro();

builder.AddRizzy(config =>
    {
        config.RootComponent = typeof(HtmxApp<AppLayout>); //typeof(HtmxApp<AppLayout>);
        config.DefaultLayout = typeof(HtmxApp<MainLayout>);  // typeof(HtmxLayout<MainLayout>);

        // config.DefaultLayout = typeof(_Page_Pages_Shared__Layout_cshtml);
        config.AntiforgeryStrategy = AntiforgeryStrategy.GenerateTokensPerPage;
    })
    .WithHtmxConfiguration(config => { config.SelfRequestsOnly = true; })
    .WithHtmxConfiguration("articles", config =>
    {
        config.SelfRequestsOnly = true;
        config.GlobalViewTransitions = true;
    });


var main_assembly = Assembly.GetExecutingAssembly();
builder.Services.AddSingleton<IEmbeddedResourceQuery>(
    new EmbeddedResourceService(
            new Assembly[]
            {
                main_assembly
            },
            debugMode: true
        )
        .CacheAllEmbeddedFileContents());


builder.Services.AddSingleton<ICalendarEventService, CalendarEventService>();
builder.Services.AddSingleton<IGunService, GunService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseHydro(builder.Environment);
app.UseRizzy();

app.Run();