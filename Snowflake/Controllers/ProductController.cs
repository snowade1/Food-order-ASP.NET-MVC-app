using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Snowflake.Data;
using Snowflake.Models;
using Snowflake.Models.ViewModels;

namespace Snowflake.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
			_webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product;

			foreach (var obj in objList)
			{
				obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
			}

            return View(objList);
        }

        //GET - UPSERT
		public IActionResult Upsert(int? id)
		{
			//IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
			//{
			//	Text = i.Name,
			//	Value = i.Id.ToString()
			//});

			////ViewBag.CategoryDropDown = CategoryDropDown;
			//ViewData["CategoryDropDown"] = CategoryDropDown;

			//Product product = new Product();

			ProductVM productVM = new ProductVM()
			{
				Product = new Product(),
				CategorySelectList = _db.Category.Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				})
			};

			if (id == null)
			{
				return View(productVM);
			}
			else
			{
				productVM.Product = _db.Product.Find(id);
				if (productVM.Product == null)
				{
					return NotFound();
				}
				return View(productVM);
			}
		}

		//POST - UPSERT
		[HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Upsert(ProductVM productVM)
		{
			//if (ModelState.IsValid)
			//{
				var files = HttpContext.Request.Form.Files;
				string webRootPath = _webHostEnvironment.WebRootPath;

				if (productVM.Product.Id == 0)
				{
					//Creating
					string upload = webRootPath + WC.ImagePath;
					string fileName = Guid.NewGuid().ToString();
					string extension = Path.GetExtension(files[0].FileName);

					using (var fileStream = new FileStream(Path.Combine(upload, fileName+extension), FileMode.Create))
					{
						files[0].CopyTo(fileStream);
					}

					productVM.Product.Image =  fileName + extension;

					_db.Product.Add(productVM.Product);
				}
				else
				{
					//Updating

				}
				_db.SaveChanges();
				LogAction("Created/edited product: " + productVM.Product.Name);
				return RedirectToAction("Index");
			//}
			return View();
		}

		//GET - DELETE
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var obj = _db.Category.Find(id);
			if (obj == null)
			{
				return NotFound();
			}

			return View(obj);
		}

		//POST - DELETE
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePost(int? id)
		{
			var obj = _db.Category.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			_db.Category.Remove(obj);
			_db.SaveChanges();
			LogAction("Deleted product: " + obj.Name);
			return RedirectToAction("Index");
		}

		private void LogAction(string action)
		{
			string filePath = "Logs.txt";
			using (StreamWriter writer = new StreamWriter(filePath, true))
			{
				writer.WriteLine(action);
			}
		}
	}
}
