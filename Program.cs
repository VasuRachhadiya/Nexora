using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;
using Nexora.Services;

var builder = WebApplication.CreateBuilder(args);

// Database configuration with SQL Server primary and SQLite fallback
var sqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=(localdb)\\mssqllocaldb;Database=NexoraDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;AttachDbFilename=E:\\Nexora\\NexoraDb.mdf;";
var sqliteConnection = builder.Configuration.GetConnectionString("SqliteConnection") 
    ?? "Data Source=E:\\Nexora\\Nexora.db;";
var dbProvider = builder.Configuration["DatabaseProvider"] ?? "SqlServer";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (dbProvider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(sqliteConnection);
    }
    else
    {
        try
        {
            options.UseSqlServer(sqlServerConnection, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
            });
        }
        catch
        {
            options.UseSqlite(sqliteConnection);
        }
    }
});

// Configure ASP.NET Core Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

// Register Domain Services
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<INewsletterService, NewsletterService>();
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddScoped<IServiceOfferingService, ServiceOfferingService>();
builder.Services.AddScoped<ITestimonialService, TestimonialService>();

// Add Controllers with Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Auto-seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        await DbInitializer.SeedAsync(services);
        logger.LogInformation("NEXORA database seeded successfully.");
    }
    catch (Exception ex)
    {
        logger.LogWarning(ex, "Primary database seeding encountered an issue. Initializing with local fallback SQLite context.");
        try
        {
            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.EnsureCreatedAsync();
            await DbInitializer.SeedAsync(services);
        }
        catch (Exception innerEx)
        {
            logger.LogError(innerEx, "Failed initializing database.");
        }
    }
}

// Configure HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Route configuration
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
