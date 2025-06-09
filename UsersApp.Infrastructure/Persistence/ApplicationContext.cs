using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Domain.Entities;


namespace UsersApp.Infrastructure.Persistence;

public class ApplicationContext(DbContextOptions<ApplicationContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{
    public DbSet<Book>? Books { get; set; }
    public DbSet<Loan>? Loans { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<User>().ToTable("Users");

        builder.Entity<ApplicationUser>()
            .HasOne(u => u.Profile)
            .WithOne()
            .HasForeignKey<User>(d => d.Id);

        builder.Entity<ApplicationUser>().Property(u => u.PasswordHash); // security
        builder.Entity<User>().Property(d => d.DisplayName);             // business
    }

}
