using System.ComponentModel.DataAnnotations;

namespace UsersApp.Web.Views.User
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Var vänlig och ange ett användarnamn")]
        [Display(Name = "Användarnamn")]
        public required string UserName { get; set; }
        [Required(ErrorMessage = "Var vänlig och ange ett lösenord")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
