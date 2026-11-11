using System.Text.Json;
using Books.Services;
using Books.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers;

public class ShopController : Controller
{
    private readonly ShopService shop;
    private readonly CartService cart;

    public ShopController(ShopService shop, CartService cart)
    {
        this.shop = shop;
        this.cart = cart;
    }

    public IActionResult Index()
    {
        return View(shop.GetAll());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(int id)
    {
        TempData["Message"] = cart.Add(id) ? "The product was added to your cart." : "This product is out of stock.";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Cart()
    {
        return View(cart.GetCart());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        cart.Remove(id);
        return RedirectToAction(nameof(Cart));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        cart.Clear();
        return RedirectToAction(nameof(Cart));
    }

    public IActionResult Checkout()
    {
        var items = cart.GetCart().Items;
        return items.Count == 0 ? RedirectToAction(nameof(Cart)) : View(new CheckoutViewModel { Items = items });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Checkout([Bind("FullName,Phone,Address")] CheckoutViewModel model)
    {
        model.Items = cart.GetCart().Items;
        if (model.Items.Count == 0)
        {
            return RedirectToAction(nameof(Cart));
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (!shop.CompleteOrder(model.Items))
        {
            ModelState.AddModelError("", "A product is no longer available in this quantity. Update your cart.");
            return View(model);
        }

        model.FullName = model.FullName.Trim();
        model.Phone = model.Phone.Trim();
        model.Address = model.Address.Trim();
        HttpContext.Session.SetString("Order", JsonSerializer.Serialize(model));
        cart.Clear();
        return RedirectToAction(nameof(Summary));
    }

    public IActionResult Summary()
    {
        var json = HttpContext.Session.GetString("Order");
        return json == null ? RedirectToAction(nameof(Index)) : View(JsonSerializer.Deserialize<CheckoutViewModel>(json));
    }
}
