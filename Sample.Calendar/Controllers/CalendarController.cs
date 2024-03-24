using Microsoft.AspNetCore.Mvc;

namespace Sample.Calendar.Controllers
{
    public class CalendarController : Controller
    {
        [Route("calendar")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OnGetLombax()
        {
            return Content("<b>Foo</b>");
        }
    }
}