using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Application.Dtos;

namespace UsersApp.Application.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserDtoById(string id);
        Task<UserDto[]> GetAll();
        Task<AdminUserDto[]> GetAllWithId();
        Task EditAsync(string id, UserDto userProfileDto);
        Task RemoveAsync(string id);
        Task UpdateLastLogin(string id);
    }
}
