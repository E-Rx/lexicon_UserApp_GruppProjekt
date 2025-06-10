using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersApp.Domain.Entities
{
    public class LibraryUser
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? DisplayName { get; set; }

    }
}
