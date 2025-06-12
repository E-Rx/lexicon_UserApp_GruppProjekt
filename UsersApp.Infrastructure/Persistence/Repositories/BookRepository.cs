using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces.Books;
using UsersApp.Domain.Entities;

namespace UsersApp.Infrastructure.Persistence.Repositories;

public class BookRepository(ApplicationContext context) : IBookRepository
{
    public async Task AddAsync(BookDto bookDto)
    {
        Book book = new()
        {
            ISBN = bookDto.isbn,
            Title = bookDto.title,
            Author = bookDto.author,
            Status = bookDto.status,
            Condition = bookDto.condition,
            Genre = bookDto.genre
        };

        await context.Books!.AddAsync(book);   
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

    private Book GetBookById(string isbn)
    {
        Book? book =  context.Books!.SingleOrDefault(b => b.ISBN == isbn);

        return book ?? throw new ArgumentNullException
            (
                nameof(book) + "returnerade null",
                nameof(book)
            ); ;
    }

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

    public void EditAsync(BookDto bookDto)
    {
        Book? book = GetBookById(bookDto.isbn);

        book!.ISBN = bookDto.isbn;
        book.Title = bookDto.title;
        book.Author = bookDto.author;
        book.Status = bookDto.status;
        book.Condition = bookDto.condition;
        book.Genre = bookDto.genre;
        
        var result = context.Books!.Update(book);

        if (result.State != EntityState.Modified)
            throw new ArgumentException
                (
                    nameof(result),
                    "Applikationen misslyckades med att lägga till objektet " + nameof(book)
                );
    }
        
    

    public async Task RemoveAsync(BookDto book)
    {
        context.Remove(book);
        await context.SaveChangesAsync();
    }
}
