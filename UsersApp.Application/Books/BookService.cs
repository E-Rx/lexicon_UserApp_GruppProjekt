using UsersApp.Application.Books.Interfaces;
using UsersApp.Domain.Entities;

namespace UsersApp.Infrastructure.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task AddAsync(Book book) => await bookRepository.AddAsync(book);
    public Book[] GetAll() => bookRepository.GetAll();
    public Book? GetById(string isbn) => bookRepository.GetById(isbn);
    public async Task RemoveAsync(Book book) => await bookRepository.RemoveAsync(book);
}
