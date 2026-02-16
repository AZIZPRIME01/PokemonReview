# 🧩 PokemonReviewApp

A clean architecture ASP.NET Core Web API built with **.NET 10**, **Entity Framework Core**, and **PostgreSQL** for managing Pokémon, reviews, owners, categories, and countries.

This project demonstrates real-world backend development concepts including:

- Repository Pattern
- DTO Pattern
- Dependency Injection
- AutoMapper
- EF Core Migrations
- Clean Project Structure
- Swagger API Documentation

---

# 📦 Tech Stack

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- PostgreSQL (Npgsql)
- AutoMapper
- Swashbuckle (Swagger)
- Dependency Injection

---

# 📁 Project Structure

```
POKEMONREVIEW
│
└── PokemonReviewApp
│
├── Controllers
│ ├── CategoryController.cs
│ ├── CountryController.cs
│ ├── OwnerController.cs
│ ├── PokemonController.cs
│ ├── ReviewController.cs
│ └── ReviewerController.cs
│
├── Data
│ └── DataContext.cs
│
├── Dto
│ ├── CategoryDto.cs
│ ├── CountryDto.cs
│ ├── OwnerDto.cs
│ ├── PokemonDto.cs
│ ├── ReviewDto.cs
│ └── ReviewerDto.cs
│
├── Interfaces
│ ├── ICategoryRepository.cs
│ ├── ICountryRepository.cs
│ ├── IOwnerRepository.cs
│ ├── IPokemonRepository.cs
│ ├── IReviewRepository.cs
│ └── IReviewerRepository.cs
│
├── Repository
│ ├── CategoryRepository.cs
│ ├── CountryRepository.cs
│ ├── OwnerRepository.cs
│ ├── PokemonRepository.cs
│ ├── ReviewRepository.cs
│ └── ReviewerRepository.cs
│
├── Models
│ ├── Category.cs
│ ├── Country.cs
│ ├── Owner.cs
│ ├── Pokemon.cs
│ ├── PokemonCategory.cs
│ ├── PokemonOwner.cs
│ ├── Review.cs
│ └── Reviewer.cs
│
├── Migrations
│ ├── InitialCreate.cs
│ ├── InitialCreate.Designer.cs
│ └── DataContextModelSnapshot.cs
│
├── Helper
│ └── (AutoMapper Profile)
│
├── Seed.cs
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── PokemonReviewApp.csproj
└── PokemonReviewApp.http
```

# 🔄 CRUD Operations

The application provides full **CRUD (Create, Read, Update, Delete)** functionality for all core entities including Pokémon, Reviews, Reviewers, Owners, Categories, and Countries.

Each entity follows a consistent implementation pattern:

- **Create** → Add new records using `POST`
- **Read** → Retrieve single or multiple records using `GET`
- **Update** → Modify existing records using `PUT`
- **Delete** → Remove records using `DELETE`

CRUD logic is implemented through:

- Controller endpoints (HTTP layer)
- Repository abstraction (data access layer)
- DTO mapping with AutoMapper (data transfer)
- EF Core for database persistence

All operations return proper HTTP status codes and include validation checks to ensure data integrity and clean API behavior.
