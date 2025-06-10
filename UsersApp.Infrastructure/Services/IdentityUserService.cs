using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UsersApp.Application.Dtos;
using UsersApp.Application.Users.Interfaces;
using UsersApp.Domain.Entities;
using UsersApp.Infrastructure.Persistence;

namespace UsersApp.Infrastructure.Services;

public class IdentityUserService
(
    UserManager<ApplicationUser> userManager,   
    SignInManager<ApplicationUser> loginManager,
    RoleManager<ApplicationUser> roleManager
) : IIdentityUserService

{
    public async Task<ResultDto> CreateUserAsync(UserProfileDto user, string password)
    {    
        var newUser = new ApplicationUser
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            DateOfCreation = DateTime.Now,
            LastLogin = DateTime.Now,
            Profile = new() { DisplayName = user.displayName }
        };

        var result = await userManager.CreateAsync(newUser, password);

        await userManager.AddClaimsAsync
            (
                newUser,
                [
                    new Claim("UserId", newUser.Id),
                    new Claim("DisplayName", user.displayName)
                ]
            );

        return new ResultDto(result.Errors.FirstOrDefault()?.Description);                                       
    }

    public async Task<ResultDto> SignInAsync(string email, string password)
    {
        var result = await loginManager.PasswordSignInAsync(email, password, false, false);
        return new ResultDto(result.Succeeded ? null : "Invalid user Credentials");
    }

    public async Task SignOutAsync()
    {
        await loginManager.SignOutAsync();
        
    }

    public async Task<ResultDto> EditData(UserProfileDto user)
    {
        userManager.
    }
}
