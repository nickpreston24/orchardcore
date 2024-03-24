using System;
using System.Linq;
using System.Threading.Tasks;
using CodeMechanic.Diagnostics;
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


    [HttpPost]
    public async Task<IActionResult> OnPostCreateEvent(
        CalendarEvent calendarEvent
    )
    {
        Console.WriteLine(nameof(OnPostCreateEvent));
        calendarEvent.Dump(nameof(calendarEvent));
        var count = await calendar_svc.Create("create_calendar_event.sql", calendarEvent);
        Console.WriteLine($"Created {count} calendar events");

        var all_events = await calendar_svc.GetAll();
        return PartialView("_OrchardCalendar", all_events.ToList());
    }

    public IActionResult OnGetCreateForm()
    {
        Console.WriteLine(nameof(OnGetCreateForm));
        return PartialView("_CreateEventForm", new CalendarEvent() { });
    }

    public async Task<IActionResult> OnGetDeleteAllEvents()
    {
        Console.WriteLine(nameof(OnGetDeleteAllEvents));
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
}