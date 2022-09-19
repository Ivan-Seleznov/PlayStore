using PlayStore.Models;
using Microsoft.AspNetCore.Mvc;
namespace PlayStore.Components
{
    public class NavigationMenuViewComponent:ViewComponent //пример компонента представления
    {
        public NavigationMenuViewComponent(IProductRepository repository)
        {
            this.repository = repository;

        }
        private IProductRepository repository;
        public IViewComponentResult Invoke() //Метод вызываеься когда компонент применяеься в razor.
                               //А результат вставляется в html разметку
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products.Select(c => c.Category).Distinct().OrderBy(c => c));
        }
    }
}
