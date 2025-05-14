using PROG7311_ST10263164.Models;
using System.ComponentModel.DataAnnotations;

namespace PROG7311_ST10263164.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Products> ProductList { get; set; }
    }
}
