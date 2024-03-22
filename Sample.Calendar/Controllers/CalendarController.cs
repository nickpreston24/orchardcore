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
    }
}
