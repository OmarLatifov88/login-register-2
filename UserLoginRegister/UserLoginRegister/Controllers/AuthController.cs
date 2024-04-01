using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserLoginRegister.Models;
using UserLoginRegister.Views.ViewModels;

namespace UserLoginRegister.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<appUser> _userManager;
    private readonly SignInManager<appUser> _signInManager;
    public AuthController(UserManager<appUser> userManager, SignInManager<appUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        

        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]

    public async Task<IActionResult> Login (LoginViewModel loginViewModel)
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        
        if (!ModelState.IsValid)
            return View();

        var user = await _userManager.FindByNameAsync(loginViewModel.UsernameOrEmail);
        if(user == null)
        {
            user = await _userManager.FindByEmailAsync(loginViewModel.UsernameOrEmail);
            if(user == null)
            {
                ModelState.AddModelError("", "Username or Email incorrect");
                return View();
            }
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName,
            loginViewModel.Password, loginViewModel.RememberMe, false);
        if(!signInResult.Succeeded)
        {
            ModelState.AddModelError("", "Username or Email incorrect");
            return View();
        }
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        if (!User.Identity.IsAuthenticated)
            return BadRequest();

        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    public IActionResult ForgotPassword()
    {
         return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
    {
        if(!ModelState.IsValid)
           return View();

        var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
        if(user == null)
        {
            ModelState.AddModelError("Email", "Email not found");
            return View();
        }

    https://localhost:7056/auth/ResetPassword?email=&token=
        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        string link = Url.Action("ResetPassword", "Auth", new { email = user.Email, token = token },
            HttpContext.Request.Scheme, HttpContext.Request.Host.Value);
        return Content(link);

        return RedirectToAction(nameof(Login));
    }

    public async Task<IActionResult> ResetPassword(SubmitPasswordViewModel resetPasswordViewModel)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
        if (user == null)
            return NotFound();  

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> ResetPassword(SubmitResetPasswordViewModel 
        submitResetPasswordViewModel, string email, string token)
    {
        if (!ModelState.IsValid)
            return View();

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return NotFound();

        IdentityResult identityresult = await _userManager.ResetPasswordAsync(user, token,
            submitResetPasswordViewModel.Password);
        if (!identityResult.Succeeded)
        {
            foreach(var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        return RedirectToAction(nameof(Login));
    }
}
