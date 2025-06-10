using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersApp.Application.Users;

namespace UsersApp.Web.Controllers;

[Authorize]
public class UserController(UserService userService) : Controller
{
    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        // var model = userService.
        return View();
    }


    [HttpGet("/register")]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost("/register")]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        return RedirectToAction(nameof(Index));
    }

    
}
