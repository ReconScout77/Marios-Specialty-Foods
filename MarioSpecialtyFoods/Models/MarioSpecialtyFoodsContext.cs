using Microsoft.EntityFrameworkCore;

namespace MarioSpecialtyFoods.Models
{
	public class MarioSpecialtyFoodsContext : DbContext
	{
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Review> Reviews { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseMySql(@"Server=localhost;Port=8889;database=mariospecialtyfoods;uid=root;pwd=root;");
	
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}