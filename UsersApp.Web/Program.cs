using UsersApp.Application.Books.Interfaces;
using UsersApp.Application.Loans.Interfaces;
using UsersApp.Infrastructure.Persistence;
using UsersApp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UsersApp.Infrastructure.Persistence.Repositories;
using UsersApp.Application.Users.Interfaces;


namespace UsersApp.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        // Configure Entity Framework and Identity
        builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
        // Register the identity user service
        builder.Services.AddScoped<IIdentityUserService, IdentityUserService>();
        // Add controllers with views
        builder.Services.AddControllersWithViews();




        var app = builder.Build();
       
        app.UseAuthentication();

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}