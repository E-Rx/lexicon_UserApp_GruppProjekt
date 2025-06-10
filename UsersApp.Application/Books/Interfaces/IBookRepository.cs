using UsersApp.Application.Dtos;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Books.Interfaces;

public interface IBookRepository
{
    Task<ResultDto> AddAsync(Book book);
    Book[] GetAll();
    Book? GetById(string isbn);
    Task<ResultDto> RemoveAsync(Book book); 
}
