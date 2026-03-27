# RestApiBoilerplate

Clean Architecture ASP.NET Core REST API for `Person` CRUD with EF Core (code-first) and Swagger.

## Recent Changes

### 1. Clean Architecture Setup

The solution is split into 4 projects:

- `RestApiBoilerplate.Api` - HTTP API layer (controllers, Swagger, startup)
- `RestApiBoilerplate.Application` - use cases, service contracts, DTOs
- `RestApiBoilerplate.Domain` - core entities and enums
- `RestApiBoilerplate.Infrastructure` - EF Core persistence and repository implementations

### 2. Person CRUD

Implemented CRUD for the following fields:

- `Name`
- `BirthDate`
- `Gender`

API endpoints:

- `GET /api/people`
- `GET /api/people/{id}`
- `POST /api/people`
- `PUT /api/people/{id}`
- `DELETE /api/people/{id}`

### 3. Swagger

Swagger/OpenAPI UI is enabled in Development.

After running the API:

- Swagger UI: `http://localhost:5154/swagger`
- OpenAPI JSON: `http://localhost:5154/swagger/v1/swagger.json`

### 4. EF Core + Code-First Migration

Implemented SQL Server persistence with EF Core:

- `AppDbContext`
- `Person` entity mapping (`People` table)
- `PersonRepository` using `DbContext`

Generated initial migration in Infrastructure:

- `Persistence/Migrations/*InitialCreate*`
- `AppDbContextModelSnapshot`

Applied migration successfully, creating database:

- `RestApiBoilerplateDb`

## Database Configuration

Connection string is stored in:

- `src/RestApiBoilerplate.Api/appsettings.json`

Current value uses SQL Server LocalDB:

```json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=RestApiBoilerplateDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

## How To Run

From repository root:

```powershell
dotnet restore
dotnet build RestApiBoilerplate.sln
dotnet run --project src/RestApiBoilerplate.Api/RestApiBoilerplate.Api.csproj
```

## How To Run Migrations

### Install/restore local tools

```powershell
dotnet tool restore
```

### Add a new migration

```powershell
dotnet tool run dotnet-ef migrations add <MigrationName> \
  --project src/RestApiBoilerplate.Infrastructure/RestApiBoilerplate.Infrastructure.csproj \
  --startup-project src/RestApiBoilerplate.Api/RestApiBoilerplate.Api.csproj \
  --output-dir Persistence/Migrations
```

### Apply migrations to database

```powershell
dotnet tool run dotnet-ef database update \
  --project src/RestApiBoilerplate.Infrastructure/RestApiBoilerplate.Infrastructure.csproj \
  --startup-project src/RestApiBoilerplate.Api/RestApiBoilerplate.Api.csproj
```

## SSMS / LocalDB Connection

Use these SSMS settings:

- Server type: `Database Engine`
- Server name: `(localdb)\\MSSQLLocalDB`
- Authentication: `Windows Authentication`

LocalDB commands:

```powershell
sqllocaldb i
sqllocaldb start MSSQLLocalDB
sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "SELECT name FROM sys.databases;"
```

## Notes

- Swagger is enabled only in Development environment.
- Persistence is now EF Core-based (in-memory repository is no longer used by DI).
- For production, replace LocalDB with SQL Server instance, Azure SQL, or containerized SQL Server.
