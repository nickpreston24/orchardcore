using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Models;
using OrchardCore.Services;

namespace Sample.Calendar.Controllers
{
    public class CalendarController : Controller
    {
        private ICalendarEventService calendar_svc;

        public CalendarController(
            ICalendarEventService calendar)
        {
            calendar_svc = calendar;
        }

        [Route("calendar")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OnGetAlpineCounter()
        {
            Console.WriteLine(nameof(OnGetAlpineCounter));
            return PartialView("_AlpineCounter", 10);
        }

        public async Task<IActionResult> OnGetInitialCalendar()
        {
            Console.WriteLine(nameof(OnGetInitialCalendar));
            var events = await calendar_svc.GetAll();
            return PartialView("_OrchardCalendar", events.ToList());
        }
    }
}