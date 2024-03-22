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
        await calendar_svc.SeedCalendar(30);
        var events = (await calendar_svc.GetAll()) /*.Dump("all calendar events")*/;
    }

    public async Task<IActionResult> OnGetDeleteAll()
    {
        var count = await calendar_svc.DeleteAll();
        return Content($"<alert>{count} rows were permanently deleted!</alert>");
    }

    public async Task<IActionResult> OnGetPublishedEvents()
    {
        Console.WriteLine(nameof(OnGetPublishedEvents));
        var results = await calendar_svc.Search(new CalendarEvent() { status = "published" });
        return Content("<alert>Saved!</alert>");
    }


    public async Task<IActionResult> OnGetCreateForm() => Partial("_CreateEventForm", this);

    public async Task<IActionResult> OnPostSaveEvent()
    {
        Console.WriteLine(nameof(OnPostSaveEvent));
        var to_save = new CalendarEvent()
        {
            event_name = Name,
            description = Description,
            duration = TimeSpan.FromMinutes(Duration),
            start_date = InitialDate,
            end_date = InitialDate
        };
        to_save.Dump(nameof(to_save), ignoreNulls: true);

        var row_count = await calendar_svc.Create("create_calendar_event.sql", to_save.AsArray());
        var all_events = await calendar_svc.GetAll();
        return Page();
        // return Partial("_HydroCalendar", all_events.ToArray());
        // return Content(
        //     $"<alert>Saved {to_save.event_name} at {to_save.start_date}!</alert>"
        //     // + @"<script>$dispatch('notice', {type: 'success', text: '🔥 Success!'})</script>" // See: hydrotoast.cs
        // );
    }

    public async Task<IActionResult> OnPostSave()
    {
        Console.WriteLine(nameof(OnPostSave));
        return Page();
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

    // public class CalendarHandler : HxHandler
    // {
    //     public static CalendarHandler OnGetSave = new CalendarHandler(1, nameof(OnGetSave), nameof(OnGetSave));
    //
    //     public static implicit operator CalendarHandler(string handler)
    //     {
    //         Console.WriteLine("searching for hanlder : " + handler);
    //         var found = GetAll<CalendarHandler>()
    //             .Dump("all handlers")
    //             .SingleOrDefault(x =>
    //                 x.Name
    //                     .Dump("hanldername").Equals(handler, StringComparison.InvariantCultureIgnoreCase)
    //             );
    //
    //         if (found == default)
    //         {
    //             throw new Exception($"No handler with name {handler} found!");
    //         }
    //
    //         return new CalendarHandler(found.Id, found.Name, found.Handler);
    //     }
    //
    //     public CalendarHandler(int id, string name, string handler_name = "OnGet") : base(id, name, handler_name)
    //     {
    //     }
    // }
}