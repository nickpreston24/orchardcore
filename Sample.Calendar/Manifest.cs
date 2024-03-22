using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Sample Calendar Module",
    Author = "GovBuilt team",
    Version = "0.1",
    Website = "https://github.com/GovBuilt/Sample-Calendar-Module"
)]

[assembly: Feature(
    Id = "Sample.Calendar",
    Name = "Sample Calendar Module",
    Description = "Sample Calendar which provides info for events, birthday, tasks and many things.",
    Category = "GovBuilt Sample"
)]
