using Books.Models;

namespace Books.ViewModels;

public class CartViewModel
{
    public List<CartItem> Items { get; set; } = new();
    public int Count => Items.Sum(item => item.Quantity);
    public decimal Total => Items.Sum(item => item.Total);
}
