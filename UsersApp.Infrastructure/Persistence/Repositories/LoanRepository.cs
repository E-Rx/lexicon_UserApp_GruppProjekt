using Microsoft.EntityFrameworkCore;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces.Loans;
using UsersApp.Domain.Entities;

namespace UsersApp.Infrastructure.Persistence.Repositories;

public class LoanRepository(ApplicationContext context) : ILoanRepository
{
    public async Task AddAsync(Loan loan)
    {
        await context.AddAsync(loan);
        await context.SaveChangesAsync(); // TODO - Add UnitofWork    
    }

    public Loan[] GetAll() => [.. context.Loans!];

    public Loan? GetById(Guid id)
        => context.Loans!
        .SingleOrDefault(l => l.Id == id);


    public async Task RemoveAsync(Loan loan)
    {
        context.Remove(loan);
        await context.SaveChangesAsync();
    }

    public async Task<LoanDto[]> GetAllByUserIdAsync(Guid userId)
    {
        return await context.Users!
            .Where(u => u.LibraryUserId == userId)
            .Join(
                context.Loans!,
                u => u.LibraryUserId,
                l => l.UserId,
                (user, loan) => new { user, loan }
            )
            .Join(
                context.Books!,
                lj => lj.loan.BookId,
                b => b.ISBN,
                (lj, b) => new 
                { 
                    Id = lj.loan.Id, 
                    ISBN = b.ISBN, 
                    Title = b.Title, 
                    DueDate = lj.loan.DueDate 
                }
            )
            .OrderBy(l => l.DueDate)
            .Select(l => new LoanDto
            (
                l.Id,
                l.ISBN,
                l.Title,
                l.DueDate
            ))
            .ToArrayAsync();
    }
}
