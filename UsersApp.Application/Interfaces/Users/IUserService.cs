using UsersApp.Application.Dtos;

namespace UsersApp.Application.Interfaces.Users;

public interface IUserService
{
    Task<ResultDto> CreateUserAsync(UserDto user, string password);
    Task<ResultDto> SignInAsync(string userName, string password);
    Task SignOutAsync();
    Task<bool> IsAdmin(string id);
    Task<UserDto> GetUserDtoById(string id);
    Task<UserDto[]> GetAll();
    Task EditAsync(string id, UserDto userProfileDto);
    Task RemoveAsync(string id);
    Task UpdateLastLogin(string id);


    // Add role to user 
    //Task AddRoleAsync(string userName, string roleName);
}
