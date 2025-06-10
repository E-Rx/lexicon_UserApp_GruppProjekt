using Microsoft.EntityFrameworkCore;
using UsersApp.Application.Dtos;
using UsersApp.Application.Users.Interfaces;

namespace UsersApp.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ApplicationContext context) : IUserRepository
    {
        public async Task<LibraryUser> GetById(string id)
        {
            ApplicationUser? user = await context.Users
               .Include(c => c.Profile)
               .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentException
                    (
                        "Objektet " + nameof(user) + " kunde inte hittas.",
                        nameof(user)
                    );
            }

            return user;
        }

        public async Task<ApplicationUser[]> GetAll() => await context.Users.ToArrayAsync();
        public async Task EditAsync(string id, UserProfileDto userProfileDto)
        {
            ApplicationUser user = await GetById(id);

            user.Email = userProfileDto.Email;
            user.FirstName = userProfileDto.FirstName;
            user.LastName = userProfileDto.LastName;
            user.Profile.DisplayName = userProfileDto.DisplayName; 
            
            context.Users.Update(user);              
        }
        public async Task RemoveAsync(string id)
        {
            ApplicationUser user = await GetById(id);
            context.Users.Remove(user);
        }

        public async Task UpdateLastLogin(string id)
        {
            ApplicationUser user = await GetById(id);           
            user.LastLogin = DateTime.Now;

            context.Users.Update(user);      
        }
    }
}
