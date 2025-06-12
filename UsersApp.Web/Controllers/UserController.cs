using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces;
using UsersApp.Application.Interfaces.Users;
using UsersApp.Application.Services.Users;
using UsersApp.Web.Views.User;

namespace UsersApp.Web.Controllers;

[Authorize]
public class UserController(IUserService userService) : Controller   
{
    [AllowAnonymous]
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {       
        return RedirectToAction(nameof(Login));
    }

    //////////////////////////////////////////////////////

    [HttpGet("/users")]   
    public async Task<IActionResult> Users()
    {
        
        try
        {
            string? user = User.FindFirstValue("UserId");
            if (user != null)
            {
                UserDto userDto = await userService.GetUserDtoById(user);

                UsersVM usersVM = new()
                {
                    UserName = userDto.UserName,
                    DisplayName = userDto.DisplayName,
                    LoanedBooks = []
                };

                return View(usersVM);
            }

            else
            {
                throw new ArgumentNullException
                    (
                        nameof(user),
                        nameof(user) + " returnerade null"
                    );
            }
        }

        catch (Exception err)
        {
            Console.WriteLine("Error: " + err);
        }

        return View();
    }

    /*
    [HttpPost("/users/{Id}")]
    public async Task<IActionResult> Users(UsersVM usersVM)
    {
        if (!ModelState.IsValid)
            return View();

        try
        {
            //var model = userService.Get

        }

        catch
        {


        }
        return RedirectToAction(nameof(Users));
    }
    */
    //////////////////////////////////////////////////////

    /*[HttpGet("users/{Id}/details")]
    public async Task<IActionResult> UserDetails()
    {
        return View();
    }

    [HttpPost("users/{Id}/details")]
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
    }*/

    //////////////////////////////////////////////////////


    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
            return View();

        try
        {
            var result = await userService.SignInAsync(loginVM.UserName, loginVM.Password);
            if (result.Succeeded)
            {
                await userService.UpdateLastLogin(loginVM.UserName);
                return RedirectToAction(nameof(Users));
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }

        }

        catch
        {
            throw;
        }

        return RedirectToAction(nameof(Users));
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await userService.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    //////////////////////////////////////////////////////
    ///
    [AllowAnonymous]
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }


    [AllowAnonymous]
    [HttpPost("register")] 
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (!ModelState.IsValid)
            return View();
        try
        {
            if (registerVM == null)
                throw new ArgumentNullException
                    (
                        nameof(registerVM),
                        nameof(registerVM) + " är null"                       
                    );

            UserDto userDto = new UserDto
            (
                registerVM.UserName,
                registerVM.Email,
                registerVM.FirstName,
                registerVM.LastName,
                registerVM.DisplayName!
            );

            var result = await userService.CreateUserAsync(userDto, registerVM.Password);

            if (result.Succeeded) 
                result = await userService.SignInAsync(userDto.UserName, registerVM.Password);

            else
            {
                return RedirectToAction(nameof(Login));
            }


        }

        catch (Exception err)
        {
            Console.WriteLine("Error: " + err);
        }

        return RedirectToAction(nameof(Users));
    }

    //////////////////////////////////////////////////////

}
