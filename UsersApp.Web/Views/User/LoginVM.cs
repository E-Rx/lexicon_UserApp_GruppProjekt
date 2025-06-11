using System.ComponentModel.DataAnnotations;

namespace UsersApp.Web.Views.User
{
    public class LoginVM
    {
        [Required(ErrorMessage = "You must enter an UserName")]
        public required string UserName { get; set; }
        [Required(ErrorMessage = "You must enter a Password")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
