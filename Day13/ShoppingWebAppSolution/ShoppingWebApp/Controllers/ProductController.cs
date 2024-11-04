using Microsoft.AspNetCore.Mvc;
using ShoppingWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingWebApp.Controllers
{
    public class ProductController : Controller
    {
        static List<Product> products = new List<Product>
        {
            new Product{Id=1, Name="BMW", Description="First and Comfort", Image="/images/bmw.jpg", Price=24000},
            new Product{Id=2, Name="Lamborghini", Description="Used for Racing", Price=30000, Image="/images/lamborghini.jpg"}
        };

        public IActionResult Index()
        {
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = products.Count + 1;
                // Assuming image is set in a valid way
                product.Image = "/images/" + product.Image;
                products.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int pid)
        {
            Console.WriteLine(pid);
            var product = products.FirstOrDefault(p => p.Id == pid);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int pid, Product product)
        {
            Console.WriteLine(pid);
            Product oldProduct = products.FirstOrDefault(p => p.Id == pid);
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Image = "/images/" + product.Image;
            return RedirectToAction("Index");
        }   

    }
}
