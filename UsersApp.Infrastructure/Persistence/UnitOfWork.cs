using UsersApp.Application.Interfaces;
using UsersApp.Application.Interfaces.Books;
using UsersApp.Application.Interfaces.Loans;
using UsersApp.Application.Interfaces.Users;

namespace UsersApp.Infrastructure.Persistence;

public class UnitOfWork
    (
        ApplicationContext context, 
        IUserRepository userRepository, 
        IBookRepository bookRepository, 
        ILoanRepository loanRepository
    ) : IUnitOfWork
{
    public IUserRepository UserRepository => userRepository;
    public IBookRepository BookRepository => bookRepository;
    public ILoanRepository LoanRepository => loanRepository;

    public async Task Save() => await context.SaveChangesAsync();

}
