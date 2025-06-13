using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UsersApp.Infrastructure.Persistence;
using UsersApp.Domain.Entities;

namespace UsersApp.Infrastructure.Persistence
{
    public static class IdentitySeeder
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            var adminEmail = "admin@users.se";

            LibraryUser libraryUser = new LibraryUser()
            {
                DisplayName = "Admin"
            };

            var admin = await userManager.FindByEmailAsync(adminEmail)
                ?? new ApplicationUser()
                {
                    UserName = "Admin",
                    LibraryUserId = libraryUser.Id,
                    LibraryUser = libraryUser
                };

            if (admin.Id == null)
                await userManager.CreateAsync(admin, "admin123!");

            if (!await userManager.IsInRoleAsync(admin, "Admin"))
                await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
