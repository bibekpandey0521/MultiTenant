# MultiTenant - ABC School Management System

A **multi-tenant school management system** built with **ASP.NET Core, Entity Framework Core, ASP.NET Core Identity, and Finbuckle.MultiTenant**.

The system is designed to allow multiple schools to use the same application while keeping each school's data isolated.

---

## 📌 Repository Overview

**MultiTenant** is a school management system designed with a **multi-tenant architecture**. The repository follows a layered architecture with separate **Domain, Application, Infrastructure, and WebApi** projects.

The project uses **ASP.NET Core Identity** for authentication and authorization, **Entity Framework Core** for data access, **SQL Server** for persistence, and **Finbuckle.MultiTenant** for tenant management and data isolation.

The project is currently under active development, with additional school management modules planned.

---

## 🚧 Project Status

**Status: In Development**

This project is currently under active development. The architecture, database structure, and features may change as development continues.

---

## 🎯 Project Goals

The main goals of this project are:

* Support multiple schools using a single application
* Secure tenant data isolation
* Tenant-specific database configuration
* Authentication and authorization
* User and role management
* School management
* Student management
* Teacher management
* Academic management
* Attendance management
* Examination and result management
* Reporting
* Scalable and maintainable architecture

---

## 🏗️ Architecture

The project follows a layered architecture.

```text
MultiTenant
│
├── Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Features
│
├── Domain
│   └── Entities
│
├── Infrastructure
│   ├── Contexts
│   ├── Identity
│   ├── Tenancy
│   ├── Configurations
│   └── Persistence
│
├── WebApi
│   ├── Controllers
│   ├── Middleware
│   └── Configuration
│
├── ABCSchool.slnx
└── README.md
```

### Domain

Contains the core business entities and domain logic.

The Domain layer should remain independent of infrastructure and external frameworks whenever possible.

### Application

Contains application-level business logic, use cases, DTOs, interfaces, services, and validation.

### Infrastructure

Contains implementations for:

* Entity Framework Core
* SQL Server
* ASP.NET Core Identity
* Finbuckle.MultiTenant
* Database contexts
* Entity configurations
* Persistence
* External services

### WebApi

Provides the HTTP API for the application.

Responsibilities include:

* API endpoints
* Controllers
* Authentication
* Authorization
* Middleware
* Request/response handling

---

# 🏢 Multi-Tenancy

The project uses **Finbuckle.MultiTenant** for multi-tenant support.

Each school represents a tenant.

```text
Tenant A
└── School A
    ├── Users
    ├── Students
    ├── Teachers
    ├── Classes
    └── Academic Data

Tenant B
└── School B
    ├── Users
    ├── Students
    ├── Teachers
    ├── Classes
    └── Academic Data
```

A user belonging to one school must not be able to access another school's data.

### Tenant Isolation

Tenant isolation is a core requirement of the system.

Tenant-aware entities are configured using Finbuckle's multi-tenant support.

Example:

```csharp
builder
    .ToTable("Schools", "Academics")
    .IsMultiTenant();
```

---

# 🔐 Authentication & Identity

The project uses **ASP.NET Core Identity** for authentication and authorization.

Custom Identity models include:

```text
ApplicationUser
ApplicationRole
ApplicationRoleClaim
```

The Identity context is integrated with Finbuckle.MultiTenant.

### Identity Tables

Identity tables are organized under the `Identity` schema.

```text
Identity
├── Users
├── Roles
├── UserRoles
├── UserClaims
├── UserLogins
├── UserTokens
├── RoleClaim
└── UserPasskeys
```

Custom table names are used instead of the default ASP.NET Identity names such as `AspNetUsers` and `AspNetRoles`.

---

# 🗄️ Database

The project uses:

* SQL Server
* Entity Framework Core
* EF Core Migrations
* Finbuckle.MultiTenant

## Database Schemas

### Identity

```text
Identity
├── Users
├── Roles
├── UserRoles
├── UserClaims
├── UserLogins
├── UserTokens
├── RoleClaim
└── UserPasskeys
```

### Academics

```text
Academics
├── Schools
├── Students
├── Teachers
├── Classes
└── ...
```

Additional schemas will be introduced as new modules are added.

---

# ⚙️ Entity Configuration

Entity configuration is separated from the `DbContext` using:

```csharp
IEntityTypeConfiguration<TEntity>
```

Example:

```csharp
internal class SchoolConfig : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder
            .ToTable("Schools", "Academics")
            .IsMultiTenant();
    }
}
```

Configurations can be applied using:

```csharp
builder.ApplyConfigurationsFromAssembly(
    typeof(DbConfigurations).Assembly);
```

This keeps database configuration organized and prevents the `DbContext` from becoming too large.

---

# 📦 Technologies

| Technology            | Purpose                        |
| --------------------- | ------------------------------ |
| C#                    | Programming Language           |
| .NET                  | Application Platform           |
| ASP.NET Core          | Web Framework                  |
| Entity Framework Core | ORM / Data Access              |
| SQL Server            | Database                       |
| ASP.NET Core Identity | Authentication & Authorization |
| Finbuckle.MultiTenant | Multi-Tenancy                  |
| REST API              | Application API                |

---

# 🛠️ Prerequisites

Before running the project, install:

* .NET SDK
* SQL Server
* Git
* Entity Framework Core CLI

Check .NET:

```bash
dotnet --version
```

Check EF Core:

```bash
dotnet ef
```

If EF Core CLI is not installed:

```bash
dotnet tool install --global dotnet-ef
```

---

# 🚀 Getting Started

## 1. Clone the Repository

```bash
git clone https://github.com/bibekpandey0521/MultiTenant.git
```

