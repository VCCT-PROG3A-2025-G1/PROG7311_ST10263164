using PROG7311_ST10263164.Models;

namespace PROG7311_ST10263164.ViewModels
{
    public class MarketPlaceViewModel
    {
        public string ?CategoryFilter { get; set; }
        public IEnumerable<Products> ProductList { get; set; } // List of products
        public DateOnly? ProductDateFrom { get; set; }
        public DateOnly? ProductDateTill { get; set; }
    }
}
