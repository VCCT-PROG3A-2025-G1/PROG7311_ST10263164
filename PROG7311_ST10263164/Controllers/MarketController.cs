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
            var products = await _dbContext.Products.ToListAsync();
            var viewModel = new ProductsViewModel
            {
                ProductList = products
            };
            return View(viewModel);
        }
        public IActionResult AddItem()
        {
            return View();
        }
        public IActionResult MyItems()
        {
            return View();
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

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("MarketPlace","Market");
        }
    }
}
