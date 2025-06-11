using System.ComponentModel.DataAnnotations;

namespace UsersApp.Web.Views.User
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "You must specify an UserName")]
        [Display(Name = "User Name")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "You must specify an e-mail address")]
        [EmailAddress(ErrorMessage = "Invalid e-mail address")]
        [Display(Name = "E-mail")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "You must specify a FirstName")]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "You must specify a LastName")]
        [Display(Name = "Last Name")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "You must specify a Password")]
        [DataType(DataType.Password)]      
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare(nameof(Password))]
        public required string PasswordRepeat { get; set; }

    }
}
