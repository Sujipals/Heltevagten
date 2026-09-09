# Heltevagten
# Heltevagten

## Project Description

Heltevagten is a C# console application that simulates an emergency hero dispatch system.

The system allows users to report incidents and dispatch suitable heroes to help solve them. Different heroes have different specialties and abilities.

The project demonstrates important object-oriented programming concepts in C#, including:

* Abstract classes
* Inheritance
* Polymorphism
* Interfaces
* Collections
* Exception handling
* Custom exceptions
* Generics
* Delegates and callbacks
* Strategy pattern
* Encapsulation
* LINQ
* User interaction through a console menu

---

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

---

## Heroes

All heroes inherit from the abstract `Hero` class.

The current heroes are:

### FlyingHero

The `FlyingHero` represents a hero who can fly and is specialized in rescue situations.

It implements the `IFlyable` interface.

### StrongHero

The `StrongHero` represents a physically strong hero who can lift heavy objects.

It implements the `ISuperStrong` interface.

### HealingHero

The `HealingHero` represents a medical hero who can help injured people.

---

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

---

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

---

## Dispatch Strategies

The system uses the `IDispatchStrategy` interface.

There are three dispatch strategies.

### FirstAvailableStrategy

Selects the first available hero.

### StrongestHeroStrategy

Selects the available hero with the highest amount of energy.

### BestMatchStrategy

Selects the first available hero whose specialty matches the incident's required specialty.

The strategy can be changed while the application is running.

This demonstrates loose coupling because `DispatchCenter` works with the `IDispatchStrategy` interface instead of depending directly on one specific strategy.

---

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

---

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

This demonstrates both generics and delegates.

---

## Delegates and Callbacks

The `ResolveIncident()` method in `DispatchCenter` accepts an `Action<Incident>` callback.

The callback can be supplied as either:

* A named method
* A lambda expression

This allows another piece of code to decide what should happen after an incident is resolved.

---

## Hero Energy

Heroes have an energy system.

Using an ability consumes energy.

For example, a hero's signature move may consume energy.

When an incident is resolved, the assigned hero becomes available again and receives some energy back.

This makes the dispatch system more interactive.

---

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
├── README.md
└── Reflection.md
```

---

## How to Run

1. Open the project in Visual Studio.
2. Build the solution.
3. Make sure there are no build errors.
4. Run the console application.
5. Use the menu to interact with the system.

A typical test can be:

```text
1. Show heroes
2. Report an incident
3. Choose severity
4. Choose a required specialty
5. Change strategy to Best Match
6. Dispatch a hero
7. Resolve the incident
```

---

## Technologies

The project was developed using:

* C#
* .NET
* Visual Studio
* Git
* GitHub

---

## Learning Goals

The main purpose of the project is to practise object-oriented programming and combine several C# concepts in one application.

The project also focuses on writing understandable, maintainable and loosely coupled code.
