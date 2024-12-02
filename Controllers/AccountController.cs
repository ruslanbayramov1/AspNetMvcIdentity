using AspNetIdentity.Models;
using AspNetIdentity.ViewModels.Auths;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentity.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        User user = new User
        {
            Fullname = vm.Fullname,
            Email = vm.EmailAdress,
            UserName = vm.Username
        };

        var res = await _userManager.CreateAsync(user, vm.Password);
        if (!res.Succeeded)
        {
            foreach (var err in res.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }

        if (!ModelState.IsValid)
        {
            return View();
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        User? user = await _userManager.FindByNameAsync(vm.Username);
        if (user == null)
        {
            ModelState.AddModelError("", "Username not exists");
            return View();
        }

        var res = await _userManager.CheckPasswordAsync(user, vm.Password);

        if (!res)
        {
            ModelState.AddModelError("", "Password is wrong");
            return View();
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction("Index", "Home");
    }
}
