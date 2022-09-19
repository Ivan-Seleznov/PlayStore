using Microsoft.EntityFrameworkCore;

namespace PlayStore.Models
{
    public class EFOrderRepository:IOrderRepository
    {
        public ApplicationContext Context { get; set; }

        public IQueryable<Order> Orders => Context.Orders.Include(o => o.Lines).ThenInclude(i => i.Product);

        public EFOrderRepository(ApplicationContext ctx)
        {
            Context = ctx;
        }
      
        public void SaveOrder(Order order)
        {
            Context.AttachRange(order.Lines.Select(i => i.Product));
            if (order.OrderId == 0)
            {
                Context.Orders.Add(order);
            }
            Context.SaveChanges();
        }
    }
}
