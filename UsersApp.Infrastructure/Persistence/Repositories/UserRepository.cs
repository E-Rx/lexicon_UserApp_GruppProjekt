using Microsoft.EntityFrameworkCore;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces.Users;

namespace UsersApp.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ApplicationContext context) : IUserRepository
    {
       
        private async Task<ApplicationUser> GetUserById(string id)
        {
            ApplicationUser? user = await context.Users
               .Include(c => c.Profile)
               .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)          
                throw new ArgumentException
                    (
                        "Objektet " + nameof(user) + " kunde inte hittas.",
                        nameof(user)
                    );
            
            return user;
        }

        public async Task<UserDto> GetUserDtoById(string id)
        {
            ApplicationUser user = await GetUserById(id);

            UserDto userDto = new
                (
                    user.Email!,
                    user.FirstName!,
                    user.LastName!,
                    user.Profile.DisplayName!
                );

            if (userDto.Email == null)
                throw new ArgumentException
                    (
                        "Objektet " + nameof(userDto) + " returnerade null.",
                        nameof(userDto)
                    );

            return userDto;
        }
        public async Task<UserDto[]> GetAll()
        {
            ApplicationUser[] users = await context.Users.ToArrayAsync();
            UserDto[] userDtos = [.. users.Select(u => new UserDto(u.Email!, u.FirstName!, u.LastName!, u.Profile.DisplayName!))];

            return userDtos;
        }
            
        public async Task EditAsync(string id, UserDto userProfileDto)
        {
            ApplicationUser user = await GetUserById(id);

            user.Email = userProfileDto.Email;
            user.FirstName = userProfileDto.FirstName;
            user.LastName = userProfileDto.LastName;
            user.Profile.DisplayName = userProfileDto.DisplayName; 
            
            context.Users.Update(user);              
        }
        public async Task RemoveAsync(string id)
        {
            ApplicationUser user = await GetUserById(id);
            context.Users.Remove(user);
        }

        public async Task UpdateLastLogin(string id)
        {
            ApplicationUser user = await GetUserById(id);           
            user.LastLogin = DateTime.Now;

            context.Users.Update(user);      
        }
    }
}
