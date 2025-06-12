using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces.Books;
using UsersApp.Domain.Entities;

namespace UsersApp.Infrastructure.Persistence.Repositories;

public class BookRepository(ApplicationContext context) : IBookRepository
{
    public async Task AddAsync(BookDto book)
    {
        await context.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public BookDto[] GetAll() => context.Books!
        .OrderBy(b => b.Title)
        .Select(b => new BookDto
        (
            b.ISBN,
            b.Title,
            b.Author,
            b.Status,
            b.Condition,
            b.Genre
        ))
        .ToArray();

    public BookDto? GetById(string isbn)
    {
        Book? book = context.Books!.SingleOrDefault(b => b.ISBN == isbn);
        if (book is null)
            return null;
        return new BookDto
        (
            book.ISBN,
            book.Title,
            book.Author,
            book.Status,
            book.Condition,
            book.Genre
        );
    }

    public async Task RemoveAsync(BookDto book)
    {
        context.Remove(book);
        await context.SaveChangesAsync();
    }
}
