using System;
using System.Linq;
using System.Threading.Tasks;
using CodeMechanic.Diagnostics;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Admin;
using OrchardCore.Models;
using OrchardCore.Services;

namespace Sample.Calendar.Controllers;

[Admin]
public class AdminController : Controller
{
    private readonly ICalendarEventService calendar_svc;

    public AdminController(ICalendarEventService calendar)
    {
        this.calendar_svc = calendar;
    }

    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> OnPutUpdateEvent(CalendarEvent calendarEvent)
    {
        calendarEvent.Dump(nameof(calendarEvent));
        int count = await calendar_svc.Update(calendarEvent.id, calendarEvent);
        Console.WriteLine($"Updated {count} calendar events");

        bool is_list = calendarEvent.ViewName.Equals("Event List", StringComparison.OrdinalIgnoreCase);

        var all_events = await calendar_svc.GetAll();

        return
            is_list
                ? PartialView("_OrchardEventList", all_events.ToList())
                : PartialView("_OrchardCalendar", all_events.ToList());
    }

    [HttpPost]
    public async Task<IActionResult> OnPostCreateEvent(CalendarEvent calendarEvent)
    {
        var count = await calendar_svc.Create("create_calendar_event.sql", calendarEvent);
        Console.WriteLine($"Created {count} calendar events");

        var all_events = await calendar_svc.GetAll();


        // TODO: make this change with the current tab:
        // TODO: Make sure to return only events with 'published' status for non-admins
        // return PartialView("_OrchardEventList", all_events.ToList());

        bool is_list = calendarEvent.ViewName.Equals("Event List", StringComparison.OrdinalIgnoreCase);

        return
            is_list
                ? PartialView("_OrchardEventList", all_events.ToList())
                : PartialView("_OrchardCalendar", all_events.ToList());
    }

    public async Task<IActionResult> OnGetEditForm(int id)
    {
        Console.WriteLine(nameof(OnGetEditForm));
        Console.WriteLine(id);
        var record = await calendar_svc.GetById(id);
        return PartialView("_CreateEventForm", record.With(r => { r.EditMode = "Edit"; }));
    }

    public IActionResult OnGetCreateForm()
    {
        Console.WriteLine(nameof(OnGetCreateForm));
        return PartialView("_CreateEventForm", new CalendarEvent() { EditMode = "Add" });
    }

    public async Task<IActionResult> OnGetRemoveAllEvents()
    {
        Console.WriteLine(nameof(OnGetRemoveAllEvents));
        var count = await calendar_svc.DeleteAll();
        return Content(@$"<button class='btn btn-primary border-accent border-2'>Deleted all {count} events!</b>");
    }

    public async Task<IActionResult> OnDeleteRemoveEvent(int id)
    {
        Console.WriteLine(nameof(OnDeleteRemoveEvent));
        await calendar_svc.Delete(id);
        // return Content($@"<button class='btn btn-primary border-accent border-2'>Deleted event with id '{id}'!</b>");
        return PartialView("_OrchardEventList", default);
    }

    public async Task<IActionResult> OnGetEventList()
    {
        Console.WriteLine(nameof(OnGetEventList));
        var events = await calendar_svc.GetAll();
        return PartialView("_OrchardEventList", events.ToList());
    }
}