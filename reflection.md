# Reflection

## Introduction

The Heltevagten project has helped me practise several important C# and object-oriented programming concepts.

The goal was not only to make the program work, but also to understand how the different classes, interfaces and methods work together.

---

## What I Learned

### Abstract Classes

I used the abstract `Hero` class as the common base class for all heroes.

The class contains common properties and methods such as:

* Name
* Energy
* Specialty
* Availability
* `UseEnergy()`
* `RestoreEnergy()`

The method `UseSignatureMove()` is abstract because each type of hero should implement its own version.

This helped me understand why an abstract class is useful when several classes share common functionality.

---

## Inheritance

`FlyingHero`, `StrongHero` and `HealingHero` inherit from `Hero`.

For example:


public class FlyingHero : Hero


This means the derived class automatically gets the common functionality from `Hero`.

The derived classes can then add their own abilities.

---

## Polymorphism

Polymorphism is used through the `Hero` type.

The program can work with a collection of:


List<Hero>


The collection can contain different hero types.

For example:

FlyingHero
StrongHero
HealingHero


Even though they are different classes, they can all be handled as `Hero`.

The overridden `UseSignatureMove()` method also demonstrates polymorphism because each hero provides its own implementation.

---

## Interfaces

I created interfaces such as:

IFlyable
ISuperStrong
IDispatchStrategy


`IFlyable` is used by `FlyingHero`.

`ISuperStrong` is used by `StrongHero`.

`IDispatchStrategy` is used by the different dispatch strategies.

I learned that an interface describes what a class can do without defining the complete implementation.

---

## Encapsulation

The project uses properties with private setters.

For example:


public string Name { get; private set; }


This means other classes can read the name but cannot directly change it.

Energy is also controlled through methods such as:


UseEnergy()
RestoreEnergy()


This prevents other parts of the program from changing the energy incorrectly.

---

## Collections

The `DispatchCenter` stores heroes and incidents in collections.

It uses:


List<Hero>
List<Incident>


This makes it possible to register multiple heroes and report multiple incidents.

I also learned how `IEnumerable<Hero>` can be used when a method only needs to search through a collection.

---

## Exception Handling

The project uses exceptions to handle invalid situations.

For example:

* A hero may not have enough energy.
* An incident may already be resolved.
* An incident may already have a hero.
* No suitable hero may be available.

I created custom exceptions:


HeroUnavailableException
NoSuitableHeroFoundException


This helped me understand when standard exceptions are enough and when a custom exception can make an error more meaningful.

---

## Generic Methods

I created the generic method:

FindFirst<T>()


The method can work with different types instead of being written for only one specific type.

It also accepts:


Func<T, bool>


which allows the caller to provide the search condition.

This helped me understand how generics and delegates can work together.

---

## Delegates and Callbacks

The `ResolveIncident()` method uses:


Action<Incident>


This is a delegate.

The program can provide a named method or a lambda expression as the callback.

This helped me understand how one method can receive another method as an argument.

---

## Strategy Pattern

One of the most useful concepts in this project was the Strategy pattern.

I created:


IDispatchStrategy


and three implementations:


FirstAvailableStrategy
StrongestHeroStrategy
BestMatchStrategy


The `DispatchCenter` does not need to know the exact strategy.

It only works with:


IDispatchStrategy


This makes the system more loosely coupled.

It also makes it easier to add another strategy later without changing the main dispatch system.

---

## Hero and Incident Matching

I added a `HeroSpecialty` property to heroes and incidents.

For example:


FlyingHero → Rescue
StrongHero → Strength
HealingHero → Medical


The `BestMatchStrategy` compares:


h.Specialty == incident.RequiredSpecialty


This means the system can select a hero based on what the incident actually requires instead of simply selecting the first available hero.

This made the program more realistic and helped me understand how objects can work together.

---

## Challenges

One challenge was understanding how all the classes should communicate with each other.

At first, it was difficult to understand the relationship between:


Hero
Incident
DispatchCenter
IDispatchStrategy


I learned that each class should have a clear responsibility.

Another challenge was dealing with errors caused by different C# language versions. Some newer C# syntax was not supported by the project's C# version, so I had to use simpler syntax.

I also had to fix errors when the `Incident` class was changed to use `IncidentStatus` instead of an older `IsResolved` property.

---

## What I Would Improve

If I continued developing the project, I could add more hero types and specialties.

For example:


TechHero → Technology


This could be used for computer or cyber-related incidents.

I could also add:

* Saving incidents to a file
* Loading previous incidents
* More hero abilities
* More dispatch strategies
* Statistics about resolved incidents
* More detailed incident history

However, I would first make sure that the existing functionality remains simple and reliable.

---

## Conclusion

The Heltevagten project has given me practical experience with C# and object-oriented programming.

The most important things I learned were:

* How inheritance works
* How abstract classes are used
* How polymorphism allows different objects to be treated as the same base type
* How interfaces provide common contracts
* How exceptions handle invalid situations
* How generic methods can be reused
* How delegates and callbacks work
* How the Strategy pattern can create loose coupling

The project also taught me that designing the classes and their relationships before writing all the code makes the implementation easier to understand.

Overall, I now have a better understanding of how several C# concepts can be combined to build a complete application.
