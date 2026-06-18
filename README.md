# Minibank: Technology and Architecture Notes

## Core C# technologies and libraries
- **.NET 8 / C#** solution with multiple class-library projects and one console app.
- **Dependency Injection** via `Microsoft.Extensions.DependencyInjection`.
- **Configuration** via `Microsoft.Extensions.Configuration` + JSON binding.
- **Deep cloning** via `Force.DeepCloner` to isolate stored data from mutable runtime objects.
- **Data annotations** (`System.ComponentModel.DataAnnotations`) for entity-level validation rules.

## Reflection usage
- Attribute-based validation dispatch in domain and database layers.
- Runtime attribute inspection for `[PrimaryKey]`, `[ForeignKey]`, `[DefaultValue]`, and `[Nullable]`.
- Generic validator type checks using `MakeGenericType(...)`.
- Reflection-based member mutation helper in repository mapping updates (including backing-field fallback).

## Dependency injection approach
- Single composition root in `ServiceCollection/ServiceCollection.cs`.
- Explicit manual registrations for:
  - handlers
  - validators
  - repositories
  - DAO/entity converters and updaters
  - infrastructure services (config, SMS, in-memory DB services)
- Service lifetimes are mixed (`Singleton` + `Transient`) based on responsibility.

## Patterns used
- **Repository pattern** for data access abstraction.
- **Mapper/Adapter pattern** for DAO ↔ domain conversions.
- **Template Method pattern** in `BaseValidator<TEntity>` with specialized overrides.
- **Strategy-style composition** for DB validation and reference handling through injected interfaces.
- **Facade-like wrapper** (`RepositoryWrapperValidator`) combining validation and persistence calls.

## Notable architectural decisions
- Layered modular design:
  - `App` (entry point)
  - `ServiceCollection` (composition root)
  - `MiniBank` (domain and use-case handlers)
  - `Repository` (persistence abstraction + mapping)
  - `InMemoryDataBase` (custom attribute-driven in-memory store)
  - `Abstractions` (shared contracts)
- Custom in-memory database enforces relational-like constraints using attributes and reflection.
- Optimistic-concurrency style version fields exist across domain and DB contracts.
- Static service-provider bridges are used in some places (service-locator style access), which couples parts of domain/validation code to container state.
