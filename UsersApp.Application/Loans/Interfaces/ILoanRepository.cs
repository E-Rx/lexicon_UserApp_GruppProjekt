
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Loans.Interfaces;

public interface ILoanRepository
{
    Task AddAsync(Loan loan);
    Loan[] GetAll();
    Loan? GetById(Guid id);   
    Task RemoveAsync(Loan loan); 
}
