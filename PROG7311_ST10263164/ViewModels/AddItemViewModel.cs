using System.ComponentModel.DataAnnotations;
using PROG7311_ST10263164.Models;

namespace PROG7311_ST10263164.ViewModels
{
    public class AddItemViewModel
    {
        //public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateOnly ProductDate { get; set; }
        public string UserEmail { get; set; }
        public string Category { get; set; }

        public Users User { get; set; }
        public Products Product { get; set; }

    }
}
