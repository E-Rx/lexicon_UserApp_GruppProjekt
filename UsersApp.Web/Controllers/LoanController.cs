using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using UsersApp.Application.Interfaces.Loans;
using UsersApp.Domain.Entities;
using UsersApp.Web.Views.Loan;

namespace UsersApp.Web.Controllers;

[Authorize]
[Route("/loan")]
public class LoanController(ILoanService loanService) : Controller
{
    [Authorize]
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

        return RedirectToAction("Index", nameof(BookController));
    }
}
