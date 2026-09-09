
using System;
using Heltevagten.Characters;
using Heltevagten.Exceptions;
using Heltevagten.Interfaces;
using Heltevagten.Models;
using Heltevagten.Services;
using Heltevagten.Strategies;

namespace Heltevagten
{
    public class Program
    {
        // The DispatchCenter manages heroes and incidents.
        private static DispatchCenter _dispatchCenter;

        // The program starts here.
        public static void Main(string[] args)
        {
            SetupSystem();

            bool running = true;

            while (running)
            {
                ShowMenu();

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowHeroes();
                        break;

                    case "2":
                        ShowIncidents();
                        break;

                    case "3":
                        ReportNewIncident();
                        break;

                    case "4":
                        DispatchHero();
                        break;

                    case "5":
                        ResolveIncident();
                        break;

                    case "6":
                        UseHeroAbility();
                        break;

                    case "7":
                        ChangeStrategy();
                        break;

                    case "8":
                        SearchHeroes();
                        break;

                    case "9":
                        running = false;
                        Console.WriteLine(
                            "Thank you for using Heltevagten!");
                        break;

                    default:
                        Console.WriteLine(
                            "Invalid choice. Please choose 1-9.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Press ENTER to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        // Creates the initial heroes and dispatch strategy.
        private static void SetupSystem()
        {
            IDispatchStrategy strategy =
                new FirstAvailableStrategy();

            _dispatchCenter =
                new DispatchCenter(strategy);

            FlyingHero flyingHero =
                new FlyingHero(
                    "Sky Hero",
                    100);

            StrongHero strongHero =
                new StrongHero(
                    "Titan",
                    120,
                    90);

            HealingHero healingHero =
                new HealingHero(
                    "Medic",
                    100);

            _dispatchCenter.RegisterHero(flyingHero);
            _dispatchCenter.RegisterHero(strongHero);
            _dispatchCenter.RegisterHero(healingHero);
        }

        // Displays the main menu.
        private static void ShowMenu()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("       HELTEVAGTEN SYSTEM");
            Console.WriteLine("=================================");
            Console.WriteLine();

            Console.WriteLine("1. Show heroes");
            Console.WriteLine("2. Show incidents");
            Console.WriteLine("3. Report new incident");
            Console.WriteLine("4. Dispatch hero");
            Console.WriteLine("5. Resolve incident");
            Console.WriteLine("6. Use hero ability");
            Console.WriteLine("7. Change dispatch strategy");
            Console.WriteLine("8. Search for available hero");
            Console.WriteLine("9. Exit");

            Console.WriteLine();
        }

        // Shows all registered heroes.
        private static void ShowHeroes()
        {
            Console.WriteLine("=== HEROES ===");

            if (_dispatchCenter.Heroes.Count == 0)
            {
                Console.WriteLine("No heroes registered.");
                return;
            }

            for (int i = 0;
                i < _dispatchCenter.Heroes.Count;
                i++)
            {
                Console.WriteLine(
                    (i + 1)
                    + ". "
                    + _dispatchCenter.Heroes[i]);
            }
        }

        // Shows all reported incidents.
        private static void ShowIncidents()
        {
            Console.WriteLine("=== INCIDENTS ===");

            if (_dispatchCenter.Incidents.Count == 0)
            {
                Console.WriteLine("No incidents reported.");
                return;
            }

            for (int i = 0;
                i < _dispatchCenter.Incidents.Count;
                i++)
            {
                Incident incident =
                    _dispatchCenter.Incidents[i];

                Console.WriteLine(
                    (i + 1)
                    + ". "
                    + incident);

                if (incident.AssignedHeroName != null)
                {
                    Console.WriteLine(
                        "   Assigned hero: "
                        + incident.AssignedHeroName);
                }
            }
        }

        // Allows the user to create a new incident.
        private static void ReportNewIncident()
        {
            Console.WriteLine("=== REPORT NEW INCIDENT ===");

            Console.Write("Enter incident Description: ");
            string description = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine(
                    "Description cannot be empty.");

                Console.Write("Description: ");
                description = Console.ReadLine();
            }

            Console.Write("Enter Location: ");
            string location = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(location))
            {
                Console.WriteLine(
                    "Location cannot be empty.");

                Console.Write("Location: ");
                location = Console.ReadLine();
            }

            Severity severity =
                ReadSeverity();

            HeroSpecialty specialty = 
                ReadHeroSpecialty();

            Incident incident =
                new Incident(
                    description,
                    location,
                    severity,
                    specialty);

            _dispatchCenter.ReportIncident(
                incident);

            Console.WriteLine();
            Console.WriteLine(
                "Incident created successfully!");
        }


