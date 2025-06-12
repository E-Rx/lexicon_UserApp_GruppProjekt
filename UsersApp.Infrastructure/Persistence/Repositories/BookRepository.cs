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
        Book? book = await GetBookById(bookDto.isbn) ?? throw new ArgumentNullException
        (
            nameof(book) + "returnerade null",
            nameof(book)
        ); 

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
