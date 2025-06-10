using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces;
using UsersApp.Application.Interfaces.Books;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Services.Books;

public class BookService(IUnitOfWork unitOfWork) : IBookService
{
    public async Task AddAsync(Book book)
    {
        await unitOfWork.BookRepository.AddAsync(book);
        await unitOfWork.Save();
    }
    
    public Book[] GetAll() => unitOfWork.BookRepository.GetAll();
    public Book? GetById(string isbn) => unitOfWork.BookRepository.GetById(isbn);
    public async Task RemoveAsync(Book book)
    {
        await unitOfWork.BookRepository.RemoveAsync(book);
        await unitOfWork.Save();
    } 
        
        
}
