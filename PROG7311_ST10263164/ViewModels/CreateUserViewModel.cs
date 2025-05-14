using System.ComponentModel.DataAnnotations;
namespace PROG7311_ST10263164.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "*Email Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*Full Name Required")]
        public string FullName { get; set; }

        public string Role { get; set; } = "Employee";
    }
}
