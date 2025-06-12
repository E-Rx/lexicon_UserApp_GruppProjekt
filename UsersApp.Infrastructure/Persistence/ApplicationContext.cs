using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UsersApp.Domain.Entities;
using UsersApp.Domain.Enums.Entities;


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


        builder.Entity<Book>().HasData(
            new Book
            {
                ISBN = "9780140449136",
                Title = "Brott och straff",
                Author = "Fjodor Dostojevskij",
                Status = BookStatus.Loaned,
                Condition = BookCondition.Good,
                Genre = BookGenre.Crime
            },
            new Book
            {
                ISBN = "9780143128540",
                Title = "1984",
                Author = "George Orwell",
                Status = BookStatus.Available,
                Condition = BookCondition.New,
                Genre = BookGenre.Science_Fiction
            },
            new Book
            {
                ISBN = "9780451524935",
                Title = "Stolthet och fördom",
                Author = "Jane Austen",
                Status = BookStatus.Loaned,
                Condition = BookCondition.Fair,
                Genre = BookGenre.Romance
            },
            new Book
            {
                ISBN = "9780307277671",
                Title = "Sapiens: En kort historik över mänskligheten",
                Author = "Yuval Noah Harari",
                Status = BookStatus.Available,
                Condition = BookCondition.New,
                Genre = BookGenre.Non_Fiction
            },
            new Book
            {
                ISBN = "9780062315007",
                Title = "Den gamle och havet",
                Author = "Ernest Hemingway",
                Status = BookStatus.Available,
                Condition = BookCondition.Good,
                Genre = BookGenre.Fiction
            }
        );
    }
}


    //        builder.Entity<ApplicationUser>().ToTable("Users");
    //        builder.Entity<LibraryUser>().ToTable("Users");

    //        builder.Entity<ApplicationUser>()
    //            .HasKey(u => u.Id);
    //        //builder.Entity<ApplicationUser>()
    //        //    .HasOne(u => u.Profile)
    //        //    .WithOne();
    ////            .HasForeignKey<LibraryUser>(d => d.Id);

    //        builder.Entity<ApplicationUser>().Property(u => u.PasswordHash); // security
    //        builder.Entity<LibraryUser>().Property(d => d.DisplayName);             // business

    //        builder.Entity<Book>().ToTable("Books")
    //            .HasKey(b => b.ISBN);

    //        builder.Entity<Loan>().ToTable("Loans")
    //            .HasKey(l => l.Id);
    //        //builder.Entity<Loan>()
    //        //    .
    //        //    .HasFo

