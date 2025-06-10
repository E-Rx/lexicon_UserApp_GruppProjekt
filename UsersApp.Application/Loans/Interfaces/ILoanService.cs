using UsersApp.Application.Dtos;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Loans.Interfaces
{
    public interface ILoanService
    {
        Task<ResultDto> AddAsync(Loan loan);
        Loan[] GetAll();
        Loan? GetById(Guid id);
        Task<ResultDto> RemoveAsync(Loan loan);
    }
}