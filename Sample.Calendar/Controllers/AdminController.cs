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


    [HttpPost]
    public async Task<IActionResult> OnPostCreateEvent(CalendarEvent calendarEvent)
    {
        calendarEvent.Dump(nameof(calendarEvent));

        bool is_update = calendarEvent.EditMode.Equals("Edit", StringComparison.OrdinalIgnoreCase) ? true : false;

        if (is_update)
        {
            // update instead of creating:
            // int count = 
            await calendar_svc.Update(calendarEvent.id, calendarEvent);
            // Console.WriteLine($"Updated {count} calendar events");
        }

        else if (!is_update)
        {
            // Console.WriteLine(nameof(OnPostCreateEvent));
            var count = await calendar_svc.Create("create_calendar_event.sql", calendarEvent);
            Console.WriteLine($"Created {count} calendar events");
        }

        var all_events = await calendar_svc.GetAll();


        // TODO: make this change with the current tab:
        return PartialView("_OrchardEventList", all_events.ToList());
        
        // return
        //     is_update
        //         ? PartialView("_OrchardEventList", all_events.ToList())
        //         : PartialView("_OrchardCalendar", all_events.ToList());
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
        return PartialView("_CreateEventForm", new CalendarEvent() { });
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