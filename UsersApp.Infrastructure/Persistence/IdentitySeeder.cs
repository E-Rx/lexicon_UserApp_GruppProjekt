using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using UsersApp.Domain.Entities;
using UsersApp.Infrastructure.Persistence;

namespace UsersApp.Infrastructure.Persistence
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            var adminEmail = "admin@users.se";

           LibraryUser libraryUser = new()
            {
                DisplayName = "Admin"
            };

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin =
                new ApplicationUser()
                {
                    UserName = "Admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Email = adminEmail,
                    DateOfCreation = DateTime.Now,
                    LastLogin = DateTime.Now,
                    LibraryUserId = libraryUser.Id,
                    LibraryUser = libraryUser
                };

                var result = await userManager.CreateAsync(admin, "Admin123!");

                await userManager.AddClaimsAsync
               (
                   admin,
                   [
                       new Claim("UserId", admin.Id),
                        new Claim("DisplayName", libraryUser.DisplayName)
                   ]
               );
            }

            if (!await userManager.IsInRoleAsync(admin, "Admin"))
                await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
