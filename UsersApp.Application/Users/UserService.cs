using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Application.Dtos;
using UsersApp.Application.Users.Interfaces;

namespace UsersApp.Application.Users;

public class UserService(IIdentityUserService identityUserService) : IUserService
{
    public async Task<ResultDto> CreateUserAsync(UserProfileDto user, string displayName, string password) 
        => await identityUserService.CreateUserAsync(user, password);
    public async Task<ResultDto> SignInAsync(string email, string password) 
        => await identityUserService.SignInAsync(email, password);
    public async Task SignOutAsync()
        => await identityUserService.SignOutAsync();
}
