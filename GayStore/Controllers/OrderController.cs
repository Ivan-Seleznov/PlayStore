using PlayStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlayStore.Controllers
{
    public class OrderController:Controller
    {
        public bool Shipped { get; set; }
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repo, Cart cartservice)
        {
            repository = repo;
            cart = cartservice;
        }
        public ViewResult Checkout()
        {
            return View(new Order());
        }
        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() ==0)
            {
                ModelState.AddModelError("","Sorry, your cart is empty");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
        [Authorize]
        public ViewResult List()
        {
            return View(repository.Orders.Where(o => !o.Shipped));
        }
        [Authorize]
        public IActionResult MarkShipped(int orderId)
        {
            Order order = repository.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
    }
}
