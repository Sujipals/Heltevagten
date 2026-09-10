# Heltevagten

## Project Description

Heltevagten is a C# console application that simulates an emergency hero dispatch system.

The system allows users to report incidents and dispatch suitable heroes to help solve them. Different heroes have different specialties and abilities.

The project demonstrates important object-oriented programming concepts in C#, including:

* Abstract classes
* Inheritance
* Polymorphism
* Interfaces
* Encapsulation
* Collections
* Enums
* Exception handling
* Custom exceptions
* Generics
* Delegates and callbacks
* Strategy Pattern
* LINQ
* Separation of responsibilities

## How the System Works

The user interacts with the system through an interactive console menu.

The main menu provides these options:

1. Show heroes
2. Show incidents
3. Report new incident
4. Dispatch hero
5. Resolve incident
6. Use hero ability
7. Change dispatch strategy
8. Search for available hero
9. Exit

## Heroes

All heroes inherit from the abstract `Hero` class.

### FlyingHero

`FlyingHero` represents a hero who can fly and is specialized in rescue situations.

It implements the `IFlyable` interface.

### StrongHero

`StrongHero` represents a physically strong hero who can lift heavy objects.

It implements the `ISuperStrong` interface.

### HealingHero

`HealingHero` represents a medical hero who can help injured people.

It provides a healing ability.

## Hero Specialties

Each hero has a specialty.

The `HeroSpecialty` enum contains:

* General
* Rescue
* Strength
* Medical

When an incident is reported, the user selects the required specialty.

This allows the system to find a hero who is suitable for the incident.

For example:

* Rescue incident → FlyingHero
* Strength incident → StrongHero
* Medical incident → HealingHero

## Incidents

An incident contains information about:

* Description
* Location
* Severity
* Required hero specialty
* Status
* Assigned hero

The incident status can be:

```text
Open
InProgress
Resolved
```

The normal incident lifecycle is:

```text
Open
  ↓
InProgress
  ↓
Resolved
```

An incident cannot be dispatched if it is already resolved or already has a hero.

## Dispatch Strategies

The system uses the `IDispatchStrategy` interface and the Strategy Pattern.

There are three dispatch strategies.

### FirstAvailableStrategy

Selects the first available suitable hero.

### StrongestHeroStrategy

Selects the strongest available hero based on energy.

### BestMatchStrategy

Selects an available hero whose specialty matches the incident's required specialty.

The strategy can be changed while the application is running.

This demonstrates loose coupling because `DispatchCenter` works with the `IDispatchStrategy` interface instead of depending directly on one specific strategy.

## Exceptions

The project contains custom exceptions.

### HeroUnavailableException

Used when an unavailable hero is attempted to be dispatched.

### NoSuitableHeroFoundException

Used when no suitable hero can be found for an incident.

The system also uses standard exceptions such as:

* `ArgumentException`
* `ArgumentNullException`
* `InvalidOperationException`

These help prevent invalid operations and invalid data.

## Generic Method

The project contains a generic method:

```csharp
FindFirst<T>()
```

It is located in `SearchHelper`.

The method searches through a collection and returns the first item that satisfies a condition.

It uses:

```csharp
Func<T, bool>
```

This demonstrates generics and delegates.

## Delegates and Callbacks

The `ResolveIncident()` method in `DispatchCenter` accepts an `Action<Incident>` callback.

The callback can be supplied as either:

* A named method
* A lambda expression

This allows another piece of code to decide what should happen after an incident is resolved.

## Hero Energy

Heroes have an energy system.

Using an ability consumes energy.

When an incident is resolved, the assigned hero becomes available again and receives some energy back.

This makes the dispatch system more interactive.

## Project Structure

```text
Heltevagten
│
├── Characters
│   ├── Hero.cs
│   ├── FlyingHero.cs
│   ├── StrongHero.cs
│   └── HealingHero.cs
│
├── Exceptions
│   ├── HeroUnavailableException.cs
│   └── NoSuitableHeroFoundException.cs
│
├── Interfaces
│   ├── IFlyable.cs
│   ├── ISuperStrong.cs
│   └── IDispatchStrategy.cs
│
├── Models
│   ├── Incident.cs
│   ├── Severity.cs
│   ├── IncidentStatus.cs
│   └── HeroSpecialty.cs
│
├── Services
│   ├── DispatchCenter.cs
│   └── SearchHelper.cs
│
├── Strategies
│   ├── FirstAvailableStrategy.cs
│   ├── StrongestHeroStrategy.cs
│   └── BestMatchStrategy.cs
│
├── Program.cs
├── Heltevagten.csproj
├── README.md
└── Reflection.md
```

## How to Run

1. Open the project in Visual Studio.
2. Build the solution.
3. Make sure there are no build errors.
4. Run the console application.
5. Use the menu to interact with the system.

You can also run the project from the terminal:

```bash
dotnet build
```

Then:

```bash
dotnet run
```

## Technologies

The project was developed using:

* C#
* .NET
* Visual Studio
* Git
* GitHub

## UML

A UML class diagram was created to represent the main classes, interfaces, inheritance relationships, associations, dependencies, and the Strategy Pattern.

Important relationships include:

* `FlyingHero` inherits from `Hero`
* `StrongHero` inherits from `Hero`
* `HealingHero` inherits from `Hero`
* `FlyingHero` implements `IFlyable`
* `StrongHero` implements `ISuperStrong`
* Dispatch strategies implement `IDispatchStrategy`
* `DispatchCenter` works with `Hero`
* `DispatchCenter` works with `Incident`
* `DispatchCenter` uses `IDispatchStrategy`

## Reflection

A separate `Reflection.md` file contains reflections about the development process, programming concepts, challenges, and what was learned during the project.

## Learning Goals

The main purpose of the project is to practise object-oriented programming and combine several C# concepts in one application.

The project also focuses on writing understandable, maintainable and loosely coupled code.

## Author

Sujitha Palanivel

C# / .NET programming project
