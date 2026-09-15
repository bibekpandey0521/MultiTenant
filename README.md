# ABC School Management System

A scalable **multi-tenant school management system** built with **ASP.NET Core**, **Entity Framework Core**, **ASP.NET Core Identity**, and **Finbuckle.MultiTenant**.

The system is designed to support multiple schools from a single application while keeping each school's data isolated.

---

## 🚧 Project Status

**Status:** In Development

This project is currently under active development.

Features, database structure, and architecture may change as development progresses.

---

## 🎯 Project Overview

The goal of this project is to build a modern school management platform where multiple schools can use the same application.

Each school is represented as a **tenant**.

### Example

```text
School A (Tenant A)
├── Users
├── Students
├── Teachers
├── Classes
├── Subjects
└── Academic Data

School B (Tenant B)
├── Users
├── Students
├── Teachers
├── Classes
├── Subjects
└── Academic Data
```

Users from one school must not be able to access data belonging to another school.

---

# 🏗️ Architecture

The project follows a layered architecture with a strong separation of concerns.

```text
ABCSchool
│
├── Domain
│   ├── Entities
│   └── Common
│
├── Application
│   ├── Features
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Common
│
├── Infrastructure
│   ├── Contexts
│   ├── Identity
│   ├── Tenancy
│   ├── Persistence
│   └── Configurations
│
└── API
    ├── Controllers
    ├── Middleware
    ├── Endpoints
    └── Configuration
```

### Layer Responsibilities

#### Domain

Contains the core business entities and business rules.

```text
Domain
└── Entities
    ├── School
    ├── Student
    ├── Teacher
    └── ...
```

The Domain layer should remain independent of infrastructure and framework-specific implementations.

---

#### Application

Contains application business logic and use cases.

Responsibilities include:

* DTOs
* Interfaces
* Commands
* Queries
* Services
* Validation
* Application-specific business rules

---

#### Infrastructure

Contains implementations that interact with external systems.

Responsibilities include:

* Entity Framework Core
* Database context
* Identity
* Multi-tenancy
* Entity configurations
* Repositories
* External services

---

#### API

Provides access to the application through HTTP APIs.

Responsibilities include:

* Controllers
* Endpoints
* Authentication
* Authorization
* Middleware
* Request/response handling

---

# 🏢 Multi-Tenancy

The project uses **Finbuckle.MultiTenant** for multi-tenant support.

Each school is treated as an individual tenant.

```text
Tenant
│
├── Tenant Information
│   ├── Id
│   ├── Identifier
│   ├── Name
│   └── Connection String
│
└── School Data
    ├── Students
    ├── Teachers
    ├── Classes
    └── ...
```

### Tenant Isolation

Tenant isolation is a core requirement.

For example:

```text
Tenant A
├── Student A1
├── Student A2
└── Teacher A1

Tenant B
├── Student B1
├── Student B2
└── Teacher B1
```

A user belonging to **Tenant A** must never be able to access resources belonging to **Tenant B**.

---

# 🗄️ Database Strategy

The application uses:

* **SQL Server**
* **Entity Framework Core**
* **EF Core Migrations**
* **Multi-tenant database configuration**

The project currently supports tenant-aware database configuration.

Depending on the final deployment strategy, tenants can use separate databases or another supported tenant isolation strategy.

---

# 🔐 Identity

Authentication and authorization are implemented using **ASP.NET Core Identity**.

Custom Identity models include:

```text
ApplicationUser
ApplicationRole
ApplicationRoleClaim
```

The Identity context is tenant-aware through Finbuckle MultiTenant.

---

## Identity Tables

Identity tables are mapped to the `Identity` schema.

```text
Identity
│
├── Users
├── Roles
├── UserRoles
├── UserClaims
├── UserLogins
├── UserTokens
├── RoleClaim
└── UserPasskeys
```

The default ASP.NET Identity table names such as `AspNetUsers` and `AspNetRoles` are replaced with application-specific names.

---

# 🗂️ Database Schemas

The database uses separate schemas to organize different areas of the application.

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

Additional schemas may be introduced as the project grows.

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

Configurations are applied using:

```csharp
builder.ApplyConfigurationsFromAssembly(
    typeof(DbConfigurations).Assembly);
```

This keeps the `DbContext` clean and makes database configuration easier to maintain.

---

# 📦 Technologies

| Technology            | Purpose                        |
| --------------------- | ------------------------------ |
| C#                    | Programming language           |
| ASP.NET Core          | Application framework          |
| Entity Framework Core | ORM / data access              |
| SQL Server            | Database                       |
| ASP.NET Core Identity | Authentication & authorization |
| Finbuckle.MultiTenant | Multi-tenancy                  |
| REST API              | Application communication      |
| EF Core Migrations    | Database versioning            |

---

# 🛠️ Prerequisites

Before running the project, install:

