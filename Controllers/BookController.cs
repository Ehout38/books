using Books.Services;
using Books.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers;

public class BookController : Controller
{
    private readonly BookService books;

    public BookController(BookService books)
    {
        this.books = books;
    }

    public IActionResult Index()
    {
        return View(books.GetAll());
    }

    public IActionResult Add()
    {
        return View(new BookViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(BookViewModel model)
    {
        if (model.Year > DateTime.Now.Year)
        {
            ModelState.AddModelError(nameof(model.Year), "The year cannot be in the future.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        books.Add(model);
        TempData["Message"] = "The book was added.";
        return RedirectToAction(nameof(Index));
    }
}
