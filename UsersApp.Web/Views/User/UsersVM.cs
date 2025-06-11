using UsersApp.Web.Views.Book;

namespace UsersApp.Web.Views.User
{
    public class UsersVM
    {
        public required string UserName { get; set; }
        public required string DisplayName { get; set; }       
        public BookVM[]? LoanedBooks { get; set; }
    }
}
