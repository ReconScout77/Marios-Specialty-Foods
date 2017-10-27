using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarioSpecialtyFoods.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarioSpecialtyFoods.Controllers
{
    public class ReviewsController : Controller
    {
        private IReviewRepository reviewRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewsController(UserManager<ApplicationUser> userManager,IReviewRepository thisRepo = null)
        {
            _userManager = userManager;

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
            if (review.CheckValid())
            {
                reviewRepo.Save(review);
                return RedirectToAction("Details", "Products", new { id = review.ProductId });
            }
            else
            {
                return RedirectToAction("Error");
            }
		}

		public IActionResult Edit(int id)
		{
			Review thisReview = reviewRepo.Reviews.FirstOrDefault(x => x.ReviewId == id);
			return View(thisReview);
		}

		[HttpPost]
        public IActionResult Edit(Review review)	
        {
            if (review.CheckValid())
            {
                reviewRepo.Edit(review);
                return RedirectToAction("Details", "Products", new { id = review.ProductId });
            }
			else
			{
				return RedirectToAction("Error");
			}
		}

        [Authorize]
		public IActionResult Delete(int id)
		{
			Review thisReview = reviewRepo.Reviews.FirstOrDefault(x => x.ReviewId == id);
			return View(thisReview);
		}

        [Authorize]
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			Review thisReview = reviewRepo.Reviews.FirstOrDefault(x => x.ReviewId == id);
            int thisProductId = thisReview.ProductId;
			reviewRepo.Remove(thisReview);
            return RedirectToAction("Details", "Products", new{ id = thisProductId });
		}

        public ViewResult Error(Review review)
        {
            return View(review);
        }

		//[HttpPost]
		//public IActionResult CreateReview(string newAuthor, string newContent, int newRating)
		//{
		//	Review newReview = new Review(newAuthor, newContent, newRating, 1);
		//	reviewRepo.Save(newReview);
		//	return Json(newReview);
		//}

	}
}
