using Books.Models;
using Books.ViewModels;

namespace Books.Services;

public class BookService
{
    private readonly object sync = new();
    private readonly List<Book> books = new()
    {
        new Book { Id = 1, Title = "The Hobbit", Author = "J. R. R. Tolkien", Genre = "Fantasy", Year = 1937 },
        new Book { Id = 2, Title = "The Little Prince", Author = "Antoine de Saint-Exupery", Genre = "Fiction", Year = 1943 },
        new Book { Id = 3, Title = "Clean Code", Author = "Robert C. Martin", Genre = "Programming", Year = 2008 }
    };
    private int nextId = 4;

    public List<BookViewModel> GetAll()
    {
        lock (sync)
        {
            return books.Select(book => new BookViewModel
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Year = book.Year
            }).ToList();
        }
    }

    public void Add(BookViewModel model)
    {
        lock (sync)
        {
            books.Add(new Book
            {
                Id = nextId++,
                Title = model.Title.Trim(),
                Author = model.Author.Trim(),
                Genre = model.Genre.Trim(),
                Year = model.Year
            });
        }
    }
}
