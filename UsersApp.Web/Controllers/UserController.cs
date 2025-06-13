using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces;
using UsersApp.Application.Interfaces.Books;
using UsersApp.Application.Interfaces.Loans;
using UsersApp.Application.Interfaces.Users;
using UsersApp.Application.Services.Users;
using UsersApp.Web.Views.User;

namespace UsersApp.Web.Controllers;

[Authorize]
public class UserController(IUserService userService, ILoanService loanService) : Controller
{
    [AllowAnonymous]
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    //////////////////////////////////////////////////////

    [HttpGet("users")]
    public async Task<IActionResult> Users()
    {
        try
        {
            string? user = User.FindFirstValue("UserId");
            if (user != null)
            {
                UserDto userDto = await userService.GetUserDtoById(user);
                LoanDto[] loanDtos = await loanService.GetAllByUserIdAsync(Guid.Parse(user));

                UsersVM usersVM = new()
                {
                    UserName = userDto.UserName,
                    DisplayName = userDto.DisplayName,
                    LoanedBooks = loanDtos
                    .Select(l => new LoanVM
                    {
                        id = l.id,
                        ISBN = l.ISBN,
                        Title = l.Title,
                        DueDate = l.dueDate
                    })
                    .ToArray()
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


    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public async Task<IActionResult> Admin()
    {
        try
        {
            string? user = User.FindFirstValue("UserId");
            if (user != null)
            {
                UserDto userDto = await userService.GetUserDtoById(user);
                if (userDto == null)
                    throw new ArgumentNullException
                    (
                        nameof(userDto),
                        nameof(userDto) + " returnerade null"
                    );

                AdminUserDto[] adminUserDtos = await userService.GetAllWithId();
  
                if (adminUserDtos != null)
                    return View(adminUserDtos);
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


    //[HttpPost("/users/{Id}")]
    //public async Task<IActionResult> Users(UsersVM usersVM)
    //{
    //    if (!ModelState.IsValid)
    //        return View();

    //    try
    //    {
    //        //var model = userService.Get

    //    }

    //    catch
    //    {


    //    }
    //    return RedirectToAction(nameof(Users));
    //}

    //////////////////////////////////////////////////////

    [HttpGet("users/details")]
    public async Task<IActionResult> UserDetails()
    {
        try
        {
            string? user = User.FindFirstValue("UserId");
            if (user != null)
            {
                UserDto userDto = await userService.GetUserDtoById(user);

                UserDetailsVM userDetailsVM = new()
                {
                    UserName = userDto.UserName,
                    DisplayName = userDto.DisplayName,
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName
                };

                return View(userDetailsVM);

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

    [HttpPost("users/details")]
    public async Task<IActionResult> UserDetails(UserDetailsVM userDetails)
    {
        if (!ModelState.IsValid)
            return View();

        try
        {
            string? user = User.FindFirstValue("UserId");

            if (user != null)
            {

                UserDto userDto = new UserDto
                    (
                        userDetails.UserName,
                        userDetails.DisplayName!,
                        userDetails.Email,
                        userDetails.FirstName,
                        userDetails.LastName
                    );

                await userService.EditAsync(user, userDto);
            }
        }

        catch (Exception err)
        {
            Console.WriteLine("Error: " + err);
        }

        return RedirectToAction(nameof(Users));
    }

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

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }

            await userService.UpdateLastLogin(loginVM.UserName);
        }

        catch (Exception err)
        {
            Console.WriteLine("Error: " + err);
        }

        string? userId = User.FindFirstValue("UserId");

        if (userId != null && await userService.IsAdmin(userId))
            return RedirectToAction(nameof(Admin));

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
                registerVM.DisplayName!,
                registerVM.Email,
                registerVM.FirstName,
                registerVM.LastName
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
