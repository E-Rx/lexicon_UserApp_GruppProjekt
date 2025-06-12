using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces.Books;
using UsersApp.Application.Services.Users;
using UsersApp.Domain.Enums.Entities;
using UsersApp.Web.Views.Book;
using UsersApp.Web.Views.User;


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
    public async Task<IActionResult> Details(string Isbn)
    {
        BookDto? book = await bookService.GetById(Isbn);
        if (book is null)
            return NotFound($"Boken med ISBN {Isbn} hittades inte.");

        DetailsVM model = new DetailsVM
        {
            ISBN = book?.isbn ?? string.Empty,
            Title = book?.title ?? string.Empty,
            Author = book?.author ?? string.Empty,
            Status = book?.status ?? BookStatus.Available,
            Condition = book?.condition ?? BookCondition.New,
            Genre = book?.genre ?? BookGenre.Fiction
        };
        return View(model);
    }

    [HttpGet("register")]
    public IActionResult RegisterBook() => View();

    [HttpPost("register")]
    public async Task<IActionResult> RegisterBook(RegisterBookVM registerBookVM)
    {
        if (!ModelState.IsValid)
            return View();

        try
        {
            if (registerBookVM == null)
                throw new ArgumentNullException
                    (
                        nameof(registerBookVM),
                        nameof(registerBookVM) + " är null"
                    );           

            BookDto bookDto = new BookDto
            (
                registerBookVM.ISBN,
                registerBookVM.Title,
                registerBookVM.Author,
                registerBookVM.Status,
                registerBookVM.Condition,
                registerBookVM.Genre
            );

            await bookService.AddAsync(bookDto);
        }

        catch (Exception err)
        {
            Console.WriteLine("Error: " + err);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("details/edit")]
    public async Task<IActionResult> EditBook(string isbn)
    {
        try
        {
            BookDto? bookDto = await bookService.GetById(isbn);
            if (bookDto != null)
            {               
                EditBookVM editBookVM = new ()
                {
                    ISBN = bookDto.isbn,
                    Title = bookDto.title,
                    Author = bookDto.author,
                    Status = bookDto.status,
                    Condition = bookDto.condition,
                    Genre = bookDto.genre
                };

                return View(editBookVM);

            }

            else
            {
                throw new ArgumentNullException
                (
                    nameof(bookDto),
                    nameof(bookDto) + " returnerade null"
                );
            }
        }

        catch (Exception err)
        {
            Console.WriteLine("Error: " + err);
        }


        return View();
    }

    [HttpPost("details/edit")]
    public async Task<IActionResult> EditBook(EditBookVM editBookVM)
    {
        if (!ModelState.IsValid)
            return View();

        try
        {
            if (editBookVM == null)
                throw new ArgumentNullException
                    (
                        nameof(editBookVM),
                        nameof(editBookVM) + " är null"
                    );

            BookDto bookDto = new
                (
                    editBookVM.ISBN,
                    editBookVM.Title,
                    editBookVM.Author,
                    editBookVM.Status,
                    editBookVM.Condition,
                    editBookVM.Genre
                );          

            await bookService.EditAsync(bookDto);
        }

        catch (Exception err)
        {
            Console.WriteLine("Error: " + err);
        }

        return RedirectToAction(nameof(Index));
    }

}
