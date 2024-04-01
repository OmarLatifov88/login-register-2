using Microsoft.AspNetCore.Mvc;
using UserLoginRegister.Contexts;

namespace UserLoginRegister.Controllers;

//private readonly ProniaDbContext _context;
public class HomeController : Controller
{
    private readonly ProniaDbContext _context;

    public HomeController(ProniaDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var appusers = _context.AppUsers.ToList();

        return View();
    }
}
