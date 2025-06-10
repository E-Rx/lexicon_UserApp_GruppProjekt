using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Application.Dtos;

namespace UsersApp.Application.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<object> GetById(string id);
        Task<object[]> GetAll();
        Task EditAsync(string id, UserProfileDto userProfileDto);

        Task RemoveAsync(string id);
    }
}
