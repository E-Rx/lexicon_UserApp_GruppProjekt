using UsersApp.Application.Interfaces.Books;
using UsersApp.Application.Interfaces.Loans;
using UsersApp.Application.Interfaces.Users;

namespace UsersApp.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IBookRepository BookRepository { get; }
    ILoanRepository LoanRepository { get; }

    Task Save();
}
