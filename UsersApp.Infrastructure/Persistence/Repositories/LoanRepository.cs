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
        return await context.Loans!
            .Include(l => context.Books)
            .Where(l => l.UserId == userId)
            .OrderBy(l => l.DueDate)
            .Select(l => new LoanDto
            (
                l.Id,
                l.Book.ISBN,
                l.Book.Title,
                l.UserId,
                l.User.LibraryUser.DisplayName!,
                l.LoanDate,
                l.DueDate,
                l.ReturnedDate
            ))
            .ToArrayAsync();
    }
}
