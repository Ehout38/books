using Books.Models;

namespace Books.Services;

public class ShopService
{
    private readonly object sync = new();
    private readonly List<Product> products = new()
    {
        new Product { Id = 1, Name = "Notebook", Quantity = 20, Price = 4.50m },
        new Product { Id = 2, Name = "Pen set", Quantity = 15, Price = 3.25m },
        new Product { Id = 3, Name = "Backpack", Quantity = 8, Price = 35m },
        new Product { Id = 4, Name = "Desk lamp", Quantity = 10, Price = 22.90m }
    };

    public List<Product> GetAll()
    {
        lock (sync)
        {
            return products.Select(product => new Product
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price
            }).ToList();
        }
    }

    public bool CompleteOrder(List<CartItem> items)
    {
        lock (sync)
        {
            if (items.Count == 0 || items.Any(item => item.Quantity < 1
                || !products.Any(product => product.Id == item.ProductId && product.Quantity >= item.Quantity)))
            {
                return false;
            }

            foreach (var item in items)
            {
                products.First(product => product.Id == item.ProductId).Quantity -= item.Quantity;
            }

            return true;
        }
    }
}
