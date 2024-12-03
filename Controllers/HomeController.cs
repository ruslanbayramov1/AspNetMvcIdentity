using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentity.Controllers;

#region Notes
// [Authorize] - makes all actions unaccessable if not logged in
// [AllowAnonymous] - makes only the action accessible if not logged in
#endregion

[Authorize] 
public class HomeController : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Cart()
    {
        return View();
    }
}
