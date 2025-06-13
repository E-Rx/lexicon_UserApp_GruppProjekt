using UsersApp.Infrastructure.Persistence;

namespace UsersApp.Web.Views.User
{
    public class AdminVM
    {
        public required string UserName { get; set; }
        public ApplicationUser[] Users { get; set; }
    }
}
