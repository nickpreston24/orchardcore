using Microsoft.AspNetCore.Mvc;
using OrchardCore.Admin;

namespace Sample.Calendar.Controllers;

[Admin]
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
