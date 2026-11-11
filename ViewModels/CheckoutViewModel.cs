using System.ComponentModel.DataAnnotations;
using Books.Models;

namespace Books.ViewModels;

public class CheckoutViewModel
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [Display(Name = "Full name")]
    public string FullName { get; set; } = "";

    [Required]
    [Phone]
    [StringLength(30)]
    public string Phone { get; set; } = "";

    [Required]
    [StringLength(200, MinimumLength = 5)]
    public string Address { get; set; } = "";

    public List<CartItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(item => item.Total);
}
