using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersApp.Domain.Entity;

internal class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string ISBN { get; set; } = null!;

    public bool IsAvailable { get; set; } = true;

}
