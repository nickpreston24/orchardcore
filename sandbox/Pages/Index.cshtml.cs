using CodeMechanic.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public void OnGet()
    {
        calendar_svc.GetAll().Dump("all calendar events");
    }
}
