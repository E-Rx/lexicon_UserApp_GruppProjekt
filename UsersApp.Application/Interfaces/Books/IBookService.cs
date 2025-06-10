using UsersApp.Application.Dtos;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Interfaces.Books;

public interface IBookService
{
    Task AddAsync(Book book);
    Book[] GetAll();
    Book? GetById(string isbn);
    Task RemoveAsync(Book book);
}
