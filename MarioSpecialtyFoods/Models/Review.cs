﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarioSpecialtyFoods.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Author { get; set; }
        public string ContentBody { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

		public bool CheckValid()
		{
            if((this.Rating > 5 || this.Rating < 1) || (this.ContentBody.Length > 250 || this.ContentBody.Length < 50))
            {
                return false;
            }
            else
            {
                return true;
            }
		}
    }
}
