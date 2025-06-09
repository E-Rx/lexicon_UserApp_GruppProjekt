using UsersApp.Application.Books.Interfaces;
using UsersApp.Domain.Entities;

namespace UsersApp.Infrastructure.Persistence.Repositories;

public class BookRepository(ApplicationContext context) : IBookRepository
{
    public async Task AddAsync(Book book)
    {
        await context.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public Book[] GetAll() => [.. context.Books!];

    public Book? GetById(string isbn) => context.Books!.SingleOrDefault(b => b.ISBN == isbn);

    public async Task RemoveAsync(Book book)
    {
        context.Remove(book);
        await context.SaveChangesAsync();
    }
}
