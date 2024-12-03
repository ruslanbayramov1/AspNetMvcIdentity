using AspNetIdentity.Enums;
using AspNetIdentity.Extensions;
using AspNetIdentity.Models;
using AspNetIdentity.ViewModels.Auths;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            return View();
        }

        var roleRes = await _userManager.AddToRoleAsync(user, Roles.User.GetRole());
        if (!roleRes.Succeeded)
        {
            foreach (var err in roleRes.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View();
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        User? user = await _userManager.FindByNameAsync(vm.Username);
        if (user == null)
        {
            ModelState.AddModelError("", "Username or password is wrong");
            return View();
        }

        var res = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);

        if (!res.Succeeded)
        {
            if (res.IsNotAllowed)
            {
                ModelState.AddModelError("", "Username or password is wrong");
            }
            else if (res.IsLockedOut)
            {
                ModelState.AddModelError("", $"Wait until {user.LockoutEnd!.Value}");
            }
            return View();
        }

        if (!string.IsNullOrWhiteSpace(returnUrl))
        { 
            return LocalRedirect(returnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
