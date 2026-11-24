# NEXORA — Premium Modern Web Experience

<div align="center">

![NEXORA Banner](https://images.unsplash.com/photo-1618005182384-a83a8bd57fbe?auto=format&fit=crop&w=1200&q=80)

[![.NET](https://img.shields.io/badge/.NET-10.0-8b5cf6?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-6366f1?style=for-the-badge&logo=csharp&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework](https://img.shields.io/badge/EF_Core-SQL_Server-06b6d4?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://learn.microsoft.com/ef/core/)
[![Design](https://img.shields.io/badge/Design-Awwwards_Style-ec4899?style=for-the-badge&logo=awwwards&logoColor=white)](#)

### *"Ideas Deserve Extraordinary Experiences."*

A cinematic, award-winning digital experience crafted with **ASP.NET Core MVC, C#, Razor Views, Entity Framework Core, and SQL Server**.

[Key Features](#key-features) • [Design System](#design-system) • [Architecture](#architecture) • [Getting Started](#getting-started) • [Admin Portal](#admin-portal)

</div>

---

## 🌟 Overview

**NEXORA** is designed to look and feel like an elite modern digital studio product. It rejects the generic bootstrap aesthetic in favor of **spatial computing aesthetics**, high-contrast typography, ambient lighting meshes, and 3D kinetic interactions.

---

## 🚀 Key Features

- **Cinematic Hero:** Massive editorial typography with gradient text clipping, floating 3D glass telemetry card, and scroll indicators.
- **Interactive Particle Network:** Hardware-accelerated canvas background with dynamic node repulsion.
- **Showcase & Case Studies:** Interactive gallery with category filtering tabs (*AI & Spatial, Fintech Platform, Autonomous Systems, Digital Product, Immersive 3D, Brand Architecture*).
- **Features Pillars:** 4 cards highlighting *Innovative Design, Intelligent Experience, Seamless Performance, Future-Ready Tech*.
- **Live Statistics:** Dynamic counters that animate up into view (**10K+ Experiences, 98% Satisfaction, 24/7 Accessibility, 100% Innovation**).
- **Testimonial Carousel:** Multi-slide client review carousel with star ratings and auto-rotation.
- **Global Search:** Instant modal search triggered via `Ctrl+K` or header action with real-time filtering.
- **Interactive Contact & Dispatch:** Form with client and server-side validation, database persistence, and success notification.
- **Insider Dispatch Newsletter:** AJAX email subscription with duplicate detection.
- **Authentication & Security:** ASP.NET Core Identity with password hashing, cookie management, role authorization, and anti-forgery tokens.
- **Executive Admin Portal:** Comprehensive dashboard with metrics, contact message viewer, experience artifact CRUD, and subscriber directory.

---

## 🎨 Design System

| Element | Specification |
| :--- | :--- |
| **Canvas Background** | `#050608` & `#090b10` with surface elevations at `#0e111a` and `#141826` |
| **Accents** | Electric Violet (`#8b5cf6`), Cyber Cyan (`#06b6d4`), Indigo (`#6366f1`) |
| **Typography** | `Syne` (Headings), `Plus Jakarta Sans` (Body), `Space Grotesk` (Telemetry) |
| **Glassmorphism** | `backdrop-filter: blur(20px)`, `border: 1px solid rgba(255, 255, 255, 0.08)` |
| **Interactions** | 3D card tilt, IntersectionObserver reveals, viewport counters |

---

## 🏗️ Architecture

```
Nexora/
├── Areas/Admin/          # Executive Admin portal (Dashboard, Messages, Experiences, Subscribers, Users)
├── Controllers/          # Public MVC Controllers (Home, Explore, Services, Search, Account, Newsletter)
├── Data/                 # EF Core ApplicationDbContext & Automatic Database Seeder
├── Models/               # Entity models (ApplicationUser, ExperienceItem, ContactMessage, etc.)
├── Services/             # Domain logic (ExperienceService, ContactService, StatsService, etc.)
├── ViewModels/           # Strongly-typed models for Razor Views
├── Views/                # Razor Views with custom partials and layouts
└── wwwroot/              # Custom design system CSS, interactive Canvas & 3D JS
```

---

## 🛠️ Getting Started

### Prerequisites
- [.NET 8, 9, or 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server or SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)

### Installation & Execution

1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/Nexora.git
   cd Nexora
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Run the application:**
   ```bash
   dotnet run
   ```

4. **Access the application:**
   Open your browser and navigate to:
   ```
   http://localhost:5248
   ```

The database and seed data (experiences, services, testimonials, metrics, and admin credentials) are created automatically on first launch!

---

## 🔑 Pre-Seeded Credentials

| Role | Email | Password | Access |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin@nexora.studio` | `Nexora@2026!` | Admin Area (`/Admin/Dashboard`) & Public Site |
| **Member** | `elena.rostova@nexora.studio` | `Nexora@2026!` | Member Access & Public Site |

---

## 📄 License

This project is licensed under the MIT License.
