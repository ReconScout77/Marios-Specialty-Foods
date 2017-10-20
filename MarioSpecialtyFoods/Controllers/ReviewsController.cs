using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarioSpecialtyFoods.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarioSpecialtyFoods.Controllers
{
    public class ReviewsController : Controller
    {
        private IReviewRepository reviewRepo;

        public ReviewsController(IReviewRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.reviewRepo = new EFReviewRepository();
            }
            else
            {
                this.reviewRepo = thisRepo;
            }
        }

        public ViewResult Index()
        {
            return View(reviewRepo.Reviews.ToList());
        }

		public IActionResult Details(int id)
		{
			Review thisReview = reviewRepo.Reviews.FirstOrDefault(x => x.ReviewId == id);
			return View(thisReview);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Create(Review review)
        {
            reviewRepo.Save(review);
            return RedirectToAction("Details", "Products", new{ id = review.ProductId });
		}

		public IActionResult Edit(int id)
		{
			Review thisReview = reviewRepo.Reviews.FirstOrDefault(x => x.ReviewId == id);
			return View(thisReview);
		}

		[HttpPost]
        public IActionResult Edit(Review review)	
        {
            reviewRepo.Edit(review);
            return RedirectToAction("Details", "Products", new{ id = review.ProductId });
		}

		public IActionResult Delete(int id)
		{
			Review thisReview = reviewRepo.Reviews.FirstOrDefault(x => x.ReviewId == id);
			return View(thisReview);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			Review thisReview = reviewRepo.Reviews.FirstOrDefault(x => x.ReviewId == id);
            int thisProductId = thisReview.ProductId;
			reviewRepo.Remove(thisReview);
            return RedirectToAction("Details", "Products", new{ id = thisProductId });
		}
    }
}
