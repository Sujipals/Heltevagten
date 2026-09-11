
using System;
using Heltevagten.Characters;
using Heltevagten.Exceptions;
using Heltevagten.Interfaces;
using Heltevagten.Models;
using Heltevagten.Services;
using Heltevagten.Strategies;

namespace Heltevagten
{
    /// <summary> 
    /// Main entry point of the Heltevagten console application.
    /// The DispatchCenter manages heroes and incidents.
    /// </summary>
    public class Program
    {

       
        // Stores the DispatchCenter used by the whole program.
        // It manages heroes, incidents and the dispatch strategy.
        private static DispatchCenter _dispatchCenter;

        // The program starts here.
        public static void Main(string[] args)
        {
            // Create the initial heroes and configure 
            // the first dispatch strategy.
            SetupSystem();

            bool running = true;// Controls whether the main menu should continue running.

            // Continue showing the menu while the user has not selected option 9.
            while (running)
            {
                ShowMenu(); // Display the main menu.

                Console.Write("Choose an option: ");// Ask the user to choose an option.
                string choice = Console.ReadLine();// Read the user's input as a string.

                Console.WriteLine();

                switch (choice)// Decide what action to perform based on the user's menu choice.
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
                        running = false; // Set running to false so the while loop stops.
                        Console.WriteLine( "Thank you for using Heltevagten!");
                        break;

                    default: // Runs when the user enters something that is not one of the valid choices.
                        Console.WriteLine("Invalid choice. Please choose 1-9.");
                        break;
                }

                if (running) // Only pause the program if the user has not selected Exit.
                {
                    Console.WriteLine();
                    Console.WriteLine( "Press ENTER to continue...");
                    Console.ReadLine();
                    Console.Clear(); // Clear the console before showing the menu again.
                }
            }
        }

        // ---------------------------------------------------------
        // SETUP
        // --------------------------------------------------------- 
        
        /// <summary>
        /// Creates the initial heroes and dispatch strategy. 
        /// </summary>
        
        private static void SetupSystem()
        {

            // Create the first dispatch strategy. 
            // The program initially uses FirstAvailableStrategy.
            // 
            // IDispatchStrategy is the interface type. 
            // This demonstrates loose coupling.
            IDispatchStrategy strategy = new FirstAvailableStrategy();

            // Create the DispatchCenter and give it the strategy.
            _dispatchCenter = new DispatchCenter(strategy);


            // Create a FlyingHero.
            FlyingHero flyingHero =  new FlyingHero( "Sky Hero", 100);


            // Create a StrongHero.
            StrongHero strongHero = new StrongHero( "Titan", 120, 90);


            // Create a HealingHero.
            HealingHero healingHero = new HealingHero( "Medic", 100);

            // Register the heroes in the DispatchCenter.
            _dispatchCenter.RegisterHero(flyingHero);
            _dispatchCenter.RegisterHero(strongHero);
            _dispatchCenter.RegisterHero(healingHero);
        }

        // --------------------------------------------------------- 
        // MENU 
        // ---------------------------------------------------------

        /// <summary>
        /// Displays the main menu.
        /// </summary>
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

        // --------------------------------------------------------- 
        // HEROES 
        // ---------------------------------------------------------


        /// <summary>
        /// Shows all registered heroes.
        /// /// </summary>
        private static void ShowHeroes()
        {
            Console.WriteLine("=== HEROES ===");


            // Check whether there are any heroes.
            if (_dispatchCenter.Heroes.Count == 0)
            {
                Console.WriteLine("No heroes registered.");
                return;
            }

            // Loop through every hero in the collection.
            for (int i = 0;  i < _dispatchCenter.Heroes.Count;  i++)
            {

                // Display the hero number and the hero information. 
                // 
                // ToString() from Hero is automatically called here.
                Console.WriteLine( (i + 1) + ". "  + _dispatchCenter.Heroes[i]);
            }
        }

        // ---------------------------------------------------------
        // INCIDENTS
        // ---------------------------------------------------------

        /// <summary>
        /// Shows all reported incidents.
        /// </summary>
        private static void ShowIncidents()
        {
            Console.WriteLine("=== INCIDENTS ===");
            // Check whether there are any incidents.
            if (_dispatchCenter.Incidents.Count == 0)
            {
                Console.WriteLine("No incidents reported.");
                return;
            }
            // Loop through all incidents.
            for (int i = 0; i < _dispatchCenter.Incidents.Count;  i++)
            {
                // Get the current incident from the list.
                Incident incident = _dispatchCenter.Incidents[i];

                Console.WriteLine((i + 1) + ". " + incident); // Display the incident.
                // Check whether a hero has been assigned.
                if (incident.AssignedHeroName != null)
                {
                    Console.WriteLine( "   Assigned hero: " + incident.AssignedHeroName);
                }
            }
        }