* [.NET SDK](https://dotnet.microsoft.com/download)
* SQL Server
* Git
* Entity Framework Core CLI

Check your .NET version:

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

# 📥 Getting Started

## 1. Clone the Repository

```bash
git clone <repository-url>
```

Navigate into the project:

```bash
cd <project-directory>
```

---

## 2. Restore Dependencies

```bash
dotnet restore
```

---

## 3. Configure the Database

Update your development configuration.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ABCSchool;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

> Never commit production connection strings, passwords, API keys, or other secrets to source control.

---

# 🗃️ Database Migrations

Create a migration:

```bash
dotnet ef migrations add InitialCreate
```

Apply the migration:

```bash
dotnet ef database update
```

If the solution contains separate Infrastructure and API projects:

```bash
dotnet ef migrations add InitialCreate \
    --project Infrastructure \
    --startup-project API
```

Apply the database:

```bash
dotnet ef database update \
    --project Infrastructure \
    --startup-project API
```

---

# ▶️ Running the Application

Build the project:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

For development:

```bash
dotnet watch
```

---

# 👤 User & Role Management

The system uses Identity for managing:

* Users
* Roles
* Claims
* User roles
* Login information
* Authentication tokens

Example roles may include:

```text
SuperAdmin
SchoolAdmin
Teacher
Student
Parent
Accountant
Staff
```

The final role structure will be defined as the application develops.

---

# 🏫 School Management

The school module is responsible for managing school-level information.

Current entity:

```text
School
```

Planned school-related features:

* School profile
* School settings
* Departments
* Academic years
* Classes
* Sections
* Subjects
* Teachers
* Students

---

# 📚 Academic Management

Planned academic modules include:

```text
Academic
│
├── Academic Years
├── Classes
├── Sections
├── Subjects
├── Students
├── Teachers
├── Attendance
├── Exams
├── Results
├── Assignments
└── Timetable
```

---

# 👨‍🎓 Student Management

Planned student features:

* Student registration
* Student profiles
* Student admission
* Parent/guardian information
* Student enrollment
* Class assignment
* Attendance
* Examination results
* Academic history

---

# 👨‍🏫 Teacher Management

Planned teacher features:

* Teacher profiles
* Teacher accounts
* Subject assignment
* Class assignment
* Attendance management
* Examination management
* Timetable management

---

# 📊 Future Modules

The following modules are planned:

### Administration

* [ ] School administration
* [ ] User management
* [ ] Role management
* [ ] Permission management
* [ ] Audit logs

### Academic

* [ ] Students
* [ ] Teachers
* [ ] Classes
* [ ] Sections
* [ ] Subjects
* [ ] Academic years
* [ ] Attendance
* [ ] Exams
* [ ] Results
* [ ] Timetable
* [ ] Assignments

### Communication

* [ ] Notifications
* [ ] Email notifications
* [ ] Announcements
* [ ] Parent communication

### Reporting

* [ ] Student reports
* [ ] Attendance reports
* [ ] Examination reports
* [ ] Academic reports
* [ ] School dashboards

### Portals

* [ ] Admin portal
* [ ] Teacher portal
* [ ] Student portal
* [ ] Parent portal

### Future

* [ ] Mobile application
* [ ] Payment management
* [ ] Fee management
* [ ] Library management
* [ ] Transport management

---

# 🧪 Testing

Testing will be introduced progressively.

Planned test types:

```text
Tests
│
├── Unit Tests
├── Integration Tests
├── API Tests
├── Authentication Tests
├── Authorization Tests
└── Multi-Tenant Isolation Tests
```

Multi-tenant isolation testing is especially important to ensure that one school's data cannot be accessed by another school.

---

# 🔒 Security Principles

The project follows these security principles:

1. Tenant boundaries must always be enforced.
2. Client-provided tenant information must not be blindly trusted.
3. Users must only access resources belonging to their tenant.
4. Authorization must be applied to protected resources.
5. Passwords and secrets must never be stored in source control.
6. Production credentials must be stored securely.
7. Database access must be properly secured.
8. Sensitive information must not be exposed through API responses or logs.

---

# 🧭 Development Guidelines

When adding a new feature:

1. Create the domain entity if required.
2. Define application interfaces/use cases.
3. Implement infrastructure requirements.
4. Add EF Core entity configuration.
5. Make the entity tenant-aware where appropriate.
6. Add migrations.
7. Implement API endpoints.
8. Add authorization.
9. Add tests.
10. Update the documentation.

---

# 🛣️ Roadmap

## Foundation

* [x] .NET project setup
* [x] Entity Framework Core
* [x] ASP.NET Core Identity
* [x] Finbuckle.MultiTenant
* [x] Multi-tenant Identity
* [x] School entity
* [x] Entity configurations
* [ ] Tenant creation
* [ ] Tenant resolution
* [ ] Tenant administration

## School Management

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
* [ ] Assignments
* [ ] Timetable
* [ ] Grading

## Administration

* [ ] User management
* [ ] Role management
* [ ] Permissions
* [ ] Audit logging

## Communication

* [ ] Notifications
* [ ] Announcements
* [ ] Email integration

## Reporting

* [ ] Dashboards
* [ ] Student reports
* [ ] Attendance reports
* [ ] Examination reports

---

# 🤝 Contributing

This project is currently under active development.

When contributing:

1. Create a feature branch.
2. Keep changes focused.
3. Follow the existing architecture.
4. Add tests where appropriate.
5. Ensure tenant isolation.
6. Keep migrations consistent.
7. Update documentation when necessary.

Example:

```bash
git checkout -b feature/student-management
```

---

# 🌱 Branch Naming

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
feature/attendance
bugfix/tenant-resolution
refactor/identity
docs/update-readme
```

---

# 📝 Commit Convention

Recommended commit format:

```text
feat: add student management
fix: resolve tenant connection issue
refactor: improve identity configuration
docs: update README
test: add tenant isolation tests
chore: update dependencies
```

---

# ⚠️ Development Notes

This project is currently in the early development stage.

The following areas are expected to evolve:

* Database architecture
* Tenant resolution
* Identity configuration
* Domain entities
* API structure
* Authorization
* Deployment strategy

The README should be updated whenever significant architectural or functional changes are introduced.

---

# 📄 License

License information will be added when the project is ready for release.

---

## 👨‍💻 Development

**ABCSchool Management System**

Built with ❤️ using ASP.NET Core and .NET.
