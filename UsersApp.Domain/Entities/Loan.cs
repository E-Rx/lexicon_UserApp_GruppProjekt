using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UsersApp.Domain.Entities
{
    public class Loan
    {
        [Key]
        public required Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey(nameof(Book))]
        public required string ISBN { get; set; } = null!;
        [ForeignKey(nameof(LibraryUser))]
        public required string UserId { get; set; }
        public required DateTime LoanDate { get; set; }
        public required DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } = null;
    }
}
