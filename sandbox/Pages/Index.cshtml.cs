using CodeMechanic.Diagnostics;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orchard.Sandbox.Services;

namespace Orchard.Sandbox.Pages;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICalendarEventService calendar_svc;

    public DateTime InitialDate { get; set; } = DateTime.UtcNow;
    public int Count { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string Description { get; set; } = string.Empty;

    public IndexModel(ILogger<IndexModel> logger
        , ICalendarEventService calendarsvc
    )
    {
        _logger = logger;
        calendar_svc = calendarsvc;
    }

    public async Task OnGet()
    {
        await calendar_svc.SeedCalendar();
        var events = (await calendar_svc.GetAll()) /*.Dump("all calendar events")*/;
    }


    public async Task<IActionResult> OnGetPublishedEvents()
    {
        Console.WriteLine(nameof(OnGetPublishedEvents));
        var results = await calendar_svc.Search(new CalendarEvent() { status = "published" });
        return Content("<alert>Saved!</alert>");
    }

    public async Task<IActionResult> OnPostSaveEvent()
    {
        Console.WriteLine(nameof(OnPostSaveEvent));
        var to_save = new CalendarEvent()
        {
            event_name = Name,
            description = Description,
            duration = TimeSpan.FromMinutes(Duration),
            start_date = InitialDate
        };
        to_save.Dump(nameof(to_save), ignoreNulls: true);

        var row_count = await calendar_svc.Create("create_calendar_event.sql", to_save.AsArray());

        return Content($"<alert>Saved {to_save.event_name} at {to_save.start_date}!</alert>");
    }

    public async Task<IActionResult> OnGetSave()
    {
        Console.WriteLine(nameof(OnGetSave));
        return Content("<alert>Saved!</alert>");
    }

    public async Task<IActionResult> OnGetRepaintCalendar()
    {
        Console.WriteLine(nameof(OnGetRepaintCalendar));
        // return Content("<hydro class='border-2 border-red' name=\"HydroCalendar\" />");
        // return Partial("~/Pages/Sandbox/_RenderCalendar", default);

        return Content("replaced!");
    }
}