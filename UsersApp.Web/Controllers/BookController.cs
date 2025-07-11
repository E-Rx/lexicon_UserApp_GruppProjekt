﻿using Microsoft.AspNetCore.Authorization;
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
        BookVM[] bookVM = [.. bookService
            .GetAll()
            .OrderBy(b => b.Title)
            .Select(b => new BookVM
            {
                Isbn = b.ISBN,
                Title = b.Title,
                Author = b.Author
            })];
        return View(bookVM);
    }

    [HttpGet("details")]
    public async Task<IActionResult> Details(string Isbn)
    {
        BookDto? book = await bookService.GetById(Isbn);
        if (book is null)
            return NotFound($"Boken med ISBN {Isbn} hittades inte.");

        DetailsVM model = new()
        {
            ISBN = book?.ISBN ?? string.Empty,
            Title = book?.Title ?? string.Empty,
            Author = book?.Author ?? string.Empty,
            Status = book?.Status ?? BookStatus.Available,
            Condition = book?.Condition ?? BookCondition.New,
            Genre = book?.Genre ?? BookGenre.Fiction
        };
        return View(model);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("register")]
    public IActionResult RegisterBook() => View();

    [Authorize(Roles = "Admin")]
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

            BookDto bookDto = new (
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

    [Authorize(Roles = "Admin")]
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
                    ISBN = bookDto.ISBN,
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    Status = bookDto.Status,
                    Condition = bookDto.Condition,
                    Genre = bookDto.Genre
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

    [Authorize(Roles = "Admin")]
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


    [Authorize(Roles = "Admin")]
    [HttpGet("details/delete")]
    public async Task<IActionResult> DeleteBook(string isbn)
    {
        if (string.IsNullOrEmpty(isbn))
            return BadRequest("ISBN är tomt eller null.");

        try
        {
            await bookService.RemoveAsync(isbn);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fel vid borttagning: " + ex);
            return StatusCode(500, "Ett fel uppstod vid borttagning av boken.");
        }

        return RedirectToAction(nameof(Index));
    }
}
