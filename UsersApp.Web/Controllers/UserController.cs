using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces;
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
        UsersVM usersVM;
        try
        {
            string? user = User.FindFirstValue("UserId");
            if (user != null)
            {
                UserDto model = await userService.GetUserDtoById(user);
                usersVM.UserName = model.UserName
            }

            else
            {
                throw new ArgumentException("", "");
            }
        }

        catch
        {


        }

        return View();
    }

    [HttpPost("/users")]
    public async Task<IActionResult> Users(UsersVM usersVM)
    {
        if (!ModelState.IsValid)
            return View();

        try
        {
            var model = userService.Get

        }

        catch
        {


        }
        return RedirectToAction(nameof(Users));
    }

    //////////////////////////////////////////////////////

    [HttpGet("/users/details")]
    public async Task<IActionResult> UserDetails()
    {
        return View();
    }

    [HttpPost("/users/details")]
    public async Task<IActionResult> UserDetails(UserDetailsVM userDetails)
    {
        if (!ModelState.IsValid)
            return View();

        try
        {


        }

        catch
        {


        }

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
        if (!ModelState.IsValid)
            return View();

        try
        {


        }

        catch
        {


        }

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
        if (!ModelState.IsValid)
            return View();
        try
        {


        }

        catch
        {


        }

        return RedirectToAction(nameof(Users));
    }

    //////////////////////////////////////////////////////

}