Move into the repository:

```bash
cd MultiTenant
```

## 2. Restore Dependencies

```bash
dotnet restore
```

## 3. Build the Solution

```bash
dotnet build
```

---

# 🗃️ Database Configuration

Configure your SQL Server connection string in the appropriate application configuration.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ABCSchool;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

> **Important:** Never commit production passwords, connection strings, API keys, or other secrets to GitHub.

---

# 🔄 Entity Framework Migrations

Create a migration:

```bash
dotnet ef migrations add InitialCreate
```

Apply the migration:

```bash
dotnet ef database update
```

If migrations are located in the `Infrastructure` project:

```bash
dotnet ef migrations add InitialCreate \
    --project Infrastructure \
    --startup-project WebApi
```

Apply the migration:

```bash
dotnet ef database update \
    --project Infrastructure \
    --startup-project WebApi
```

---

# ▶️ Run the Application

Run the application:

```bash
dotnet run --project WebApi
```

For development:

```bash
dotnet watch --project WebApi
```

---

# 🏫 Current Features

### Foundation

* [x] .NET solution setup
* [x] Domain project
* [x] Application project
* [x] Infrastructure project
* [x] WebApi project
* [x] Entity Framework Core
* [x] SQL Server
* [x] ASP.NET Core Identity
* [x] Finbuckle.MultiTenant
* [x] Multi-tenant Identity
* [x] School entity
* [x] Entity configurations

---

# 📚 Planned Modules

## Tenant Management

* [ ] Tenant creation
* [ ] Tenant resolution
* [ ] Tenant administration
* [ ] Tenant onboarding
* [ ] Tenant-specific configuration

## School Management

* [ ] School profile
* [ ] Students
* [ ] Teachers
* [ ] Classes
* [ ] Sections
* [ ] Subjects
* [ ] Academic years
* [ ] Departments

## Academic Management

* [ ] Attendance
* [ ] Exams
* [ ] Results
* [ ] Grading
* [ ] Timetable
* [ ] Assignments

## Administration

* [ ] User management
* [ ] Role management
* [ ] Permission management
* [ ] Audit logging

## Communication

* [ ] Notifications
* [ ] Announcements
* [ ] Email integration

## Reporting

* [ ] Student reports
* [ ] Attendance reports
* [ ] Examination reports
* [ ] Academic reports
* [ ] Dashboard

## Future

* [ ] Parent Portal
* [ ] Student Portal
* [ ] Teacher Portal
* [ ] Fee Management
* [ ] Library Management
* [ ] Mobile Application

---

# 👥 Planned Roles

The system may support roles such as:

```text
SuperAdmin
SchoolAdmin
Teacher
Student
Parent
Accountant
Staff
```

The final role and permission structure will be defined during development.

---

# 🔒 Security

Security and tenant isolation are major requirements.

The application should ensure:

1. Users can only access resources belonging to their tenant.
2. Tenant information is resolved securely.
3. Authorization is applied to protected resources.
4. Tenant boundaries are validated for tenant-specific operations.
5. Sensitive information is not exposed through API responses.
6. Production secrets are not committed to source control.

---

# 🧪 Testing

Testing will be introduced progressively.

Planned tests include:

* Unit Tests
* Integration Tests
* API Tests
* Authentication Tests
* Authorization Tests
* Multi-Tenant Isolation Tests

Multi-tenant isolation tests are particularly important to verify that one school's users cannot access another school's data.

---

# 🌿 Branch Naming

Recommended branch naming:

```text
feature/<feature-name>
bugfix/<bug-name>
hotfix/<issue-name>
refactor/<area-name>
docs/<documentation-name>
```

Examples:

```text
feature/student-management
feature/tenant-management
feature/attendance
bugfix/tenant-resolution
refactor/identity
docs/update-readme
```

---

# 📝 Commit Convention

Recommended commit messages:

```text
feat: add student management
fix: resolve tenant connection issue
refactor: improve identity configuration
docs: update README
test: add tenant isolation tests
chore: update dependencies
```

---

# 🛣️ Roadmap

### Phase 1 — Foundation

* [x] Project architecture
* [x] Entity Framework Core
* [x] ASP.NET Core Identity
* [x] Finbuckle.MultiTenant
* [x] Multi-tenant Identity
* [x] School entity
* [x] Entity configurations

### Phase 2 — Tenant Management

* [ ] Tenant creation
* [ ] Tenant resolution
* [ ] Tenant administration
* [ ] Tenant onboarding

### Phase 3 — School Management

* [ ] Students
* [ ] Teachers
* [ ] Classes
* [ ] Sections
* [ ] Subjects
* [ ] Academic years

### Phase 4 — Academic Management

* [ ] Attendance
* [ ] Exams
* [ ] Results
* [ ] Grading
* [ ] Timetable

### Phase 5 — Administration

* [ ] User management
* [ ] Role management
* [ ] Permissions
* [ ] Audit logs

---

# 🤝 Contributing

This project is currently under active development.

When contributing:

1. Create a feature branch.
2. Keep changes focused.
3. Follow the existing architecture.
4. Ensure tenant isolation.
5. Add tests where appropriate.
6. Keep migrations consistent.
7. Update documentation when necessary.

---

# ⚠️ Development Status

This project is currently in the early development stage.

Architecture and implementation details may change as new requirements are introduced.

The README will be updated as the project evolves.

---

# 📄 License

License information will be added when the project is ready for release.

---

## 👨‍💻 Project

**MultiTenant - ABC School Management System**

Built with  using:

**ASP.NET Core · Entity Framework Core · ASP.NET Core Identity · Finbuckle.MultiTenant · SQL Server**
