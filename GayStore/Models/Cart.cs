namespace PlayStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(i => i.Product.ProductId == product.ProductId);
        }
        public virtual decimal ComputeTotalValue()
        {
            return lineCollection.Sum(i => i.Product.Price * i.Quantity);
        }
        public virtual void Clear()
        {
            lineCollection.Clear();
        }
        public virtual IEnumerable<CartLine> Lines => lineCollection;
        
    }
}
