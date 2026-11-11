using System.Text.Json;
using Books.Models;
using Books.ViewModels;

namespace Books.Services;

public class CartService
{
    private readonly IHttpContextAccessor accessor;
    private readonly ShopService shop;
    private ISession Session => accessor.HttpContext!.Session;

    public CartService(IHttpContextAccessor accessor, ShopService shop)
    {
        this.accessor = accessor;
        this.shop = shop;
    }

    private Dictionary<int, int> GetQuantities()
    {
        var json = Session.GetString("Cart");
        return json == null ? new Dictionary<int, int>() : JsonSerializer.Deserialize<Dictionary<int, int>>(json)!;
    }

    public CartViewModel GetCart()
    {
        var quantities = GetQuantities();
        return new CartViewModel
        {
            Items = shop.GetAll().Where(product => quantities.ContainsKey(product.Id)).Select(product => new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantities[product.Id]
            }).ToList()
        };
    }

    public bool Add(int id)
    {
        var product = shop.GetAll().FirstOrDefault(product => product.Id == id);
        var quantities = GetQuantities();
        quantities.TryGetValue(id, out var count);

        if (product == null || count >= product.Quantity)
        {
            return false;
        }

        quantities[id] = count + 1;
        Session.SetString("Cart", JsonSerializer.Serialize(quantities));
        return true;
    }

    public void Remove(int id)
    {
        var quantities = GetQuantities();
        quantities.Remove(id);
        Session.SetString("Cart", JsonSerializer.Serialize(quantities));
    }

    public void Clear()
    {
        Session.Remove("Cart");
    }
}
