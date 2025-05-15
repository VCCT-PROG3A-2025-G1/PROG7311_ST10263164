using PROG7311_ST10263164.Models;

namespace PROG7311_ST10263164.ViewModels
{
    public class DateViewModel
    {
        public DateOnly? ProductDateFrom { get; set; }
        public DateOnly? ProductDateTill { get; set; }

        public IEnumerable<Products> ProductList { get; set; } = new List<Products>(); // List of products
    }
}
