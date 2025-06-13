using UsersApp.Infrastructure.Persistence;
using UsersApp.Application.Dtos;

namespace UsersApp.Web.Views.User
{
    public class AdminVM
    {
        public required string UserName { get; set; }
        public required AdminUserDto[]? Users { get; set; }
    }
}
