using UsersApp.Application.Dtos;

namespace UsersApp.Application.Interfaces.Users;

public interface IUserService
{
    Task<ResultDto> CreateUserAsync(UserDto user, string displayName, string password);
    Task<ResultDto> SignInAsync(string email, string password);
    Task SignOutAsync();
}
