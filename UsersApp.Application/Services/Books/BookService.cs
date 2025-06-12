using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces;
using UsersApp.Application.Interfaces.Books;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Services.Books;

public class BookService(IUnitOfWork unitOfWork) : IBookService
{
    public async Task AddAsync(BookDto book)
    {
        await unitOfWork.BookRepository.AddAsync(book);
        await unitOfWork.Save();
    }
    
    public BookDto[] GetAll() => unitOfWork.BookRepository.GetAll();
    public BookDto? GetById(string isbn) => unitOfWork.BookRepository.GetById(isbn);
    public async Task EditAsync(BookDto bookDto)
    {
        unitOfWork.BookRepository.EditAsync(bookDto);
        await unitOfWork.Save();
    }
    public async Task RemoveAsync(BookDto book)
    {
        await unitOfWork.BookRepository.RemoveAsync(book);
        await unitOfWork.Save();
    } 
        
        
}
