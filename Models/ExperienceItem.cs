using System.ComponentModel.DataAnnotations;

namespace Nexora.Models;

public class ExperienceItem
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Subtitle { get; set; } = string.Empty;

    [Required, StringLength(60)]
    public string Category { get; set; } = "Digital Product";

    [Required]
    public string Description { get; set; } = string.Empty;

    [StringLength(100)]
    public string Client { get; set; } = "Nexora Labs";

    [StringLength(20)]
    public string Year { get; set; } = "2026";

    [StringLength(200)]
    public string TechStack { get; set; } = "WebGL, React, ASP.NET Core, Three.js";

    [StringLength(100)]
    public string MetricResult { get; set; } = "+240% Performance";

    [Required, StringLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    [StringLength(500)]
    public string? LiveDemoUrl { get; set; }

    [StringLength(30)]
    public string AccentColor { get; set; } = "#8b5cf6";

    public bool IsFeatured { get; set; } = true;

    public int DisplayOrder { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
