using Microsoft.AspNetCore.Identity;
using UsersApp.Application.Dtos;
using UsersApp.Application.Users;
using UsersApp.Infrastructure.Persistence;


namespace UsersApp.Infrastructure.Services
{
    public class IdentityUserService
    (
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> loginManager,
        RoleManager<ApplicationUser> roleManager
    ) : IIdentityUserService

    {
        Task<UserResultDto> IIdentityUserService.CreateUserAsync(UserProfileDto user, string password)
        {
            ApplicationUser applicationUser = new()
            {             
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfCreation = DateTime.Now,
                LastLogin = DateTime.Now
            };


                                 
                
        }

        Task<UserResultDto> IIdentityUserService.SignInAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        Task<UserResultDto> IIdentityUserService.SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
