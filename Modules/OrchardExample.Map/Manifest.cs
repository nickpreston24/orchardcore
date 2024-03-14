using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "OrchardExample.Map",
    Author = "OrchardExample Team",
    Website = "https://www.OrchardExample.com",
    Version = "0.0.1",
    Description = "OrchardExample.Map",
    Dependencies = new[] { "OrchardCore.Contents" },
    Category = "Content Management"
)]