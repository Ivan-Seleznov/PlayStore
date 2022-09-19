namespace PlayStore.Models
{
    using Microsoft.EntityFrameworkCore;
    public class EFProduct : IProductRepository
    {
        private ApplicationContext context;
        public EFProduct(ApplicationContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbentry = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (dbentry != null)
                {
                    dbentry.Name = product.Name;
                    dbentry.Description = product.Description;
                    dbentry.Price = product.Price;
                    dbentry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
        public Product DeleteProduct(int ProductId)
        {
            Product product = context.Products.Find(ProductId);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }
    }
}
