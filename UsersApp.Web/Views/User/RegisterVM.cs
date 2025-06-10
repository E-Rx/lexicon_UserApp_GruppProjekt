using System.ComponentModel.DataAnnotations;

namespace UsersApp.Web.Views.User
{
    public class RegisterVM
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }

    }
}
