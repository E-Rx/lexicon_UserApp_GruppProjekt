using UsersApp.Application.Dtos;

namespace UsersApp.Application.Interfaces.Users;

public interface IIdentityUserService
{
    Task<ResultDto> CreateUserAsync(UserDto user, string password);
    Task<ResultDto> SignInAsync(string email, string password);
    Task SignOutAsync();
}
