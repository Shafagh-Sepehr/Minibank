# Minibank Project Architecture and Technologies

This document outlines the key C# technologies, patterns, libraries, and architectural decisions used in the Minibank project.

## Technologies & Frameworks
- **.NET 8.0**: The project targets the latest long-term support release of the .NET platform.
- **Microsoft.Extensions.DependencyInjection**: Used as the built-in Inversion of Control (IoC) container to manage service lifecycles.
- **Microsoft.Extensions.Configuration**: Utilized alongside JSON and Binder packages to load and manage application settings (e.g., `appsettings.json`).

## Libraries
- **DeepCloner / DeepCopier**: Third-party libraries used to perform deep copies of objects, likely to avoid reference mutation issues within the custom in-memory database.

## Reflection Usage
Reflection is heavily utilized, primarily to power a custom ORM-like in-memory database (`ShafaghDB`). Key applications include:
- **Dynamic Property Inspection**: Using `entity.GetType().GetProperty()` to automatically identify Primary Keys, Foreign Keys, and other constraint attributes.
- **Custom Attributes & Validation**: Validating that attributes like `ValidatorAttribute` match the correct entity types at runtime.
- **Data Sanitization**: Dynamically setting default values by inspecting property types and verifying they match the `DefaultValueAttribute`.
- **Reference Handling**: Managing object relationships and foreign-key constraints dynamically during Insert, Update, and Delete operations.

## Dependency Injection (DI)
The application relies strictly on DI for service resolution, configured centrally in `ServiceCollection.cs`.
- **Transient**: Used for stateless operations, such as Handlers (`IAccountHandler`, `IUserHandler`) and specific Entity Validators.
- **Singleton**: Used for stateful or heavy components, such as the `IShafaghDB` engine, Configuration, InMemoryRepositories, and core Validation services.

## Design Patterns & Architectural Decisions
- **Repository Pattern**: Separates data access logic from business logic via generic and specific repositories (`IEntityRepository<T>`, `InMemoryRepository`).
- **DAO (Data Access Object) Pattern**: Strong separation between domain entities and database models (e.g., `IDaoToEntity<AccountDao, Account>`, `IEntityToDao`).
- **Decorator / Wrapper Pattern**: The `IRepositoryWrapperValidator` wraps the base repository to automatically inject validation logic before any CRUD operations are executed.
- **Strategy Pattern (Validation & Sanitization)**: Various discrete validators (`IPrimaryKeyValidator`, `IForeignKeyValidator`, `IDeletionIntegrityValidator`) and handlers execute specific validation or reference-checking strategies on entities dynamically.
- **Modular Architecture**: The solution is split into distinct projects (`Abstractions`, `App`, `DB`, `InMemoryDataBase`, `MiniBank`, `Repository`, `ServiceCollection`) promoting clean separation of concerns and loose coupling.