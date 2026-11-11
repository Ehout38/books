using System.ComponentModel.DataAnnotations;

namespace Books.ViewModels;

public class BookViewModel
{
    [Required]
    [StringLength(120)]
    public string Title { get; set; } = "";

    [Required]
    [StringLength(100)]
    public string Author { get; set; } = "";

    [Required]
    [StringLength(60)]
    public string Genre { get; set; } = "";

    [Required]
    [Range(1, 9999)]
    public int Year { get; set; } = DateTime.Now.Year;
}
