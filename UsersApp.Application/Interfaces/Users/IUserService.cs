using UsersApp.Application.Dtos;

namespace UsersApp.Application.Interfaces.Users;

public interface IUserService
{
    Task<ResultDto> CreateUserAsync(UserDto user, string password);
    Task<ResultDto> SignInAsync(string email, string password);
    Task SignOutAsync();
    Task<UserDto> GetUserDtoById(string id);
    Task<UserDto[]> GetAll();
    Task EditAsync(string id, UserDto userProfileDto);
    Task RemoveAsync(string id);
    Task UpdateLastLogin(string id);
}
