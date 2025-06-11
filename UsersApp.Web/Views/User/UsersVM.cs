namespace UsersApp.Web.Views.User
{
    public class UsersVM
    {
        public required string UserName { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public BookVM[]? LoanedBooks { get; set; }
    }
}
