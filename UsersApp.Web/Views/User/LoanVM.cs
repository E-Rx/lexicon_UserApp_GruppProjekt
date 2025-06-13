namespace UsersApp.Web.Views.User;

public class LoanVM
{
    public required Guid id { get; set; }
    public required string ISBN { get; set; }
    public required string Title { get; set; }
    public required DateTime DueDate { get; set; }
}