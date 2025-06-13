using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UsersApp.Application.Interfaces.Loans;
using UsersApp.Domain.Entities;
using UsersApp.Web.Views.Loan;

namespace UsersApp.Web.Controllers;

[Authorize]
[Route("/loan")]
public class LoanController(ILoanService loanService) : Controller
{
    [HttpPost("Create")]
    public async Task<IActionResult> CreateAsync(CreateVM vm)
    {
        // Update loans
        await loanService.AddAsync(new Loan
        {
//            Id = Guid.NewGuid(),    
            BookId = vm.ISBN,
            UserId = Guid.Parse(User.FindFirstValue("UserId")!),
            DueDate = DateTime.Now.AddDays(30) // Example: 30 days loan period
        });

        return RedirectToAction("Index", "Book");
    }

    [HttpGet("Return")]
    public async Task<IActionResult> ReturnAsync(string ISBN)
    {
        var loan = (await loanService.GetAllByUserIdAsync(Guid.Parse(User.FindFirstValue("UserId")!)))
            .SingleOrDefault(l => l.ISBN == ISBN);
        if (loan != null)
        {
            Loan l = new()
            {
                Id = loan?.Id ?? Guid.Empty,
                BookId = loan?.ISBN ?? string.Empty,
                UserId = Guid.Parse(User.FindFirstValue("UserId") ?? string.Empty),
                DueDate = loan?.DueDate ?? DateTime.MinValue
            };
            await loanService.RemoveAsync(l);
        }
        return RedirectToAction("Users", "User");
    }
}
