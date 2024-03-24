using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Models;
using OrchardCore.Services;

namespace Sample.Calendar.Controllers
{
    public class CalendarController : Controller
    {
        [Route("calendar")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OnGetUpdatedButton()
        {
            Console.WriteLine(nameof(OnGetUpdatedButton));
            return Content(@"<button class='btn btn-primary border-accent border-2'>Bar</b>");
        }

        public IActionResult OnGetAlpineCounter()
        {
            Console.WriteLine(nameof(OnGetAlpineCounter));
            return PartialView("_AlpineCounter", 10);
        }

        public IActionResult OnGetInitialCalendar()
        {
            Console.WriteLine(nameof(OnGetInitialCalendar));
            return PartialView("_OrchardCalendar",
                Enumerable.Repeat(new CalendarEvent() { event_name = "foo" }, 10).ToList());
        }

    }
}