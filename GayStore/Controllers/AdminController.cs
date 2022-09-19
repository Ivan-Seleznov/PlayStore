using PlayStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PlayStore.Controllers
{
    [Authorize]
    public class AdminController:Controller
    {
        private IProductRepository productRepository;
        public AdminController(IProductRepository repo)
        {
            productRepository = repo;
        }
        public ViewResult Index()
        {
            return View(productRepository.Products);
        }
        public ViewResult Edit(int productId)
        {
            return View(productRepository.Products.FirstOrDefault(p => p.ProductId == productId));
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);   
            }
        }
        public ViewResult Create() => View("Edit", new Product());
        public IActionResult Delete(int productId)
        {
            Product deleteProduct = productRepository.DeleteProduct(productId);
            if (deleteProduct != null)
            {
                TempData["message"] = $"{deleteProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
