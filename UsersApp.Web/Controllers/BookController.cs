using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces.Books;
using UsersApp.Web.Views.Book;
using UsersApp.Domain.Enums.Entities;

namespace UsersApp.Web.Controllers;

[Authorize]
[Route("/book")]
public class BookController(IBookService bookService) : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        BookVM[] bookVM = bookService
            .GetAll()
            .OrderBy(b => b.title)
            .Select(b => new BookVM
            {
                Isbn = b.isbn,
                Title = b.title,
                Author = b.author
            })
            .ToArray();
        return View(bookVM);
    }

    [HttpGet("details")]
    public IActionResult Details(string Isbn)
    {
        BookDto? book = bookService.GetById(Isbn);
        if (book is null)
            return NotFound($"Boken med ISBN {Isbn} hittades inte.");

        DetailsVM model = new DetailsVM
        {
            Isbn = book?.isbn ?? string.Empty,
            Title = book?.title ?? string.Empty,
            Author = book?.author ?? string.Empty,
            Status = BookStatusExtensions.GetSwedishName(book?.status ?? BookStatus.Available),
            Condition = BookConditionExtensions.GetSwedishName(book?.condition ?? BookCondition.New),
            Genre = BookGenreExtensions.GetSwedishName(book?.genre ?? BookGenre.Fiction)
        };
        return View(book);
    }
}
