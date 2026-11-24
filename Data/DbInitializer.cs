using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nexora.Models;

namespace Nexora.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await context.Database.EnsureCreatedAsync();

        // Seed Roles
        string[] roles = ["Admin", "Member"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed Admin User
        var adminEmail = "admin@nexora.studio";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FullName = "Alexander Vance",
                Bio = "Creative Technologist & Executive Design Director at NEXORA Labs.",
                AvatarUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?auto=format&fit=crop&w=400&q=80",
                CreatedAt = DateTime.UtcNow
            };
            var result = await userManager.CreateAsync(adminUser, "Nexora@2026!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Seed Member User
        var memberEmail = "elena.rostova@nexora.studio";
        var memberUser = await userManager.FindByEmailAsync(memberEmail);
        if (memberUser == null)
        {
            memberUser = new ApplicationUser
            {
                UserName = memberEmail,
                Email = memberEmail,
                EmailConfirmed = true,
                FullName = "Elena Rostova",
                Bio = "Principal Spatial Engineer & Interaction Designer.",
                AvatarUrl = "https://images.unsplash.com/photo-1580489944761-15a19d654956?auto=format&fit=crop&w=400&q=80",
                CreatedAt = DateTime.UtcNow
            };
            var result = await userManager.CreateAsync(memberUser, "Nexora@2026!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(memberUser, "Member");
            }
        }

        // Seed Site Statistics
        if (!await context.SiteStatistics.AnyAsync())
        {
            context.SiteStatistics.AddRange(
                new SiteStatistic
                {
                    Label = "Experiences",
                    Value = "10K+",
                    Suffix = "+",
                    TargetNumber = 10000,
                    Description = "Global digital touchpoints and immersive environments shipped.",
                    Order = 1
                },
                new SiteStatistic
                {
                    Label = "Satisfaction",
                    Value = "98%",
                    Suffix = "%",
                    TargetNumber = 98,
                    Description = "Tier-1 enterprise client satisfaction and retention rate.",
                    Order = 2
                },
                new SiteStatistic
                {
                    Label = "Accessibility",
                    Value = "24/7",
                    Suffix = "/7",
                    TargetNumber = 24,
                    Description = "Continuous high-reliability uptime across decentralized edge infrastructure.",
                    Order = 3
                },
                new SiteStatistic
                {
                    Label = "Innovation",
                    Value = "100%",
                    Suffix = "%",
                    TargetNumber = 100,
                    Description = "Proprietary design architecture built without generic cookie-cutter templates.",
                    Order = 4
                }
            );
            await context.SaveChangesAsync();
        }

        // Seed Showcase Experiences
        if (!await context.Experiences.AnyAsync())
        {
            context.Experiences.AddRange(
                new ExperienceItem
                {
                    Title = "Aura Neural Spatial OS",
                    Subtitle = "Next-generation spatial computing interface for cognitive computing hardware",
                    Category = "AI & Spatial",
                    Description = "An award-winning spatial design system merging zero-latency neural inputs with reactive 3D volumetric glass surfaces. Built for spatial headsets and holographic glass displays.",
                    Client = "Aura Dynamics",
                    Year = "2026",
                    TechStack = "Three.js, WebGL 2.0, WebXR, ASP.NET Core, GLSL Shaders",
                    MetricResult = "4.2x Faster Cognition",
                    ImageUrl = "https://images.unsplash.com/photo-1618005182384-a83a8bd57fbe?auto=format&fit=crop&w=1200&q=80",
                    LiveDemoUrl = "#",
                    AccentColor = "#8b5cf6",
                    IsFeatured = true,
                    DisplayOrder = 1
                },
                new ExperienceItem
                {
                    Title = "Quantum Vault Fintech",
                    Subtitle = "Cryptographic wealth management with real-time biometric telemetry",
                    Category = "Fintech Platform",
                    Description = "Ultra-secure institutional liquidity platform with live predictive transaction visualizer and generative dark-mode typography.",
                    Client = "Vanguard Synthetics",
                    Year = "2025",
                    TechStack = "React 19, TypeScript, SignalR, WebAssembly, EF Core",
                    MetricResult = "$14.8B Volume Handled",
                    ImageUrl = "https://images.unsplash.com/photo-1639762681485-074b7f938ba0?auto=format&fit=crop&w=1200&q=80",
                    LiveDemoUrl = "#",
                    AccentColor = "#06b6d4",
                    IsFeatured = true,
                    DisplayOrder = 2
                },
                new ExperienceItem
                {
                    Title = "Kinesis Autonomous Mobility",
                    Subtitle = "Fleet orchestration visualizer and dynamic vehicle telemetry",
                    Category = "Autonomous Systems",
                    Description = "High-density map visualization and real-time path planning dashboard rendering over 50,000 concurrent autonomous vehicles at 60 FPS.",
                    Client = "Kinesis Robotics",
                    Year = "2026",
                    TechStack = "C#, Blazor / ASP.NET MVC, WebGL, Leaflet, GeoJSON",
                    MetricResult = "99.999% Reliability",
                    ImageUrl = "https://images.unsplash.com/photo-1508739773434-c26b3d09e071?auto=format&fit=crop&w=1200&q=80",
                    LiveDemoUrl = "#",
                    AccentColor = "#3b82f6",
                    IsFeatured = true,
                    DisplayOrder = 3
                },
                new ExperienceItem
                {
                    Title = "Solis Energy Grid Matrix",
                    Subtitle = "Planetary clean energy distribution & predictive microgrid management",
                    Category = "Digital Product",
                    Description = "Cinematic data cockpit translating petabytes of solar and geothermal telemetry into intuitive tactile dials and luminous heatmaps.",
                    Client = "Solis Orbital",
                    Year = "2025",
                    TechStack = "ASP.NET Core, D3.js, WebGL Shaders, SQL Server",
                    MetricResult = "-38% Energy Waste",
                    ImageUrl = "https://images.unsplash.com/photo-1550751827-4bd374c3f58b?auto=format&fit=crop&w=1200&q=80",
                    LiveDemoUrl = "#",
                    AccentColor = "#10b981",
                    IsFeatured = true,
                    DisplayOrder = 4
                },
                new ExperienceItem
                {
                    Title = "Chronos Luxury Horology",
                    Subtitle = "Interactive 3D mechanical watch customizer and haute horlogerie boutique",
                    Category = "Immersive 3D",
                    Description = "Photorealistic ray-traced WebGL customization engine allowing clients to configure titanium tourbillon movements in real-time.",
                    Client = "Chronos Geneve",
                    Year = "2026",
                    TechStack = "Three.js, WebGPU, ASP.NET Core, Stripe Connect",
                    MetricResult = "+310% Online Conversions",
                    ImageUrl = "https://images.unsplash.com/photo-1522335789203-aabd1fc54bc9?auto=format&fit=crop&w=1200&q=80",
                    LiveDemoUrl = "#",
                    AccentColor = "#f59e0b",
                    IsFeatured = false,
                    DisplayOrder = 5
                },
                new ExperienceItem
                {
                    Title = "Vesper Identity & Brand Architecture",
                    Subtitle = "Futuristic kinetic brand system and generative design tokens",
                    Category = "Brand Architecture",
                    Description = "Complete digital brand metamorphosis for a hyper-growth cyber-security firm, featuring interactive vector glyphs and typography engines.",
                    Client = "Vesper Cyber Labs",
                    Year = "2025",
                    TechStack = "SVG Animation, CSS Houdini, Razor, C#",
                    MetricResult = "Awwwards Site of the Day",
                    ImageUrl = "https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?auto=format&fit=crop&w=1200&q=80",
                    LiveDemoUrl = "#",
                    AccentColor = "#ec4899",
                    IsFeatured = false,
                    DisplayOrder = 6
                }
            );
            await context.SaveChangesAsync();
        }

        // Seed Services
        if (!await context.Services.AnyAsync())
        {
            context.Services.AddRange(
                new ServiceOffering
                {
                    Title = "Innovative Design",
                    Tagline = "Aesthetic perfection meets human intuition.",
                    Description = "We create unforgettable digital brand identities, bespoke UI design systems, and cinematic digital products that define categories and captivate audiences instantly.",
                    IconName = "palette",
                    FeaturesList = "Bespoke Design Systems|Micro-Interactions & Motion|Art-Directed Visual Architecture|Kinetic Typography|Design Token Libraries",
                    Deliverables = "Full Figma Architecture, Motion Guidelines, Design Tokens, Asset Libraries",
                    IsHighlighted = true,
                    Order = 1
                },
                new ServiceOffering
                {
                    Title = "Intelligent Experience",
                    Tagline = "Cognitive interfaces powered by predictive AI.",
                    Description = "We integrate conversational intelligence, adaptive interfaces, and contextual automation directly into seamless user flows that feel organic and human-centered.",
                    IconName = "brain",
                    FeaturesList = "Predictive User Flows|Generative UI Components|Contextual AI Assistant Workflows|Multimodal Interactions|Personalization Engines",
                    Deliverables = "AI Interaction Model, Prompt Architectures, Realtime Telemetry Pipelines",
                    IsHighlighted = false,
                    Order = 2
                },
                new ServiceOffering
                {
                    Title = "Seamless Performance",
                    Tagline = "Sub-millisecond latency and fluid 120 FPS transitions.",
                    Description = "Underneath every beautiful surface lies enterprise-grade engineering. We architect zero-bloat web apps with optimized rendering, edge caching, and scalable C# backends.",
                    IconName = "zap",
                    FeaturesList = "Core Web Vitals Perfection|Hardware-Accelerated CSS & Canvas|Distributed Edge Caching|Sub-second Time-to-Interactive|Zero Memory Leaks",
                    Deliverables = "Lighthouse 100/100 Audits, Edge CDN Configuration, Stress Test Reports",
                    IsHighlighted = false,
                    Order = 3
                },
                new ServiceOffering
                {
                    Title = "Future-Ready Technology",
                    Tagline = "Architected for spatial, cloud, and decentralized scale.",
                    Description = "Modern .NET 10 & C# microservices, robust EF Core databases, WebGL/WebGPU shaders, and secure tokenized authentication engineered to endure the next decade.",
                    IconName = "cpu",
                    FeaturesList = "ASP.NET Core & EF Core Enterprise Stack|WebXR & WebGPU Spatial Rendering|Cryptographic Security & Identity|Docker & Cloud Orchestration|Automated CI/CD Workflows",
                    Deliverables = "Production Source Code, Architecture Blueprints, Deployment Runbooks",
                    IsHighlighted = true,
                    Order = 4
                }
            );
            await context.SaveChangesAsync();
        }

        // Seed Testimonials
        if (!await context.Testimonials.AnyAsync())
        {
            context.Testimonials.AddRange(
                new Testimonial
                {
                    AuthorName = "Marcus Aurel",
                    AuthorRole = "Chief Product Officer",
                    Company = "Aura Dynamics",
                    AvatarUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?auto=format&fit=crop&w=200&q=80",
                    Quote = "NEXORA did not merely design an interface; they fundamentally redefined how humans interact with our spatial computing platform. The cinematic polish and technical precision are world-class.",
                    Rating = 5,
                    IsFeatured = true,
                    Order = 1
                },
                new Testimonial
                {
                    AuthorName = "Sophia Lin",
                    AuthorRole = "VP of Design & Brand",
                    Company = "Vanguard Synthetics",
                    AvatarUrl = "https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?auto=format&fit=crop&w=200&q=80",
                    Quote = "Our conversion rate surged by 310% within 4 weeks of launch. The dark aesthetic, ambient lighting, and razor-sharp typography elevated our perception to that of an industry titan.",
                    Rating = 5,
                    IsFeatured = true,
                    Order = 2
                },
                new Testimonial
                {
                    AuthorName = "Devon Montgomery",
                    AuthorRole = "Founder & CEO",
                    Company = "Kinesis Robotics",
                    AvatarUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?auto=format&fit=crop&w=200&q=80",
                    Quote = "Working with NEXORA felt like collaborating with a team from 2035. Every interaction is silky smooth, every micro-detail is intentional, and their .NET engineering is bulletproof.",
                    Rating = 5,
                    IsFeatured = true,
                    Order = 3
                },
                new Testimonial
                {
                    AuthorName = "Aria Takahashi",
                    AuthorRole = "Head of Digital Ecosystems",
                    Company = "Solis Orbital",
                    AvatarUrl = "https://images.unsplash.com/photo-1534528741775-53994a69daeb?auto=format&fit=crop&w=200&q=80",
                    Quote = "The attention to detail is staggering. The glassmorphism, responsive behavior on every device, and instantaneous search functionality made our board of directors applaud in the demo.",
                    Rating = 5,
                    IsFeatured = true,
                    Order = 4
                }
            );
            await context.SaveChangesAsync();
        }

        // Seed Sample Contact Messages
        if (!await context.ContactMessages.AnyAsync())
        {
            context.ContactMessages.AddRange(
                new ContactMessage
                {
                    Name = "Julian Croft",
                    Email = "julian@hyperion.ai",
                    Subject = "Partnership: AI Spatial Interface for Enterprise",
                    Message = "We are seeking a partner to build a bespoke spatial telemetry portal for our enterprise clients. Your work on Aura Neural Spatial OS is exactly the aesthetic we require.",
                    Category = "Project Inquiry",
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    IsRead = true,
                    AdminNotes = "High priority enterprise lead. Follow-up meeting scheduled."
                },
                new ContactMessage
                {
                    Name = "Claire Delacroix",
                    Email = "claire@designweek.fr",
                    Subject = "Editorial Feature for NEXORA Digital Product Showcase",
                    Message = "We would love to feature NEXORA in our upcoming autumn digital design review. Can we schedule an interview with your creative director?",
                    Category = "Press",
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    IsRead = false
                }
            );
            await context.SaveChangesAsync();
        }

        // Seed Sample Subscribers
        if (!await context.NewsletterSubscribers.AnyAsync())
        {
            context.NewsletterSubscribers.AddRange(
                new NewsletterSubscriber { Email = "founder@nextgen.tech", SubscribedAt = DateTime.UtcNow.AddDays(-10), IsActive = true },
                new NewsletterSubscriber { Email = "sarah.connor@cyberdyne.io", SubscribedAt = DateTime.UtcNow.AddDays(-5), IsActive = true },
                new NewsletterSubscriber { Email = "design.director@luxurylab.ch", SubscribedAt = DateTime.UtcNow.AddDays(-1), IsActive = true }
            );
            await context.SaveChangesAsync();
        }
    }
}
