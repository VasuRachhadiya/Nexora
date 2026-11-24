using System.ComponentModel.DataAnnotations;

namespace Nexora.Models;

public class Testimonial
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string AuthorName { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string AuthorRole { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string Company { get; set; } = string.Empty;

    [Required, StringLength(500)]
    public string AvatarUrl { get; set; } = string.Empty;

    [Required, StringLength(1000)]
    public string Quote { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Rating { get; set; } = 5;

    public bool IsFeatured { get; set; } = true;

    public int Order { get; set; } = 0;
}
