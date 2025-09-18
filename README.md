# Picsart-LibraryAPI

Library API — layered .NET Web API (Repository / Service / DTO patterns) using EF Core + PostgreSQL.

## Project layout
- `Library.API` — ASP.NET Web API (controllers, startup)
- `Library.Application` — services and interfaces
- `Library.Domain` — entities and repository interfaces
- `Library.Infrastructure` — EF Core DbContext & repository implementations
- `Library.Shared` — DTOs, Creation/Update models

## Prerequisites
- .NET SDK (recommended: .NET 7/8 compatible with your solution)
- PostgreSQL (local or remote)
- `dotnet-ef` tool (optional; only needed for migrations)

## Local setup (quick)
1. Clone:
   ```bash
   git clone https://github.com/<YourUsername>/Picsart-LibraryAPI.git
   cd Picsart-LibraryAPI

