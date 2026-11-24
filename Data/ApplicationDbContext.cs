using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nexora.Models;

namespace Nexora.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ExperienceItem> Experiences => Set<ExperienceItem>();
    public DbSet<ServiceOffering> Services => Set<ServiceOffering>();
    public DbSet<Testimonial> Testimonials => Set<Testimonial>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<NewsletterSubscriber> NewsletterSubscribers => Set<NewsletterSubscriber>();
    public DbSet<SiteStatistic> SiteStatistics => Set<SiteStatistic>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<NewsletterSubscriber>()
            .HasIndex(n => n.Email)
            .IsUnique();

        builder.Entity<ExperienceItem>()
            .HasIndex(e => e.Category);
    }
}
