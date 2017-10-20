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
    public class ProductsControllerTests : IDisposable
    {
        EFProductRepository db = new EFProductRepository(new TestDbContext());
        Mock<IProductRepository> mock = new Mock<IProductRepository>();

        private void DbSetup()
        {
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, ProductName = "Pasta", Cost = 5, CountryOfOrigin = "Italy", Featured = false },
                new Product {ProductId = 2, ProductName = "Pizza", Cost = 4, CountryOfOrigin = "Italy", Featured = false }
            }.AsQueryable());
        }

        private void DeleteAll()
        {
            TestDbContext db = new TestDbContext();
            db.Products.RemoveRange(db.Products.ToList());
            db.Reviews.RemoveRange(db.Reviews.ToList());
            db.SaveChanges();
        }

        [TestMethod]
        public void Mock_GetIndexViewResult_Test()
        {
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_IndexProductList_Test()
        {
            DbSetup();
            ViewResult indexView = new ProductsController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void DB_CreateNewProduct_Test()
        {
            ProductsController controller = new ProductsController(db);
            Product testProduct = new Product();
            testProduct.ProductName = "TestName";

            controller.Create(testProduct);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, testProduct);
        }

        public void Dispose()
        {
            this.DeleteAll();
        }
    }
}
