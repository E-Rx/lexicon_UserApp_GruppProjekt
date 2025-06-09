using UsersApp.Domain.Entities;
using UsersApp.Application.Loans.Interfaces;


namespace UsersApp.Infrastructure.Services
{
    public class LoanService(ILoanRepository loanRepository) : ILoanService
    {
        public async Task AddAsync(Loan loan) => await loanRepository.AddAsync(loan);
        public Loan[] GetAll() => loanRepository.GetAll();
        public Loan? GetById(Guid id) => loanRepository.GetById(id);
        public async Task RemoveAsync(Loan loan) => await loanRepository.RemoveAsync(loan);
    }

   
}
