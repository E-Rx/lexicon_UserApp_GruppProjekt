using UsersApp.Application.Dtos;

namespace UsersApp.Application.Users.Interfaces;

public interface IIdentityUserService
{
    Task<ResultDto> CreateUserAsync(UserProfileDto user, string password);
    Task<ResultDto> SignInAsync(string email, string password);
    Task SignOutAsync();
}
