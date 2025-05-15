using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG7311_ST10263164.Models;
using PROG7311_ST10263164.ViewModels;
using System.Security.Claims;
using PROG7311_ST10263164.Data;

namespace PROG7311_ST10263164.Controllers
{
    public class MarketController : Controller
    {
        private readonly ILogger<MarketController> _logger;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly AppDbContext _dbContext;

        public MarketController(ILogger<MarketController> logger, UserManager<Users> model, AppDbContext dbContext, SignInManager<Users> signInManager)
        {
            _logger = logger;
            _userManager = model;
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> MarketPlace()
        {
            var products = await _dbContext.Products.ToListAsync(); // Gets all products from the database
            var viewModel = new ProductsViewModel
            {
                ProductList = products
            };
            return View("MarketPlace", viewModel);
        }
        public IActionResult AddItem()
        {
            return View();
        }
        public IActionResult DateRange()
        {
            var model = new DateViewModel();
            return View(model);
        }

        public IActionResult FilterCategory()
        {
            var model = new CategoryViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(); ;
            }
            var product = new Products
            {
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription,
                Category = model.Category,
                UserEmail = user.Email,
                ProductDate = model.ProductDate
            };

            _dbContext.Products.Add(product); // Add the product to the database
            await _dbContext.SaveChangesAsync(); // Save changes to the database

            return RedirectToAction("MarketPlace","Market");
        }

        [HttpPost]
        public async Task<IActionResult> FilterDateRange(DateViewModel model)
        {
            var products = await _dbContext.Products.ToListAsync(); // Get all products from the database

            if ((model.ProductDateFrom.HasValue) && (model.ProductDateTill.HasValue))
            {
                var fromDate = model.ProductDateFrom?.ToDateTime(new TimeOnly(0, 0)); // Converting DateOnly to DateTime otheriwse .Where doesnt work
                var tillDate = model.ProductDateTill?.ToDateTime(new TimeOnly(23, 59)); // Converting DateOnly to DateTime otheriwse .Where doesnt work

                var newProducts = products
                .Where(p => (p.ProductDate.ToDateTime(new TimeOnly(0, 0)) >= fromDate) && (p.ProductDate.ToDateTime(new TimeOnly(23, 59)) <= tillDate)) //also converting products in database to datetime so they can be compared to new datetime inputs
                .ToList();

                model.ProductList = newProducts; // products filtered by date polpulates the models product list
            }
            else 
            {
                var newProducts = products; // if there is not date the all products will populate the model

                model.ProductList = newProducts; // products filtered by date polpulates the models product list
            }

            return View("DateRange", model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCategory(CategoryViewModel model)
        {
            var products = await _dbContext.Products.ToListAsync(); // Get all products from the database

            if (model.CategoryFilter is not null)
            {
                var newProducts = products
                .Where(p => p.Category == model.CategoryFilter)
                .ToList();

                model.ProductList = newProducts; // products filtered by date polpulates the models product list
            }
            else
            {
                var newProducts = products; // if there is not category then all products will populate the model, also helps to see what
                                            // categories there are due to there being no set categories. The background of the poe made it seems like
                                            // green energy was the general idea however i assume farmers are selling their products from what part 2
                                            // says so thats why the database is populated with random foods

                model.ProductList = newProducts; // products filtered by date polpulates the models product list
            }

            return View("FilterCategory", model);
        }
    }
}
