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

    //////////////////////////////////////////////////////

    [HttpGet("/users")]   
    public async Task<IActionResult> Users()
    {
        return View();
    }

    [HttpPost("/users")]
    public async Task<IActionResult> Users(UsersVM usersVM)
    {
        return RedirectToAction(nameof(Users));
    }

    //////////////////////////////////////////////////////

    [HttpGet("/login")]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost("/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {


        return RedirectToAction(nameof(Users));
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
        return RedirectToAction(nameof(Users));
    }

    //////////////////////////////////////////////////////

}
