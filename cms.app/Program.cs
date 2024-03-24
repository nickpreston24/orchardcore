using System.Reflection;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using OrchardCore.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSingleton(configuration).AddOrchardCms();



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


builder.Services.AddSingleton<ISampleMarkdownService, SampleMarkdownService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseOrchardCore();
app.Run();
