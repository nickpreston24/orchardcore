using CodeMechanic.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orchard.Sandbox.Pages.Components;
using Orchard.Sandbox.Services;

namespace Orchard.Sandbox.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICalendarEventService calendar_svc;

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