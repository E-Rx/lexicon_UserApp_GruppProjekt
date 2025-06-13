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
            ISBN = bookDto.ISBN,
            Title = bookDto.Title,
            Author = bookDto.Author,
            Status = bookDto.Status,
            Condition = bookDto.Condition,
            Genre = bookDto.Genre
        };

        await context.Books!.AddAsync(book);
    }

    public BookDto[] GetAll() => [.. context.Books!
        .OrderBy(b => b.Title)
        .Select(b => new BookDto
        (
            b.ISBN,
            b.Title,
            b.Author,
            b.Status,
            b.Condition,
            b.Genre
        )) ];

    private async Task <Book> GetBookById(string isbn)
    {
        Book? book =  await context.Books!.SingleOrDefaultAsync(b => b.ISBN == isbn);

        return book ?? throw new ArgumentNullException
        (
            nameof(book) + "returnerade null",
            nameof(book)
        ); 
    }

    public async Task<BookDto?> GetById(string isbn)
    {
        Book? book = await GetBookById(isbn);
        
        return new BookDto
        (
            book.ISBN,
            book.Title,
            book.Author,
            book.Status,
            book.Condition,
            book.Genre
        ) 
        
        ?? 
        
        throw new ArgumentNullException
        (
            nameof(book) + "returnerade null",
            nameof(book)
        );

    }

    public async Task EditAsync(BookDto bookDto)
    {
        Book? book = await GetBookById(bookDto.ISBN) ?? throw new ArgumentNullException
        (
            nameof(book) + "returnerade null",
            nameof(book)
        ); 

        book!.ISBN = bookDto.ISBN;
        book.Title = bookDto.Title;
        book.Author = bookDto.Author;
        book.Status = bookDto.Status;
        book.Condition = bookDto.Condition;
        book.Genre = bookDto.Genre;
        
        var result = context.Books!.Update(book);

        if (result.State != EntityState.Modified)
            throw new ArgumentException
                (
                    nameof(result),
                    "Applikationen misslyckades med att ändra objektet " + nameof(book)
                );
    }
        
    

    public async Task RemoveAsync(string isbn)
    {
        Book? book = await GetBookById(isbn) ?? throw new ArgumentNullException
        (
            nameof(book) + "returnerade null",
            nameof(book)
        );

        var result = context.Books!.Remove(book);

        if (result.State != EntityState.Deleted)
            throw new ArgumentException
            (
                nameof(result),
                "Applikationen misslyckades med att ta bort objektet " + nameof(book)
            );
    }
}
