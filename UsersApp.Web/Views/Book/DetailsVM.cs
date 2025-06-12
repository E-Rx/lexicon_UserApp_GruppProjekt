using UsersApp.Domain.Enums.Entities;

namespace UsersApp.Web.Views.Book;

public class DetailsVM
{
    public required string ISBN { get; set; } 
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required BookStatus Status { get; set; }
    public required BookCondition Condition { get; set; }
    public required BookGenre Genre { get; set; }

}
