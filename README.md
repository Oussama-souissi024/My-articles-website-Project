# 📰 My Articles Website – ASP.NET Core MVC

A full-featured **blog / articles portal** built with **ASP.NET Core 7 MVC**, Entity Framework Core and SQL Server.  
The platform allows administrators and authors to create, categorize, and publish rich-content articles while end-users can browse and search.

---

## ✨ Highlights

- **Clean Architecture**: separation between *Web*, *Core* (domain) & *Data* layers
- **Entity Framework Core** with Code-First migrations
- **Authentication & Roles**: Admin, Author, Reader
- **Article workflow**: create → draft → publish → edit → delete
- **Categories & Tags** for content organisation
- **Responsive UI** using Bootstrap 5 (in `wwwroot`)
- **Rich-text editing** via WYSIWYG (Quill/TinyMCE integration pending)
- **Image upload & storage** for cover pictures
- **Search & filter** across title, category or author
- **Unit tests** (xUnit) for business logic (if enabled in `Tests/`)

---

## 🗂️ Solution Overview

```
My-articles-website-Project-main
│
├── My articles website Project/     # ASP.NET Core MVC frontend & API
│   ├── Controllers/                 # Post, Category, Author controllers
│   ├── Views/                       # Razor views (Posts, Categories…)
│   ├── Pages/                       # Razor Pages if any
│   ├── Data/                        # EF Core context & migrations
│   ├── Program.cs                   # Minimal hosting model (top-level)
│   └── wwwroot/                     # Static assets (css, js, img)
│
├── MyArticlesWebsiteProject.Core/   # Domain & service layer
│   ├── Entities/                    # POCOs (Post, Category, Author…)
│   └── Interfaces/                  # Repository & service contracts
│
├── MyArticlesWebsiteProjectData/    # Data-access implementations
│   └── Repositories/                # EF Core repository classes
│
└── My Articles Website Project.sln  # Visual Studio solution
```

---

## 🛠 Requirements

- **.NET 7 SDK**
- **SQL Server Express / LocalDB**
- **Visual Studio 2022** (or VS Code + C# extensions)

---

## 🚀 Getting Started

1. **Clone** the repo:
   ```bash
   git clone https://github.com/<your-org>/My-articles-website-Project.git
   cd My-articles-website-Project-main
   ```
2. **DB Connection**: update `appsettings.json` in *My articles website Project* with your SQL connection.
3. **Run migrations** (first-time setup):
   ```bash
   dotnet ef database update --project My articles website Project
   ```
4. **Launch**:
   ```bash
   dotnet run --project "My articles website Project"
   ```
5. Browse to `https://localhost:5001/` – login / register to begin.

---

## 🔑 Default Roles & Access

| Role | Capabilities |
|------|--------------|
| **Admin** | full CRUD on posts, categories, users |
| **Author** | manage own posts |
| **Reader** | read-only access |

Update role seeding in `Data/DbInitializer.cs` if needed.

---

## 🖇️ Endpoints Snapshot

| Method | URL | Notes |
|--------|-----|-------|
| GET | `/posts` | List articles |
| GET | `/posts/{slug}` | View post |
| GET | `/admin/posts/create` | New post form |
| POST | `/admin/posts/create` | Create article |
| GET | `/categories` | Category list |

---

## 🤝 Contributing

1. Fork → `git checkout -b feature/xyz` → code → PR.
2. Ensure tests & lints pass: `dotnet test`.

---

## 📜 License

MIT (see `LICENSE`).

---

## 👤 Author

**Oussama Souissi** – [GitHub](https://github.com/Oussama-souissi024)
