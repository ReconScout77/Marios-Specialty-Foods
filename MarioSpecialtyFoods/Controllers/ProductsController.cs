using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarioSpecialtyFoods.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MarioSpecialtyFoods.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository productRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(UserManager<ApplicationUser> userManager, IProductRepository thisRepo = null)
        {
            _userManager = userManager;

            if(thisRepo == null)
            {
                this.productRepo = new EFProductRepository();
            }
            else 
            {
                this.productRepo = thisRepo;
            }
        }

        // GET: /<controller>/
        public ViewResult Index()
        {
            return View(productRepo.Products.ToList());
        }

        public IActionResult Details(int id)
        {
            Product thisProduct = productRepo.Products.Include(products => products.Reviews)
                                             .FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Product product)
        {
            productRepo.Save(product);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            productRepo.Edit(product);
            return RedirectToAction("Index");
        }

        [Authorize]
		public IActionResult Delete(int id)
		{
			Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
		}

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            productRepo.Remove(thisProduct);
            return RedirectToAction("Index");
        }
    }
}
