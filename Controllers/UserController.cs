using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers;

public class UserController : Controller
{
    private readonly UserService users;

    public UserController(UserService users)
    {
        this.users = users;
    }

    public IActionResult Register()
    {
        return View(new User());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RegisterPost(User user)
    {
        if (!ModelState.IsValid)
        {
            return View("Register", user);
        }

        if (!users.Register(user))
        {
            ModelState.AddModelError("", "This username or email is already registered.");
            return View("Register", user);
        }

        TempData["Username"] = user.Username.Trim();
        return RedirectToAction(nameof(Success));
    }

    public IActionResult Success()
    {
        var username = TempData["Username"] as string;
        return username == null ? RedirectToAction(nameof(Register)) : View("Success", username);
    }
}
