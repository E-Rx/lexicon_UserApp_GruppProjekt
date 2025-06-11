using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsersApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace UsersApp.Infrastructure.Persistence;

public class ApplicationContext(DbContextOptions<ApplicationContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{   
    public DbSet<LibraryUser>? LibraryUsers { get; set; }
    public DbSet<Book>? Books { get; set; }
    public DbSet<Loan>? Loans { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<LibraryUser>().ToTable("Users");

        builder.Entity<ApplicationUser>()
            .HasKey(u => u.Id);
        //builder.Entity<ApplicationUser>()
        //    .HasOne(u => u.Profile)
        //    .WithOne();
//            .HasForeignKey<LibraryUser>(d => d.Id);

        builder.Entity<ApplicationUser>().Property(u => u.PasswordHash); // security
        builder.Entity<LibraryUser>().Property(d => d.DisplayName);             // business

        builder.Entity<Book>().ToTable("Books")
            .HasKey(b => b.ISBN);

        builder.Entity<Loan>().ToTable("Loans")
            .HasKey(l => l.Id);
        //builder.Entity<Loan>()
        //    .
        //    .HasFo
    }

}
