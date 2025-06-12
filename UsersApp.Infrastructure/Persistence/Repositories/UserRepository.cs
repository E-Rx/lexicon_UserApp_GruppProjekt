using Microsoft.EntityFrameworkCore;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces.Users;
using UsersApp.Domain.Entities;

namespace UsersApp.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ApplicationContext context) : IUserRepository
    {

        private async Task<ApplicationUser> GetUserById(string id)
        {
            ApplicationUser? user = await context.Users
                .Include(o => o.LibraryUser)
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
                    user.UserName!,
                    user.LibraryUser.DisplayName!,
                    user.Email!,
                    user.FirstName!,
                    user.LastName!                  
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
            UserDto[] userDtos = [.. users.Select(u => new UserDto
                (
                    u.UserName!,
                    u.LibraryUser.DisplayName!,
                    u.Email!, 
                    u.FirstName!, 
                    u.LastName!                  
                ))];

            return userDtos;
        }

        public async Task EditAsync(string id, UserDto userProfileDto)
        {           
            ApplicationUser user = await GetUserById(id);

            user.Email = userProfileDto.Email;
            user.FirstName = userProfileDto.FirstName;
            user.LastName = userProfileDto.LastName;
            user.LibraryUser.DisplayName = userProfileDto.DisplayName;

            var result = context.Users.Update(user);

            if (result.State != EntityState.Modified)
                throw new ArgumentException
                    (
                        nameof(result),
                        "Applikationen misslyckades med att lägga till objektet " + nameof(user)
                    );
        }
        public async Task RemoveAsync(string id)
        {
            ApplicationUser user = await GetUserById(id) ?? throw new ArgumentNullException
            (
                nameof(user) + "returnerade null",
                nameof(user)
            ); 

            var result = context.Users.Remove(user);

            if (result.State != EntityState.Deleted)
                throw new ArgumentException
                (
                    nameof(result),
                    "Applikationen misslyckades med att ta bort objektet " + nameof(user)
                );
        }

        public async Task UpdateLastLogin(string userName)
        {
            ApplicationUser user = await GetUserByUserName(userName);
            user.LastLogin = DateTime.Now;

            context.Users.Update(user);
        }

        private async Task<ApplicationUser> GetUserByUserName(string userName)
        {
            ApplicationUser? user = await context.Users
                .Include(o => o.LibraryUser)
                .SingleOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpperInvariant() );

            if (user == null)
                throw new ArgumentException
                    (
                        "Objektet " + nameof(user) + " kunde inte hittas.",
                        nameof(user)
                    );

            return user;

        }
    }
}
