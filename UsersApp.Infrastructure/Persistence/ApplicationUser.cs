using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersApp.Infrastructure.Persistence;

public class ApplicationUser : IdentityUser
{       
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfCreation { get; set; } 
    public DateTime LastLogin { get; set; } 
}
