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
    public HxHandlerAction CurrentAction { get; set; }

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
            start_date = InitialDate,
            end_date = InitialDate
        };
        to_save.Dump(nameof(to_save), ignoreNulls: true);

        var row_count = await calendar_svc.Create("create_calendar_event.sql", to_save.AsArray());

        return Content(
            $"<alert>Saved {to_save.event_name} at {to_save.start_date}!</alert>"
            // + @"<script>$dispatch('notice', {type: 'success', text: '🔥 Success!'})</script>" // See: hydrotoast.cs
        );
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

/// <summary>
/// I'm using this special Enumeration to let us easily recycle our form(s),
/// rather than making a form for every little change.
///
/// Usage: Set the handler name to the function you wish to call.  Removes `On*` verbs for you.
/// </summary>
public class HxHandlerAction : Enumeration
{
    // The name of the handler for an hx-page-handler attribute
    public string Handler { get; set; } = string.Empty;

    public static HxHandlerAction Add = new HxHandlerAction(1, nameof(Add));
    public static HxHandlerAction Edit = new HxHandlerAction(2, nameof(Edit));
    public static HxHandlerAction Delete = new HxHandlerAction(3, nameof(Delete));
    public static HxHandlerAction Search = new HxHandlerAction(4, nameof(Search));

    public HxHandlerAction(int id, string name, string handler_name = "OnGet") : base(id, name)
    {
        // intentional, do not remove.  Let the dev set this name, and blow up if he does not.
        Handler = (handler_name.NotEmpty() ? handler_name : throw new ArgumentNullException(nameof(handler_name)))
            // TODO: modifiy the replaces to be regex replacing the START of the method name, just to be perfectly accurate
            .Replace("OnGet", "")
            .Replace("OnPatch", "")
            .Replace("OnDelete", "")
            .Replace("OnUpdate", "")
            .Replace("OnPost", "");
    }
}