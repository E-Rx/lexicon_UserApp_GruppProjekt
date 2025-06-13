using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces;
using UsersApp.Application.Interfaces.Users;
using UsersApp.Domain.Entities;
using UsersApp.Infrastructure.Persistence;

namespace UsersApp.Infrastructure.Services;

public class IdentityUserService
(    
    UserManager<ApplicationUser> userManager,   
    SignInManager<ApplicationUser> loginManager, 
    RoleManager<IdentityRole> roleManager  
) : IIdentityUserService

{
   
    public async Task<ResultDto> CreateUserAsync(UserDto user, string password)
    {
        LibraryUser libraryUser = new() { DisplayName = user.DisplayName };
        var newUser = new ApplicationUser
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            DateOfCreation = DateTime.Now,
            LastLogin = DateTime.Now,
            LibraryUserId = libraryUser.Id,
            LibraryUser = libraryUser
        };

        var result = await userManager.CreateAsync(newUser, password);

        await userManager.AddToRoleAsync(newUser, "User");

        await userManager.AddClaimsAsync
            (
                newUser,
                [
                    new Claim("UserId", newUser.Id),
                    new Claim("DisplayName", user.DisplayName)
                ]
            );

        return new ResultDto(result.Errors.FirstOrDefault()?.Description);                                       
    }

    public async Task<ResultDto> SignInAsync(string userName, string password)
    {
        var result = await loginManager.PasswordSignInAsync(userName, password, false, false);
        return new ResultDto(result.Succeeded ? null : "Invalid user Credentials");
    }

    public async Task SignOutAsync()
    {
        await loginManager.SignOutAsync();
        
    }
    
}
