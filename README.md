# LibraryAPI

LibraryAPI is a RESTful Web API built with ASP.NET Core 8.0, designed to manage a library system. It supports operations for books, authors, genres, and loans, with role-based authentication using JSON Web Tokens (JWT). The project uses Entity Framework Core with PostgreSQL for data persistence and Swagger for API documentation.

## Features
- **User Authentication**: Register and login endpoints with JWT-based authentication for Admin and Member roles.
- **Role-Based Authorization**: Admin users can create, update, and delete books; all authenticated users can view books.
- **Database**: PostgreSQL with Entity Framework Core for data storage.
- **API Documentation**: Swagger UI for interactive testing.

## Project Structure
- **LibraryAPI**: Main Web API project with controllers, middleware and configuration.
- **Library.Application**: Business logic and services with automapper
- **Library.Infrastructure**: Entity Framework Core, database migrations and repository implementation
- **Library.Domain**: Entities, interfaces, specifications and custom exceptions
- **Library.Shared**: DTOs, models, and validators.

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (version 13 or higher)
- [dotnet-ef] tool 
- in Library.API add appsettings.json with your database's connection string and JWT config

## Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/LibraryAPI.git
cd LibraryAPI
```

### 2. Configure appsettings.json
```bash
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "AllowedHosts": "*",

"Jwt": {
    "Key": "Change_ThisIsASecretKeyForJwtTokenGeneration1234567890",
    "Issuer": "https://localhost:7069",
    "Audience": "https://localhost:7069"
    },

"ConnectionStrings": {
    "LibraryConnection": "Host=localhost;Port=5432;Database=LibraryDb;Username=postgres;Password=yourpassword",
    "AuthConnection": "Host=localhost;Port=5432;Database=AuthDb;Username=postgres;Password=yourpassword"
  }
}

```
### 3. Configure Databases
```bash
dotnet ef migrations add InitialCreate --project /Library.Infrastructure/ --startup-project /Library.API/
dotnet ef database update --project /Library.Infrastructure/ --startup-project /Library.API/
