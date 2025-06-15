using Microsoft.AspNetCore.Mvc;
using Moq;
using UsersApp.Application.Dtos;
using UsersApp.Application.Interfaces.Books;
using UsersApp.Domain.Enums.Entities;
using UsersApp.Web.Controllers;
using UsersApp.Web.Views.Book;


namespace UsersApp.Web.Tests;

    public class BookControllerTests
    {
        private readonly Mock<IBookService> mockBookService;
        private readonly BookController controller;

        public BookControllerTests()
        {
            mockBookService = new Mock<IBookService>();
            controller = new BookController(mockBookService.Object);
        }

        [Fact]
    public void Index_ReturnsViewWithOrderedBooks()
    {
        // Arrange
        var books = new List<BookDto>
        {
            new BookDto("111", "C Title", "Author1", BookStatus.Available, BookCondition.New, BookGenre.Fiction),
            new BookDto("222", "A Title", "Author2", BookStatus.Available, BookCondition.New, BookGenre.Fiction),
            new BookDto("333", "B Title", "Author3", BookStatus.Available, BookCondition.New, BookGenre.Fiction)
        };

        mockBookService.Setup(s => s.GetAll())
            .Returns(books.ToArray());

        // Act
        var result = controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<BookVM>>(viewResult.Model);

        var titles = model.Select(b => b.Title).ToList();
        Assert.Equal(new List<string> { "A Title", "B Title", "C Title" }, titles);
    }


        [Fact]
        public async Task Details_ReturnsViewWithBook()
        {
            // Arrange
            var isbn = "123456";
            var book = new BookDto(
                isbn,
                "Test Title",
                "Test Author",
                BookStatus.Available,
                BookCondition.New,
                BookGenre.Fiction);

            mockBookService.Setup(s => s.GetById(isbn))
                .ReturnsAsync(book);

            // Act
            var result = await controller.Details(isbn);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<DetailsVM>(viewResult.Model);

            Assert.Equal(isbn, model.ISBN);
            Assert.Equal("Test Title", model.Title);
            Assert.Equal("Test Author", model.Author);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var isbn = "notfound";
            mockBookService.Setup(s => s.GetById(isbn))
                .ReturnsAsync((BookDto?)null);

            // Act
            var result = await controller.Details(isbn);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Boken med ISBN {isbn} hittades inte.", notFoundResult.Value);
        }
    }
