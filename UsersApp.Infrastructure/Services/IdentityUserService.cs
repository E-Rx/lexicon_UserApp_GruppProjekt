using Microsoft.AspNetCore.Identity;
using UsersApp.Application.Dtos;
using UsersApp.Application.Users;
using UsersApp.Infrastructure.Persistence;


namespace UsersApp.Infrastructure.Services;

public class IdentityUserService
(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> loginManager,
    RoleManager<ApplicationUser> roleManager
) : IIdentityUserService

{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password)
    {
        var result = await userManager.CreateAsync(new ApplicationUser
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            DateOfCreation = DateTime.Now,
            LastLogin = DateTime.Now,
            Profile = new()
        }, password);

        return new UserResultDto(result.Errors.FirstOrDefault()?.Description);
                             
            
    }

    public async Task<UserResultDto> SignInAsync(string email, string password)
    {
        var result = await loginManager.PasswordSignInAsync(email, password, false, false);
        return new UserResultDto(result.Succeeded ? null : "Invalid user Credentials");
    }

    public async Task SignOutAsync()
    {
        await loginManager.SignOutAsync();
    }
}
