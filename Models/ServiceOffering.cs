using System.ComponentModel.DataAnnotations;

namespace Nexora.Models;

public class ServiceOffering
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(250)]
    public string Tagline { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required, StringLength(60)]
    public string IconName { get; set; } = "sparkles";

    public string FeaturesList { get; set; } = string.Empty;

    [StringLength(300)]
    public string Deliverables { get; set; } = string.Empty;

    public bool IsHighlighted { get; set; } = false;

    public int Order { get; set; } = 0;
}
