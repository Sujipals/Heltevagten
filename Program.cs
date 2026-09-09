using Heltevagten.Characters;
using Heltevagten.Exceptions;
using Heltevagten.Interfaces;
using Heltevagten.Models;
using Heltevagten.Services;
using Heltevagten.Strategies;
using System;

namespace Heltevagten
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("       HELTEVAGTEN SYSTEM");
            Console.WriteLine("=================================");
            Console.WriteLine();

            // Create the first dispatch strategy.
            IDispatchStrategy firstStrategy =
                new FirstAvailableStrategy();

            // Inject the strategy into DispatchCenter.
            DispatchCenter dispatchCenter =
                new DispatchCenter(firstStrategy);

            // Create heroes.
            FlyingHero skyHero =
                new FlyingHero("Sky Hero", 100);

            StrongHero strongHero =
                new StrongHero("Titan", 120, 90);

            HealingHero healerHero =
                new HealingHero("Medic", 100);

            // Register heroes.
            dispatchCenter.RegisterHero(skyHero);
            dispatchCenter.RegisterHero(strongHero);
            dispatchCenter.RegisterHero(healerHero);

            Console.WriteLine();

            // Demonstrate polymorphism.
            Console.WriteLine("=== POLYMORPHISM ===");

            Console.WriteLine(
                skyHero.UseSignatureMove());

            Console.WriteLine(
                strongHero.UseSignatureMove());

            Console.WriteLine(
                healerHero.UseSignatureMove());

            // Restore energy.
            skyHero.RestoreEnergy(100);
            strongHero.RestoreEnergy(100);
            healerHero.RestoreEnergy(100);

            Console.WriteLine();

            // Demonstrate interfaces.
            Console.WriteLine("=== INTERFACES ===");

            skyHero.Fly();
            strongHero.LiftHeavyObject();
            healerHero.Heal();

            // Restore energy.
            skyHero.RestoreEnergy(100);
            strongHero.RestoreEnergy(100);
            healerHero.RestoreEnergy(100);

            Console.WriteLine();

            // Create incidents.
            Incident harborIncident =
                new Incident(
                    "A giant inflatable rubber duck is blocking the harbor.",
                    "Harbor",
                    Severity.Medium);

            Incident alpacaIncident =
                new Incident(
                    "Several alpacas are running on the motorway.",
                    "Motorway",
                    Severity.High);

            dispatchCenter.ReportIncident(
                harborIncident);

            dispatchCenter.ReportIncident(
                alpacaIncident);

            Console.WriteLine();

            // Dispatch hero.
            Console.WriteLine("=== DISPATCH ===");

            try
            {
                dispatchCenter.DispatchHero(
                    harborIncident);
            }
            catch (HeroUnavailableException ex)
            {
                Console.WriteLine(
                    "ERROR: " + ex.Message);
            }
            catch (NoSuitableHeroFoundException ex)
            {
                Console.WriteLine(
                    "ERROR: " + ex.Message);
            }

            Console.WriteLine();

            // Generic search.
            Console.WriteLine("=== GENERIC SEARCH ===");

            Hero availableHero =
                SearchHelper.FindFirst(
                    dispatchCenter.Heroes,
                    hero => hero.IsAvailable);

            if (availableHero != null)
            {
                Console.WriteLine(
                    "First available hero: "
                    + availableHero.Name);
            }

            Incident highIncident =
                SearchHelper.FindFirst(
                    dispatchCenter.Incidents,
                    incident =>
                        incident.Severity == Severity.High);

            if (highIncident != null)
            {
                Console.WriteLine(
                    "High severity incident: "
                    + highIncident.Description);
            }

            Console.WriteLine();

            // Demonstrate exception handling.
            Console.WriteLine("=== EXCEPTION HANDLING ===");

            try
            {
                // The hero assigned to the harbor is busy.
                dispatchCenter.DispatchHero(
                    harborIncident);
            }
            catch (HeroUnavailableException ex)
            {
                Console.WriteLine(
                    "Handled error: " + ex.Message);
            }
            catch (NoSuitableHeroFoundException ex)
            {
                Console.WriteLine(
                    "Handled error: " + ex.Message);
            }

            Console.WriteLine();

            // Named callback.
            Console.WriteLine("=== NAMED CALLBACK ===");

            dispatchCenter.ResolveIncident(
                harborIncident,
                OnIncidentResolved);

            Console.WriteLine();

            // Lambda callback.
            Console.WriteLine("=== LAMBDA CALLBACK ===");

            dispatchCenter.ResolveIncident(
                alpacaIncident,
                incident =>
                {
                    Console.WriteLine(
                        "Lambda callback: "
                        + incident.Description
                        + " is resolved.");
                });

            Console.WriteLine();

            // Second strategy.
            Console.WriteLine("=== SECOND STRATEGY ===");

            IDispatchStrategy strongestStrategy =
                new StrongestHeroStrategy();

            DispatchCenter secondDispatchCenter =
                new DispatchCenter(strongestStrategy);

            secondDispatchCenter.RegisterHero(
                new FlyingHero(
                    "Fast Flyer",
                    80));

            secondDispatchCenter.RegisterHero(
                new StrongHero(
                    "Super Titan",
                    150,
                    100));

            secondDispatchCenter.RegisterHero(
                new HealingHero(
                    "Super Medic",
                    110));

            Incident droneIncident =
                new Incident(
                    "A drone show has gone out of control.",
                    "Town Hall Square",
                    Severity.High);

            secondDispatchCenter.ReportIncident(
                droneIncident);

            try
            {
                secondDispatchCenter.DispatchHero(
                    droneIncident);
            }
            catch (HeroUnavailableException ex)
            {
                Console.WriteLine(
                    "ERROR: " + ex.Message);
            }
            catch (NoSuitableHeroFoundException ex)
            {
                Console.WriteLine(
                    "ERROR: " + ex.Message);
            }

            Console.WriteLine();

            // Display hero status.
            Console.WriteLine("=== HERO STATUS ===");

            foreach (Hero hero in dispatchCenter.Heroes)
            {
                Console.WriteLine(hero);
            }

            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("       PROGRAM FINISHED");
            Console.WriteLine("=================================");
        }

        /// <summary>
        /// Named callback executed when an incident is resolved.
        /// </summary>
        private static void OnIncidentResolved(
            Incident incident)
        {
            Console.WriteLine(
                "Named callback: "
                + incident.Description
                + " is resolved.");
        }
    }
}