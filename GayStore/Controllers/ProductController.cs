using PlayStore.Models;
using PlayStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PlayStore.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 4;
        private IProductRepository repository;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
            int p = PageSize;
        }
        public ViewResult List(string category, int productPage = 1)
        {
            if (category == String.Empty || category == null)
            {
                return View(new ProductListViewModel
                {

                    Products = repository.Products
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Products.Count()
                    },
                    CurrentCategory = category
                });
            }
            return View(new ProductListViewModel
            {

                Products = repository.Products
                .Where(p => p.Category == null || p.Category == category)
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products
                    .Where(p => p.Category == category)
                    .Count()
                },
                CurrentCategory = category
            });

        }
    }
}

