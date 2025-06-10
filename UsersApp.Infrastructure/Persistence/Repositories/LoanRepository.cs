using Microsoft.EntityFrameworkCore;
using UsersApp.Application.Dtos;
using UsersApp.Application.Loans.Interfaces;
using UsersApp.Domain.Entities;

namespace UsersApp.Infrastructure.Persistence.Repositories;

public class LoanRepository(ApplicationContext context) : ILoanRepository
{
    public async Task<ResultDto> AddAsync(Loan loan)
    {
        await context.AddAsync(loan);
        await context.SaveChangesAsync();
    }

    public Loan[] GetAll() => [.. context.Loans!];

    public Loan? GetById(Guid id) 
        => context.Loans!      
        .SingleOrDefault(l => l.Id == id);
        

    public async Task<ResultDto> RemoveAsync(Loan loan)
    {
        context.Remove(loan);
        await context.SaveChangesAsync();
    }
}
