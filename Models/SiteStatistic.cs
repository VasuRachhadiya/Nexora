using System.ComponentModel.DataAnnotations;

namespace Nexora.Models;

public class SiteStatistic
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Label { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Value { get; set; } = string.Empty;

    [StringLength(10)]
    public string? Suffix { get; set; }

    [StringLength(10)]
    public string? Prefix { get; set; }

    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    public int TargetNumber { get; set; } = 0;

    public int Order { get; set; } = 0;
}
