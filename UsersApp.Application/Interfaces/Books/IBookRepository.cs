using UsersApp.Application.Dtos;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Interfaces.Books;

public interface IBookRepository
{
    Task AddAsync(BookDto book);
    BookDto[] GetAll();
    BookDto? GetById(string isbn);
    void EditAsync(BookDto bookDto);
    Task RemoveAsync(BookDto book); 
}
