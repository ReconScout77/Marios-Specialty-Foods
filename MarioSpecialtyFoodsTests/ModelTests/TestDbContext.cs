using Microsoft.EntityFrameworkCore;

namespace MarioSpecialtyFoods.Models
{
    public class TestDbContext : MarioSpecialtyFoodsContext
    {
        public override DbSet<Product> Products { get; set; }
        public override DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(@"Server=localhost;Port=8889;database=mariospecialtyfoods_tests;uid=root;pwd=root;");
        }
    }
}
