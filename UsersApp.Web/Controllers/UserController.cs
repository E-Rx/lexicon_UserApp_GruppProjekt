using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersApp.Application.Interfaces.Users;
using UsersApp.Web.Views.User;

namespace UsersApp.Web.Controllers;

[Authorize]
public class UserController(IUserService userService) : Controller
{
    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {       
        return RedirectToAction(nameof(Login));
    }

    [HttpGet("/members")]   
    public async Task<IActionResult> Member()
    {
        return View();
    }

    [HttpPost("/members")]
    public async Task<IActionResult> Member(MembersVM memberVM)
    {
        return RedirectToAction(nameof(Member));
    }

    //////////////////////////////////////////////////////

    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {


        return RedirectToAction(nameof(Member));
    }

    //////////////////////////////////////////////////////

    [HttpGet("/register")]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost("/register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        return RedirectToAction(nameof(Member));
    }

    
}