        // Allows the user to select the type of hero
        // required for the incident.
        private static HeroSpecialty ReadHeroSpecialty()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose required hero type:");

                Console.WriteLine("1. General");
                Console.WriteLine("2. Rescue");
                Console.WriteLine("3. Strength");
                Console.WriteLine("4. Medical");

                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return HeroSpecialty.General;

                    case "2":
                        return HeroSpecialty.Rescue;

                    case "3":
                        return HeroSpecialty.Strength;

                    case "4":
                        return HeroSpecialty.Medical;

                    default:
                        Console.WriteLine(
                            "Invalid choice. Please choose 1-4.");
                        break;
                }
            }
        }
        // Lets the user choose Low, Medium or High.
        private static Severity ReadSeverity()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose severity:");
                Console.WriteLine("1. Low");
                Console.WriteLine("2. Medium");
                Console.WriteLine("3. High");

                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return Severity.Low;

                    case "2":
                        return Severity.Medium;

                    case "3":
                        return Severity.High;

                    default:
                        Console.WriteLine(
                            "Invalid choice.");
                        break;
                }
            }
        }

        // Allows the user to choose an incident and dispatch a hero.
        private static void DispatchHero()
        {
            Console.WriteLine("=== DISPATCH HERO ===");

            if (_dispatchCenter.Incidents.Count == 0)
            {
                Console.WriteLine(
                    "There are no incidents.");
                return;
            }

            ShowIncidents();

            Console.WriteLine();
            Console.Write("Choose incident number: ");

            int incidentNumber;

            if (!int.TryParse(
                Console.ReadLine(),
                out incidentNumber))
            {
                Console.WriteLine(
                    "Please enter a number.");
                return;
            }

            if (incidentNumber < 1 ||
                incidentNumber > _dispatchCenter.Incidents.Count)
            {
                Console.WriteLine(
                    "Invalid incident number.");
                return;
            }

            Incident incident =
                _dispatchCenter.Incidents[
                    incidentNumber - 1];

            if (incident.Status == IncidentStatus.Resolved)
            {
                Console.WriteLine(
                    "This incident is already resolved.");
                return;
            }

            if (incident.AssignedHeroName != null)
            {
                Console.WriteLine(
                    "This incident already has a hero.");
                return;
            }

            try
            {
                Hero hero =
                    _dispatchCenter.DispatchHero(
                        incident);

                Console.WriteLine();
                Console.WriteLine(
                    "Dispatch successful!");

                Console.WriteLine(
                    "Hero: " + hero.Name);

                Console.WriteLine(
                    "Incident: "
                    + incident.Description);
            }
            catch (HeroUnavailableException ex)
            {
                Console.WriteLine(
                    "Hero unavailable: "
                    + ex.Message);
            }
            catch (NoSuitableHeroFoundException ex)
            {
                Console.WriteLine(
                    "No suitable hero: "
                    + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(
                    "Operation failed: "
                    + ex.Message);
            }
        }

        
// Allows the user to resolve an incident.
private static void ResolveIncident()
        {
            Console.WriteLine("=== RESOLVE INCIDENT ===");

            // Check whether there are any incidents.
            if (_dispatchCenter.Incidents.Count == 0)
            {
                Console.WriteLine(
                    "There are no incidents to resolve.");

                return;
            }

            // Show all incidents so the user can choose one.
            ShowIncidents();

            Console.WriteLine();
            Console.Write("Choose incident number: ");

            int incidentNumber;

            // TryParse prevents the program from crashing
            // if the user enters text instead of a number.
            if (!int.TryParse(
                Console.ReadLine(),
                out incidentNumber))
            {
                Console.WriteLine(
                    "Please enter a valid number.");

                return;
            }

            // Make sure the selected number exists.
            if (incidentNumber < 1 ||
                incidentNumber > _dispatchCenter.Incidents.Count)
            {
                Console.WriteLine(
                    "Invalid incident number.");

                return;
            }

            // Get the selected incident from the list.
            Incident incident =
                _dispatchCenter.Incidents[
                    incidentNumber - 1];

            Console.WriteLine();

            // Check if the incident has already been resolved.
            if (incident.Status ==
                IncidentStatus.Resolved)
            {
                Console.WriteLine(
                    "This incident is already resolved.");

                return;
            }

            // The incident must have a hero before
            // it can be resolved.
            if (incident.Status ==
                IncidentStatus.Open)
            {
                Console.WriteLine(
                    "This incident has not been dispatched yet.");

                Console.WriteLine(
                    "A hero must be assigned before "
                    + "the incident can be resolved.");

                return;
            }

            // At this point the incident is InProgress.
            Console.WriteLine(
                "Incident: "
                + incident.Description);

            Console.WriteLine(
                "Assigned hero: "
                + incident.AssignedHeroName);

            Console.WriteLine();

            // Ask the user which callback to demonstrate.
            Console.WriteLine(
                "Choose callback type:");

            Console.WriteLine(
                "1. Named method");

            Console.WriteLine(
                "2. Lambda expression");

            Console.WriteLine(
                "0. Cancel");

            Console.WriteLine();

            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            try
            {
                if (choice == "1")
                {
                    // Passing a named method as an Action<Incident>.
                    _dispatchCenter.ResolveIncident(
                        incident,
                        OnIncidentResolved);
                }
                else if (choice == "2")
                {
                    // Passing a lambda expression as an Action<Incident>.
                    _dispatchCenter.ResolveIncident(
                        incident,
                        resolvedIncident =>
                        {
                            Console.WriteLine();
                            Console.WriteLine(
                                "Lambda callback:");

                            Console.WriteLine(
                                resolvedIncident.Description
                                + " is resolved.");
                        });
                }
                else if (choice == "3")
                {
                    _dispatchCenter.ChangeStrategy(
                        new BestMatchStrategy());

                    Console.WriteLine();
                    Console.WriteLine(
                        "Current strategy: Best Match");
                }
                else if (choice == "0")
                {
                    Console.WriteLine(
                        "Resolution cancelled.");
                }
                else
                {
                    Console.WriteLine(
                        "Invalid callback choice.");
                }
            }
            catch (InvalidOperationException ex)
            {
                // Handles invalid state transitions.
                Console.WriteLine();
                Console.WriteLine(
                    "Cannot resolve incident:");

                Console.WriteLine(
                    ex.Message);
            }
            catch (Exception ex)
            {
                // Handles unexpected errors.
                Console.WriteLine();
                Console.WriteLine(
                    "An unexpected error occurred:");

                Console.WriteLine(
                    ex.Message);
            }
        }



        // Named callback method.
        private static void OnIncidentResolved(
            Incident incident)
        {
            Console.WriteLine(
                "Named callback: "
                + incident.Description
                + " is resolved.");
        }

        // Allows the user to use a hero's special ability.
        private static void UseHeroAbility()
        {
            Console.WriteLine("=== HERO ABILITIES ===");

            ShowHeroes();

            if (_dispatchCenter.Heroes.Count == 0)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Choose hero number: ");

            int heroNumber;

            if (!int.TryParse(
                Console.ReadLine(),
                out heroNumber))
            {
                Console.WriteLine(
                    "Please enter a number.");
                return;
            }

            if (heroNumber < 1 ||
                heroNumber > _dispatchCenter.Heroes.Count)
            {
                Console.WriteLine(
                    "Invalid hero number.");
                return;
            }

            Hero hero =
                _dispatchCenter.Heroes[
                    heroNumber - 1];

            Console.WriteLine();
            Console.WriteLine(
                "Selected hero: "
                + hero.Name);

            Console.WriteLine();
            Console.WriteLine("Available abilities:");

            Console.WriteLine(
                "1. Signature move");

            if (hero is IFlyable)
            {
                Console.WriteLine(
                    "2. Fly");
            }

            if (hero is ISuperStrong)
            {
                Console.WriteLine(
                    "2. Lift heavy object");
            }

            Console.WriteLine(
                "0. Cancel");

            Console.WriteLine();
            Console.Write("Choose ability: ");

            string choice = Console.ReadLine();

            try
            {
                if (choice == "1")
                {
                    Console.WriteLine(
                        hero.UseSignatureMove());
                }
                else if (choice == "2")
                {
                    IFlyable flyingHero =
                      hero as IFlyable;

                    if (flyingHero != null)
                    {
                        flyingHero.Fly();
                        return;
                    }

                    ISuperStrong strongHero =
                        hero as ISuperStrong;

                    if (strongHero != null)
                    {
                        strongHero.LiftHeavyObject();
                        return;
                    }

                    Console.WriteLine(
                        "This hero does not have that ability.");
                }
                else if (choice == "0")
                {
                    Console.WriteLine(
                        "Ability cancelled.");
                }
                else
                {
                    Console.WriteLine(
                        "Invalid ability.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(
                    "Cannot use ability: "
                    + ex.Message);
            }
        }

        // Changes between the two dispatch strategies.
        
private static void ChangeStrategy()
        {
            Console.WriteLine(
                "=== CHANGE DISPATCH STRATEGY ===");

            Console.WriteLine(
                "1. First available hero");

            Console.WriteLine(
                "2. Strongest available hero");

            Console.WriteLine();

            Console.Write("Choose strategy: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                _dispatchCenter.ChangeStrategy(
                    new FirstAvailableStrategy());

                Console.WriteLine();
                Console.WriteLine(
                    "Current strategy: First Available");
            }
            else if (choice == "2")
            {
                _dispatchCenter.ChangeStrategy(
                    new StrongestHeroStrategy());

                Console.WriteLine();
                Console.WriteLine(
                    "Current strategy: Strongest Hero");
            }
            else
            {
                Console.WriteLine(
                    "Invalid strategy.");
            }
        }

        // Creates a new DispatchCenter while keeping
        // the registered heroes and incidents.
        private static DispatchCenter CreateCenterWithStrategy(
            IDispatchStrategy strategy)
        {
            DispatchCenter newCenter =
                new DispatchCenter(strategy);

            foreach (Hero hero in _dispatchCenter.Heroes)
            {
                newCenter.RegisterHero(hero);
            }

            foreach (Incident incident
                in _dispatchCenter.Incidents)
            {
                newCenter.ReportIncident(incident);
            }

            return newCenter;
        }

        // Demonstrates the generic FindFirst<T>() method.
        private static void SearchHeroes()
        {
            Console.WriteLine(
                "=== GENERIC SEARCH ===");

            Hero availableHero =
                SearchHelper.FindFirst(
                    _dispatchCenter.Heroes,
                    hero => hero.IsAvailable);

            if (availableHero == null)
            {
                Console.WriteLine(
                    "No available hero found.");
            }
            else
            {
                Console.WriteLine(
                    "First available hero: "
                    + availableHero.Name);
            }
        }
    }
}
