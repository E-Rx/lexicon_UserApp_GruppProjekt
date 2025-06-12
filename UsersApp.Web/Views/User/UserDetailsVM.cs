using System.ComponentModel.DataAnnotations;

namespace UsersApp.Web.Views.User
{
    public class UserDetailsVM
    {
        [Required(ErrorMessage = "Var vänlig och ange ett användarnamn")]
        [Display(Name = "Användarnamn")]
        public required string UserName { get; set; }

        [Display(Name = "Visningsnamn")]
        public string? DisplayName { get; set; }

        [Required(ErrorMessage = "Var vänlig och ange en e-mail address")]
        [EmailAddress(ErrorMessage = "Felaktig e-mailaddress")]
        [Display(Name = "E-mail")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Var vänlig och ange ett förnamn")]
        [Display(Name = "Förnamn")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Var vänlig och ange ett efternamn")]
        [Display(Name = "Efternamn")]
        public required string LastName { get; set; }      
    }
}
