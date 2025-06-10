using UsersApp.Application.Dtos;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Books.Interfaces;

public interface IBookRepository
{
    Task AddAsync(Book book);
    Book[] GetAll();
    Book? GetById(string isbn);
    Task RemoveAsync(Book book); 
}
