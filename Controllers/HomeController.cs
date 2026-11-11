using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers;

public class HomeController : Controller
{
    private readonly ShopService shop;

    public HomeController(ShopService shop)
    {
        this.shop = shop;
    }

    public IActionResult Index()
    {
        return View("~/Views/Shop/Index.cshtml", shop.GetAll());
    }
}
