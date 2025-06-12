using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces;
using UsersApp.Application.Interfaces.Loans;
using UsersApp.Domain.Entities;

namespace UsersApp.Application.Services.Loans;

public class LoanService(IUnitOfWork unitOfWork) : ILoanService
{
    public async Task AddAsync(Loan loan)
    {
        await unitOfWork.LoanRepository.AddAsync(loan);
        await unitOfWork.Save();
    }
           
    public Loan[] GetAll() => unitOfWork.LoanRepository.GetAll();
    public Loan? GetById(Guid id) => unitOfWork.LoanRepository.GetById(id);
    public async Task RemoveAsync(Loan loan)
    {
        await unitOfWork.LoanRepository.RemoveAsync(loan);
        await unitOfWork.Save();
    }

    public async Task<LoanDto[]> GetAllByUserIdAsync(Guid userId)
    {
        return await unitOfWork.LoanRepository.GetAllByUserIdAsync(userId);
    }
}


