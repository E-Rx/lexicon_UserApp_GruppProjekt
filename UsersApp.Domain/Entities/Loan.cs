using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UsersApp.Domain.Entities
{
    public class Loan
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey(nameof(Book))]
        public string BookId { get; set; } = null!;
        [ForeignKey(nameof(LibraryUser))]
        public Guid UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } = null;
    }
}
