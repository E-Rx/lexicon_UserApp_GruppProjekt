using UsersApp.Domain.Enums.Entities;

namespace UsersApp.Web.Views.Book
{
    public class RegisterBookVM
    {
        public record BookDto(string isbn, string title, string author, BookStatus status, BookCondition condition, BookGenre genre);

        public required string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public 


    }
}
