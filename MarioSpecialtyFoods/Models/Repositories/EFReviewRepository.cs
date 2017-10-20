using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarioSpecialtyFoods.Models
{
	public class EFReviewRepository : IReviewRepository
	{
		MarioSpecialtyFoodsContext db = new MarioSpecialtyFoodsContext();

		public EFReviewRepository(MarioSpecialtyFoodsContext connection = null)
		{
			if (connection == null)
			{
				this.db = new MarioSpecialtyFoodsContext();
			}
			else
			{
				this.db = connection;
			}
		}

		public IQueryable<Review> Reviews
		{ get { return db.Reviews; } }

		public Review Save(Review review)
		{
			db.Reviews.Add(review);
			db.SaveChanges();
			return review;
		}

		public Review Edit(Review review)
		{
			db.Entry(review).State = EntityState.Modified;
			db.SaveChanges();
			return review;
		}

		public void Remove(Review review)
		{
			db.Reviews.Remove(review);
			db.SaveChanges();
		}
	}
}
