using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentity.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string? username)
    {
        return View(username);
    }
}
