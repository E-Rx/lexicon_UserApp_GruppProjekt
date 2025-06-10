using UsersApp.Application.Dtos;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Interfaces.Loans;

public interface ILoanRepository
{
    Task AddAsync(Loan loan);
    Loan[] GetAll();
    Loan? GetById(Guid id);   
    Task RemoveAsync(Loan loan); 
}
