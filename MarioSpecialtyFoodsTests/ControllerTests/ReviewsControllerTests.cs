using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using MarioSpecialtyFoods.Models;
using MarioSpecialtyFoods.Controllers;
using System.Collections.Generic;
using Moq;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace MarioSpecialtyFoodsTests
{
    [TestClass]
    public class ReviewsControllerTests : IDisposable
    {
		EFReviewRepository db = new EFReviewRepository(new TestDbContext());
		Mock<IReviewRepository> mock = new Mock<IReviewRepository>();

		private void DbSetup()
		{
			mock.Setup(m => m.Reviews).Returns(new Review[]
			{
				new Review {ReviewId = 1, Author = "Pahstah",  ContentBody = "This is my favorite product of all time ever. I mean it. I truly do.", Rating = 5, ProductId = 1 },
				new Review {ReviewId = 2, Author = "Piksa",  ContentBody = "Excellent, Magnificent, Truly Majestic. Absolutely can't be beat by anything.", Rating = 5, ProductId = 2 },
                new Review {ReviewId = 3, Author = "Pineapple",  ContentBody = "Shameful", Rating = 1, ProductId = 2 }
			}.AsQueryable());
		}

		private void DeleteAll()
		{
			TestDbContext db = new TestDbContext();
			db.Reviews.RemoveRange(db.Reviews.ToList());
            db.Products.RemoveRange(db.Products.ToList());
			db.SaveChanges();
		}

        [TestMethod]
        public void DB_CreateNewReview_Test()
        {
            ReviewsController controller = new ReviewsController(db);
            Review testReview = new Review();
            testReview.Author = "Rick";
            testReview.ContentBody = "I just wanna tell you how I'm feelin', gotta make you understand, never gonna give you up, never gonna let you down...";
            testReview.ProductId = 1;

            controller.Create(testReview);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Review>;

            CollectionAssert.Contains(collection, testReview);
        }

		public void Dispose()
		{
			this.DeleteAll();
		}
    }
}
