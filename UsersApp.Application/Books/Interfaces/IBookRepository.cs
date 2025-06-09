using UsersApp.Domain.Entities;

namespace UsersApp.Application.Books.Interfaces;

public interface IBookRepository
{
    Task Add(Book book);
    Book[] GetAll();
    Book? GetById(string isbn);
    Task Remove(Book book); 
}
