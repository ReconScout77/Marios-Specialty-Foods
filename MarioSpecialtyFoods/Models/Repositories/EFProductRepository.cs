using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarioSpecialtyFoods.Models
{
    public class EFProductRepository : IProductRepository
    {
        MarioSpecialtyFoodsContext db = new MarioSpecialtyFoodsContext();

        public EFProductRepository(MarioSpecialtyFoodsContext connection = null)
        {
            if(connection == null)
            {
                this.db = new MarioSpecialtyFoodsContext();
            }
            else
            {
                this.db = connection;
            }
        }

        public IQueryable<Product> Products
        { get { return db.Products; } }

        public Product Save(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        public Product Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return product;
        }

        public void Remove(Product product)
        {
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }
}
