using System.ComponentModel.DataAnnotations;
namespace PROG7311_ST10263164.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "*Email Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "*Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
