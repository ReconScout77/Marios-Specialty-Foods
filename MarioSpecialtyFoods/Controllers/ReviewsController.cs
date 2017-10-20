using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarioSpecialtyFoods.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Contracts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarioSpecialtyFoods.Controllers
{
    public class ReviewsController : Controller
    {
        private MarioSpecialtyFoodsContext db = new MarioSpecialtyFoodsContext();

		public IActionResult Details(int id)
		{
			var thisReview = db.Reviews.Include(review => review.Product)
							   .FirstOrDefault(review => review.ReviewId == id);
			return View(thisReview);
		}

		public IActionResult Create()
		{
			ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
			return View();
		}

		[HttpPost]
        public IActionResult Create(Review review)
        {
			db.Reviews.Add(review);
			db.SaveChanges();
            return RedirectToAction("Details", "Products", new{ id = review.ProductId });
		}

		public IActionResult Edit(int id)
		{
			var thisReview = db.Reviews.FirstOrDefault(review => review.ReviewId == id);
			return View(thisReview);
		}

		[HttpPost]
        public IActionResult Edit(Review review)	{
			db.Entry(review).State = EntityState.Modified;
			db.SaveChanges();
            return RedirectToAction("Details", "Products", new{ id = review.ProductId });
		}

		public IActionResult Delete(int id)
		{
			var thisReview = db.Reviews.FirstOrDefault(review => review.ReviewId == id);
			return View(thisReview);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var thisReview = db.Reviews.FirstOrDefault(review => review.ReviewId == id);
            int thisProductId = thisReview.ProductId;
			db.Reviews.Remove(thisReview);
			db.SaveChanges();
            return RedirectToAction("Details", "Products", new{ id = thisProductId });
		}
    }
}
