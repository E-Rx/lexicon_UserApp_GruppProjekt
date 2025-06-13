using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UsersApp.Application.Interfaces;
using UsersApp.Application.Interfaces.Books;
using UsersApp.Application.Interfaces.Loans;
using UsersApp.Application.Interfaces.Users;
using UsersApp.Application.Services.Books;
using UsersApp.Application.Services.Loans;
using UsersApp.Application.Services.Users;
using UsersApp.Domain.Entities;
using UsersApp.Infrastructure.Persistence;
using UsersApp.Infrastructure.Persistence.Repositories;
using UsersApp.Infrastructure.Services;



namespace UsersApp.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        // Configure Entity Framework and Identity
        builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
        
        // Register the identity user service
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IIdentityUserService, IdentityUserService>();
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<ILoanService, LoanService>();
        // Add controllers with views
        builder.Services.AddControllersWithViews();

        builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/login");

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        await IdentitySeeder.SeedAsync(scope.ServiceProvider);


        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseStaticFiles();

        app.Run();
    }
}