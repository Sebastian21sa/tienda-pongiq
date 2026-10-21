# Tienda Poniq

A full-stack e-commerce platform for table tennis equipment, built with .NET and Clean Architecture. This is my third portfolio project, distinct from my ML-focused work — the goal here is to demonstrate backend engineering, database design, and software architecture skills end to end.

The domain is personal: I've played table tennis since I was 8, and this project ties directly to [PongIQ](https://pongiq-murex.vercel.app), my ML-powered table tennis technique analyzer.

## Tech Stack

- **.NET 10** / ASP.NET Core Web API
- **PostgreSQL** (via Docker)
- **Entity Framework Core** (code-first migrations)
- **Clean Architecture** (Domain / Application / Infrastructure / Api)

## Architecture

The solution follows Clean Architecture, with dependencies only ever pointing inward:

```
TiendaPoniq.Api            → depends on Application, Infrastructure
TiendaPoniq.Infrastructure → depends on Application
TiendaPoniq.Application    → depends on Domain
TiendaPoniq.Domain         → depends on nothing
```

- **Domain**: plain C# entities (`Producto`, `Categoria`, `Marca`, `VarianteProducto`, `Usuario`, `Pedido`, `ItemPedido`) with no framework dependencies.
- **Application**: interfaces and contracts (`IGenericRepository<T, TKey>`, `IUnitOfWork`) that define what the business logic needs, without knowing how it's implemented.
- **Infrastructure**: concrete implementations — EF Core `DbContext`, the generic repository, Unit of Work, and database seeding.
- **Api**: the ASP.NET Core Web API project, wiring everything together via dependency injection.

Data access goes through a generic **Repository** pattern combined with **Unit of Work**, so business logic never talks to EF Core directly, and multi-table operations commit as a single transaction.

## Project Structure

```
tienda-poniq/
├── src/
│   ├── TiendaPoniq.Domain/
│   │   ├── Entities/
│   │   └── Enums/
│   ├── TiendaPoniq.Application/
│   │   ├── Interfaces/
│   │   └── DTOs/
│   ├── TiendaPoniq.Infrastructure/
│   │   ├── Persistence/
│   │   │   ├── Seed/
│   │   │   └── Migrations/
│   │   └── Repositories/
│   └── TiendaPoniq.Api/
│       └── Controllers/
├── docker-compose.yml
├── .env                 (not committed — local secrets)
└── TiendaPoniq.sln
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Visual Studio 2026 (or any IDE with C# support)

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/Sebastian21sa/tienda-pongiq.git
   cd tienda-pongiq
   ```

2. Create a `.env` file in the project root (not tracked in git):

   ```
   POSTGRES_DB=tiendaponiq
   POSTGRES_USER=poniq_admin
   POSTGRES_PASSWORD=your-local-password
   ```

3. Make sure the password matches the connection string in `src/TiendaPoniq.Api/appsettings.json`.

4. Start the PostgreSQL container:

   ```bash
   docker compose up -d
   ```

5. Open `TiendaPoniq.sln` in Visual Studio and build the solution (this restores NuGet packages).

6. Run the API (F5, or `dotnet run --project src/TiendaPoniq.Api`). On first run, pending migrations are applied automatically and the product catalog is seeded (34 real table tennis products across 6 categories and 9 brands).

## Current Status

This project is being built day by day as part of a structured learning roadmap. Completed so far:

- [x] Clean Architecture solution scaffold
- [x] Domain model (products, categories, brands, variants, users, orders, order items)
- [x] PostgreSQL database via Docker + EF Core migrations
- [x] Generic Repository + Unit of Work pattern
- [x] Seeded product catalog (34 products)

Upcoming: authentication (ASP.NET Core Identity + JWT + 2FA), REST API endpoints, shopping cart and checkout, OWASP security hardening, event-driven order processing (RabbitMQ + SignalR), Docker Compose for the full stack, and CI/CD.

## About Me

I'm Sebastian, a Systems Engineer (Universidad de Cundinamarca) based in Colombia, looking for my first role as a software/ML engineer.

- GitHub: [@Sebastian21sa](https://github.com/Sebastian21sa)
- LinkedIn: [Sebastián Sánchez](https://www.linkedin.com/in/sebasti%C3%A1n-s%C3%A1nchez-0ab0b6314/)
- Portfolio: [web-sebastian.vercel.app](https://web-sebastian.vercel.app)
- PongIQ (flagship ML project): [pongiq-murex.vercel.app](https://pongiq-murex.vercel.app)
