using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces;
using UsersApp.Application.Interfaces.Users;

namespace UsersApp.Application.Services.Users;

public class UserService(IIdentityUserService identityUserService, IUnitOfWork unitOfWork) : IUserService
{
    public async Task<ResultDto> CreateUserAsync(UserDto user, string password) 
        => await identityUserService.CreateUserAsync(user, password);
    public async Task<ResultDto> SignInAsync(string userName, string password) 
        => await identityUserService.SignInAsync(userName, password);
    public async Task SignOutAsync()
        => await identityUserService.SignOutAsync();

    public async Task<UserDto> GetUserDtoById(string id) => await unitOfWork.UserRepository.GetUserDtoById(id);
    public async Task<UserDto[]> GetAll() => await unitOfWork.UserRepository.GetAll();
    public async Task EditAsync(string id, UserDto userProfileDto)
    {
        await unitOfWork.UserRepository.EditAsync(id, userProfileDto);
        await unitOfWork.Save();
    }
    public async Task RemoveAsync(string id)
    {
        await unitOfWork.UserRepository.RemoveAsync(id);
        await unitOfWork.Save();
    }

    public async Task UpdateLastLogin(string id)
    {
        await unitOfWork.UserRepository.UpdateLastLogin(id);
        await unitOfWork.Save();
    }

    // Add role to user
    //public async Task AddToRoleAsync(string userName, string role)
    //{
    //    await identityUserService.AddToRoleAsync(userName, role);
    //}
}