        // --------------------------------------------------------- 
        // REPORT INCIDENT 
        // ---------------------------------------------------------


        /// <summary>
        /// Allows the user to create a new incident.
        /// </summary>
        
        private static void ReportNewIncident()
        {
            Console.WriteLine("=== REPORT NEW INCIDENT ===");

            Console.Write("Enter incident Description: "); // Ask the user for a description.
            string description = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(description)) // Keep asking until the user enters valid text.
            {
                Console.WriteLine( "Description cannot be empty.");

                Console.Write("Description: ");
                description = Console.ReadLine();
            }

            Console.Write("Enter Location: "); // Ask for the incident location.
            string location = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(location)) // Make sure the location is not empty.
            {
                Console.WriteLine( "Location cannot be empty.");

                Console.Write("Location: ");
                location = Console.ReadLine();
            }

            Severity severity = ReadSeverity(); // Ask the user to choose the severity.

            HeroSpecialty specialty = ReadHeroSpecialty(); // Ask which type of hero is required.

            Incident incident = new Incident(
                    description,
                    location,
                    severity,
                    specialty); // Create a new Incident object.

            _dispatchCenter.ReportIncident(incident);// Send the new incident to DispatchCenter.

            Console.WriteLine();
            Console.WriteLine("Incident created successfully!");
        }

        // --------------------------------------------------------- 
        // HERO SPECIALTY 
        // ---------------------------------------------------------

        /// <summary>
        // Allows the user to select the type of hero required for the incident.
        /// </summary>
        private static HeroSpecialty ReadHeroSpecialty()
        {
            while (true) // Keep asking until the user enters a valid choice.
            {
                Console.WriteLine();
                Console.WriteLine("Choose required hero type:");

                Console.WriteLine("1. General");
                Console.WriteLine("2. Rescue");
                Console.WriteLine("3. Strength");
                Console.WriteLine("4. Medical");

                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                switch (choice) // Convert the user's choice into a HeroSpecialty enum.
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
                        Console.WriteLine( "Invalid choice. Please choose 1-4.");
                        break;
                }
            }
        }

        // --------------------------------------------------------- 
        // SEVERITY
        // ---------------------------------------------------------

        /// <summary>
        /// Lets the user choose Low, Medium or High.
        /// </summary>
        private static Severity ReadSeverity()
        {
            while (true) // Continue until a valid option is selected.
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
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // --------------------------------------------------------- 
        // DISPATCH HERO
        // --------------------------------------------------------- 
        
        /// <summary>
        /// Allows the user to select an incident and dispatch a hero.
        /// </summary>
        private static void DispatchHero()
        {
            Console.WriteLine("=== DISPATCH HERO ===");
            // There is nothing to dispatch if there are no incidents.
            if (_dispatchCenter.Incidents.Count == 0)
            {
                Console.WriteLine("There are no incidents.");
                return;
            }

            ShowIncidents(); // Show the incidents so the user can select one.

            Console.WriteLine();
            Console.Write("Choose incident number: ");

            int incidentNumber;


            // TryParse safely converts the input to an integer.
            // It returns false instead of crashing if the user enters invalid text.
            if (!int.TryParse(Console.ReadLine(), out incidentNumber))
            {
                Console.WriteLine("Please enter a number.");
                return;
            }

            // Check that the selected incident number exists.
            if (incidentNumber < 1 || incidentNumber > _dispatchCenter.Incidents.Count)
            {
                Console.WriteLine("Invalid incident number.");
                return;
            }

            // Get the selected incident. 
            // 
            // -1 is needed because users count from 1, while list indexes start at 0.
            Incident incident = _dispatchCenter.Incidents[ incidentNumber - 1];

            // A resolved incident cannot be dispatched again.
            if (incident.Status == IncidentStatus.Resolved)
            {
                Console.WriteLine("This incident is already resolved.");
                return;
            }

            // Do not assign a second hero to the same incident.
            if (incident.AssignedHeroName != null)
            {
                Console.WriteLine("This incident already has a hero.");
                return;
            }

            try
            {
                Hero hero =// Ask DispatchCenter to select and dispatch a hero.
                    _dispatchCenter.DispatchHero( incident);

                Console.WriteLine();
                Console.WriteLine("Dispatch successful!");

                Console.WriteLine("Hero: " + hero.Name);

                Console.WriteLine("Incident: "+ incident.Description);
            }
            catch (HeroUnavailableException ex)
            {
                Console.WriteLine("Hero unavailable: "+ ex.Message);
            }
            catch (NoSuitableHeroFoundException ex)
            {
                Console.WriteLine("No suitable hero: "+ ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Operation failed: "+ ex.Message);
            }
        }

        // --------------------------------------------------------- 
        // RESOLVE INCIDENT 
        // --------------------------------------------------------- 

        /// <summary> 
        /// Allows the user to resolve an incident. 
        /// </summary>
        private static void ResolveIncident()
        {
            Console.WriteLine("=== RESOLVE INCIDENT ===");

            // Check whether there are any incidents.
            if (_dispatchCenter.Incidents.Count == 0)
            {
                Console.WriteLine("There are no incidents to resolve.");

                return;
            }

            // Show all incidents so the user can choose one.
            ShowIncidents();

            Console.WriteLine();
            Console.Write("Choose incident number: ");

            int incidentNumber;

            // TryParse prevents the program from crashing
            // if the user enters text instead of a number.
            if (!int.TryParse(Console.ReadLine(),out incidentNumber)) // Safely convert user input into an integer.
            {
                Console.WriteLine("Please enter a valid number.");

                return;
            }

            // Make sure the selected incident number exists.
            if (incidentNumber < 1 ||  incidentNumber > _dispatchCenter.Incidents.Count)
            {
                Console.WriteLine("Invalid incident number.");

                return;
            }

            // Get the selected incident from the list.
            Incident incident =_dispatchCenter.Incidents[ incidentNumber - 1];

            Console.WriteLine();

            // Check if the incident has already been resolved.
            if (incident.Status == IncidentStatus.Resolved)
            {
                Console.WriteLine("This incident is already resolved.");

                return;
            }

            // The incident must have a hero before, it can be resolved.
            if (incident.Status == IncidentStatus.Open)
            {
                Console.WriteLine("This incident has not been dispatched yet.");

                Console.WriteLine("A hero must be assigned before "  + "the incident can be resolved.");

                return;
            }

            // At this point the incident is InProgress.
            Console.WriteLine("Incident: "+ incident.Description);

            Console.WriteLine("Assigned hero: " + incident.AssignedHeroName);

            Console.WriteLine();

            // Ask the user which callback to demonstrate.
            Console.WriteLine("Choose callback type:");

            Console.WriteLine("1. Named method");

            Console.WriteLine("2. Lambda expression");

            Console.WriteLine("0. Cancel");

            Console.WriteLine();

            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            try
            {
                if (choice == "1")
                {
                    // Passing a named method as an Action<Incident>.
                    _dispatchCenter.ResolveIncident(incident, OnIncidentResolved);
                }
                else if (choice == "2")
                {
                    // Passing a lambda expression as an Action<Incident>.
                    _dispatchCenter.ResolveIncident(incident,  resolvedIncident =>
                        {
                            Console.WriteLine();
                            Console.WriteLine( "Lambda callback:");

                            Console.WriteLine( resolvedIncident.Description + " is resolved.");
                        });
                }
                else if (choice == "3")
                {
                    _dispatchCenter.ChangeStrategy( new BestMatchStrategy());

                    Console.WriteLine();
                    Console.WriteLine("Current strategy: Best Match");
                }
                else if (choice == "0")
                {
                    Console.WriteLine( "Resolution cancelled.");
                }
                else
                {
                    Console.WriteLine( "Invalid callback choice.");
                }
            }
            catch (InvalidOperationException ex)
            {
                // Handles invalid state transitions.
                Console.WriteLine();
                Console.WriteLine("Cannot resolve incident:");

                Console.WriteLine( ex.Message);
            }
            catch (Exception ex)
            {
                // Handles unexpected errors.
                Console.WriteLine();
                Console.WriteLine("An unexpected error occurred:");

                Console.WriteLine(ex.Message);
            }
        }


        // --------------------------------------------------------- 
        // NAMED CALLBACK
        // ---------------------------------------------------------
        /// <summary> 
        /// Named callback method that is executed
        /// when an incident is resolved.
        /// </summary>
        private static void OnIncidentResolved(Incident incident)
        {
            Console.WriteLine("Named callback: " + incident.Description + " is resolved.");
        }


        // --------------------------------------------------------- 
        // HERO ABILITIES 
        // ---------------------------------------------------------

        /// <summary>
        /// Allows the user to use a hero's special ability.
        /// </summary>
        private static void UseHeroAbility()
        {
            Console.WriteLine("=== HERO ABILITIES ===");

            ShowHeroes(); // Display all heroes.

            if (_dispatchCenter.Heroes.Count == 0) // Stop if there are no heroes.
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Choose hero number: ");

            int heroNumber;

            // Safely convert the input into an integer.
            if (!int.TryParse(Console.ReadLine(),out heroNumber))
            {
                Console.WriteLine("Please enter a number.");
                return;
            }
            // Check that the hero number is valid.
            if (heroNumber < 1 || heroNumber > _dispatchCenter.Heroes.Count)
            {
                Console.WriteLine("Invalid hero number.");
                return;
            }
            // Get the selected hero.
            Hero hero = _dispatchCenter.Heroes[ heroNumber - 1];

            Console.WriteLine();
            Console.WriteLine( "Selected hero: " + hero.Name);

            Console.WriteLine();
            Console.WriteLine("Available abilities:");

            Console.WriteLine("1. Signature move"); // Every hero has a signature move.

            if (hero is IFlyable)
            {
                Console.WriteLine("2. Fly"); // Check whether the hero implements IFlyable.
            }

            if (hero is ISuperStrong) // Check whether the hero implements ISuperStrong.
            {
                Console.WriteLine("2. Lift heavy object");
            }

            Console.WriteLine("0. Cancel");

            Console.WriteLine();
            Console.Write("Choose ability: ");

            string choice = Console.ReadLine();

            try
            {
                if (choice == "1") // Use the common signature move.
                {
                    Console.WriteLine(hero.UseSignatureMove());
                }
                else if (choice == "2") // Try to treat the hero as an IFlyable object.
                {
                    IFlyable flyingHero = hero as IFlyable;

                    if (flyingHero != null) // If the conversion worked, the hero can fly.
                    {
                        flyingHero.Fly();
                        return;
                    }

                    // Otherwise try to treat the hero as ISuperStrong.
                    ISuperStrong strongHero = hero as ISuperStrong;

                    if (strongHero != null) // If the conversion worked, use the strength ability.
                    {
                        strongHero.LiftHeavyObject();
                        return;
                    }
                    // The hero does not support either ability.
                    Console.WriteLine(  "This hero does not have that ability.");
                }
                else if (choice == "0")
                {
                    Console.WriteLine( "Ability cancelled.");
                }
                else
                {
                    Console.WriteLine( "Invalid ability.");
                }
            }
            catch (InvalidOperationException ex)// Handle errors such as not having enough energy.
            {
                Console.WriteLine( "Cannot use ability: " + ex.Message);
            }
        }

        // --------------------------------------------------------- 
        // CHANGE STRATEGY 
        // ---------------------------------------------------------
        /// <summary> 
        /// Allows the user to change the dispatch strategy.
        /// </summary>

        private static void ChangeStrategy()
        {
            Console.WriteLine( "=== CHANGE DISPATCH STRATEGY ===");

            Console.WriteLine( "1. First available hero");

            Console.WriteLine( "2. Strongest available hero");

            Console.WriteLine();

            Console.Write("Choose strategy: ");
            string choice = Console.ReadLine();

            if (choice == "1") // Select FirstAvailableStrategy.
            {
                _dispatchCenter.ChangeStrategy( new FirstAvailableStrategy());

                Console.WriteLine();
                Console.WriteLine( "Current strategy: First Available");
            }
            else if (choice == "2") // Select StrongestHeroStrategy.
            {
                _dispatchCenter.ChangeStrategy( new StrongestHeroStrategy());

                Console.WriteLine();
                Console.WriteLine( "Current strategy: Strongest Hero");
            }
            else
            {
                Console.WriteLine( "Invalid strategy.");
            }
        }

        // --------------------------------------------------------
        // CREATE CENTER WITH STRATEGY
        // --------------------------------------------------------- 
        /// <summary> 
        /// Creates a new DispatchCenter with another strategy 
        /// while keeping the existing heroes and incidents. 
        /// </summary>
        private static DispatchCenter CreateCenterWithStrategy( IDispatchStrategy strategy)
        {
            // Create a new DispatchCenter using the given strategy.
            DispatchCenter newCenter = new DispatchCenter(strategy);

            foreach (Hero hero in _dispatchCenter.Heroes) // Copy all existing heroes to the new center.
            {
                newCenter.RegisterHero(hero);
            }

            foreach (Incident incident in _dispatchCenter.Incidents) // Copy all existing incidents to the new center.
            {
                newCenter.ReportIncident(incident);
            }

            return newCenter; // Return the new DispatchCenter.
        }

        // --------------------------------------------------------- 
        // GENERIC SEARCH 
        // ---------------------------------------------------------
        /// <summary> 
        /// Demonstrates the generic FindFirst<T>() method. 
        /// </summary>
        private static void SearchHeroes()
        {
            Console.WriteLine( "=== GENERIC SEARCH ===");


            // SearchHelper.FindFirst is generic.
            // Here T becomes Hero because the collection
            // contains Hero objects.
            // 
            // The lambda checks whether each hero is available.
            Hero availableHero = SearchHelper.FindFirst(_dispatchCenter.Heroes,
                    hero => hero.IsAvailable);

            if (availableHero == null) // If no hero was found, FindFirst returns null.
            {
                Console.WriteLine( "No available hero found.");
            }
            else // Display the first available hero.
            {
                Console.WriteLine( "First available hero: "  + availableHero.Name);
            }
        }
    }
}
