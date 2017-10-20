using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarioSpecialtyFoods.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Cost { get; set; }
        public string CountryOfOrigin { get; set; }
        public bool Featured { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
