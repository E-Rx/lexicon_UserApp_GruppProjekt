using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Domain.Entities;

namespace UsersApp.Infrastructure.Persistence;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfCreation { get; set; } 
    public DateTime LastLogin { get; set; }
    [Required]
    public Guid LibraryUserId { get; set; }
    [Required]
    [ForeignKey(nameof(LibraryUserId))] 
    public required virtual LibraryUser LibraryUser { get; set; }
}
