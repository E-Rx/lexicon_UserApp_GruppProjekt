using UsersApp.Application.Dtos;

namespace UsersApp.Application.Users.Interfaces;

public interface IUserService
{
    Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password);
    Task<UserResultDto> SignInAsync(string email, string password);
    Task SignOutAsync();
}
