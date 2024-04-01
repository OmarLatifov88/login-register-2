using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserLoginRegister.Models;
using UserLoginRegister.Views.ViewModels;

namespace UserLoginRegister.Controllers;

public class UserController : Controller
{
    private readonly UserManager<appUser> _userManager;

    public UserController(UserManager<appUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if(!ModelState.IsValid)
        {
            return View();
        }

        appUser appUser = new appUser()
        {
            FullName = registerViewModel.Fullname,
            UserName = registerViewModel.Username,
            Email = registerViewModel.Email,
            IsActive = true
        };

        IdentityResult identityResult = await _userManager.CreateAsync(appUser,
            registerViewModel.Password);
        if (!identityResult.Succeeded)
        {
            foreach ( var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        await _userManager.CreateAsync(appUser, registerViewModel.Password);

        return RedirectToAction("Index", "Home");
    }

   
}
