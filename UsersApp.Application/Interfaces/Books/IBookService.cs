using UsersApp.Application.Dtos;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Interfaces.Books;

public interface IBookService
{
    Task AddAsync(BookDto book);
    BookDto[] GetAll();
    BookDto? GetById(string isbn);
    Task RemoveAsync(BookDto book);
}
