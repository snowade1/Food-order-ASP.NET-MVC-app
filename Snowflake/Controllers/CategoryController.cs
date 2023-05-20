using Microsoft.AspNetCore.Mvc;
using Snowflake.Data;
using Snowflake.Models;
using System.IO;

namespace Snowflake.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Category;
            return View(objList);
        }

        //GET - CREATE
		public IActionResult Create()
		{
			
			return View();
		}

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Create(Category obj)
		{
            if (ModelState.IsValid)
            {
				_db.Category.Add(obj);
				_db.SaveChanges();
				LogAction("Created a new category: " + obj.Name);
				return RedirectToAction("Index");
			}
            return View(obj);
		}

		//GET - EDIT
		public IActionResult Edit(int? id)
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

		//POST - EDIT
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.Category.Update(obj);
				_db.SaveChanges();
				LogAction("Edited category: " + obj.Name);
				return RedirectToAction("Index");
			}
			return View(obj);
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
			LogAction("Deleted category: " + obj.Name);
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
