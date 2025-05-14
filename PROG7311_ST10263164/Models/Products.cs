using System.ComponentModel.DataAnnotations;
namespace PROG7311_ST10263164.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public DateOnly ProductDate { get; set; }
        public string? UserEmail { get; set; }
        public string? Category { get; set; }
    }
}
