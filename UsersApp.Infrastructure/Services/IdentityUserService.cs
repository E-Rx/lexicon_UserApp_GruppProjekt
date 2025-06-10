using Microsoft.AspNetCore.Identity;
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
    public async Task<LoanResultDto> CreateUserAsync(UserProfileDto user, string displayName, string password)
    {       
        var result = await userManager.CreateAsync(new ApplicationUser
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            DateOfCreation = DateTime.Now,
            LastLogin = DateTime.Now,
            Profile = new() { DisplayName = displayName }
        }, password);

        return new LoanResultDto(result.Errors.FirstOrDefault()?.Description);                                       
    }

    public async Task<LoanResultDto> SignInAsync(string email, string password)
    {
        var result = await loginManager.PasswordSignInAsync(email, password, false, false);
        return new LoanResultDto(result.Succeeded ? null : "Invalid user Credentials");
    }

    public async Task SignOutAsync()
    {
        await loginManager.SignOutAsync();
    }
}
