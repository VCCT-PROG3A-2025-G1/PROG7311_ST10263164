using Microsoft.AspNetCore.Identity;
namespace PROG7311_ST10263164.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
