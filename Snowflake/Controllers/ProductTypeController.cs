﻿using Microsoft.AspNetCore.Mvc;
using Snowflake.Data;
using Snowflake.Models;

namespace Snowflake.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductType> objList = _db.ProductType;
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
		public IActionResult Create(ProductType obj)
		{
            if (ModelState.IsValid)
            {
				_db.ProductType.Add(obj);
				_db.SaveChanges();
				LogAction("Created a new product type: " + obj.Name);
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
			var obj = _db.ProductType.Find(id);
			if (obj == null)
			{
				return NotFound();
			}

			return View(obj);
		}

		//POST - EDIT
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ProductType obj)
		{
			if (ModelState.IsValid)
			{
				_db.ProductType.Update(obj);
				_db.SaveChanges();
				LogAction("Edited product type: " + obj.Name);
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
			var obj = _db.ProductType.Find(id);
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
			var obj = _db.ProductType.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			_db.ProductType.Remove(obj);
			_db.SaveChanges();
			LogAction("Deleted product type: " + obj.Name);
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
