using UsersApp.Application.Dtos;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Interfaces.Books;

public interface IBookService
{
    Task AddAsync(BookDto book);
    BookDto[] GetAll();
    Task<BookDto?> GetById(string isbn);
    Task EditAsync(BookDto bookDto);
    Task RemoveAsync(string isbn);
}
