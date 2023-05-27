using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snowflake.Data;
using Snowflake.Models.ViewModels;

namespace Snowflake.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Product.Include(u => u.Category).Include(u => u.ProductType),
                Categories = _db.Category
            };
            return View(homeVM);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        public IActionResult Error()
        {
            return View();
        }
    }
}
