using System.ComponentModel.DataAnnotations;

namespace Books.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Username { get; set; } = "";

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(100, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Required]
    [Range(13, 120)]
    public int? Age { get; set; }
}
