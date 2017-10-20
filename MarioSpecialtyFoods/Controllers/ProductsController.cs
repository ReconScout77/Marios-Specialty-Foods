using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarioSpecialtyFoods.Models;
using Microsoft.EntityFrameworkCore;

namespace MarioSpecialtyFoods.Controllers
{
    public class ProductsController : Controller
    {
        private MarioSpecialtyFoodsContext db = new MarioSpecialtyFoodsContext();
        // GET: /<controller>/
        public IActionResult Index()
        {
            var productList = db.Products.ToList();
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            var thisProduct = db.Products.Include(products => products.Reviews)
                                .FirstOrDefault(product => product.ProductId == id);
            return View(thisProduct);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisProduct = db.Products.FirstOrDefault(product => product.ProductId == id);
            return View(thisProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		public IActionResult Delete(int id)
		{
			var thisProduct = db.Products.FirstOrDefault(product => product.ProductId == id);
            return View(thisProduct);
		}

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisProduct = db.Products.FirstOrDefault(product => product.ProductId == id);
            db.Products.Remove(thisProduct);
            db.SaveChanges();
            return RedirectToAction("Index")
        }
    }
}
