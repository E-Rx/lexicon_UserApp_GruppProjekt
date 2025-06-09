using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Domain.Enums.Entities;

namespace UsersApp.Domain.Entities
{
    public class Book
    {
        [Key]    
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public BookStatus Status { get; set; }
        public BookCondition Condition { get; set; }
        public BookGenre Genre { get; set; }

    }
}
