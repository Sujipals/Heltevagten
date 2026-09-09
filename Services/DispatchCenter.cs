
using System;
using System.Collections.Generic;
using Heltevagten.Characters;
using Heltevagten.Exceptions;
using Heltevagten.Interfaces;
using Heltevagten.Models;

namespace Heltevagten.Services
{
    /// <summary>
    /// Manages heroes and incidents in the Heltevagten system.
    /// </summary>
    public class DispatchCenter
    {
        private readonly List<Hero> _heroes;
        private readonly List<Incident> _incidents;

        // The strategy can be changed while the program is running.
        private IDispatchStrategy _dispatchStrategy;

        public DispatchCenter(
            IDispatchStrategy dispatchStrategy)
        {
            if (dispatchStrategy == null)
            {
                throw new ArgumentNullException(
                    "dispatchStrategy");
            }

            _dispatchStrategy = dispatchStrategy;

            _heroes = new List<Hero>();
            _incidents = new List<Incident>();
        }

        public IReadOnlyList<Hero> Heroes
        {
            get { return _heroes; }
        }

        public IReadOnlyList<Incident> Incidents
        {
            get { return _incidents; }
        }

        /// <summary>
        /// Changes the strategy used to select heroes.
        /// </summary>
        public void ChangeStrategy(
            IDispatchStrategy strategy)
        {
            if (strategy == null)
            {
                throw new ArgumentNullException(
                    "strategy");
            }

            _dispatchStrategy = strategy;
        }

        /// <summary>
        /// Adds a hero to the dispatch center.
        /// </summary>
        public void RegisterHero(Hero hero)
        {
            if (hero == null)
            {
                throw new ArgumentNullException("hero");
            }

            _heroes.Add(hero);

            Console.WriteLine(
                "Hero registered: "
                + hero.Name);
        }

        /// <summary>
        /// Adds a new incident to the system.
        /// </summary>
        public void ReportIncident(Incident incident)
        {
            if (incident == null)
            {
                throw new ArgumentNullException(
                    "incident");
            }

            _incidents.Add(incident);

            Console.WriteLine(
                "Incident reported: "
                + incident.Description);
        }

        /// <summary>
        /// Selects and dispatches an available hero.
        /// </summary>
        public Hero DispatchHero(Incident incident)
        {
            if (incident == null)
            {
                throw new ArgumentNullException(
                    "incident");
            }

            // An already resolved incident cannot be dispatched.
            if (incident.Status ==
                IncidentStatus.Resolved)
            {
                throw new InvalidOperationException(
                    "A resolved incident cannot be dispatched.");
            }

            // An incident that already has a hero cannot
            // receive another hero.
            if (incident.Status ==
                IncidentStatus.InProgress)
            {
                throw new InvalidOperationException(
                    "This incident already has a hero.");
            }

            Hero hero =
                _dispatchStrategy.SelectHero(
                    incident,
                    _heroes);

            if (hero == null)
            {
                throw new NoSuitableHeroFoundException(
                    "No suitable hero was found.");
            }

            if (!hero.IsAvailable)
            {
                throw new HeroUnavailableException(
                    hero.Name
                    + " is already busy.");
            }

            // The hero becomes unavailable.
            hero.IsAvailable = false;

            // Assigning the hero also changes
            // the incident status to InProgress.
            incident.AssignHero(hero.Name);

            Console.WriteLine();
            Console.WriteLine(
                hero.Name
                + " has been dispatched to: "
                + incident.Description);

            Console.WriteLine(
                hero.UseSignatureMove());

            return hero;
        }

        /// <summary>
        /// Resolves an incident after a hero has been assigned.
        /// </summary>
        public void ResolveIncident(
            Incident incident,
            Action<Incident> onResolved)
        {
            if (incident == null)
            {
                throw new ArgumentNullException(
                    "incident");
            }

            if (onResolved == null)
            {
                throw new ArgumentNullException(
                    "onResolved");
            }

            // The incident must have a hero first.
            if (incident.Status ==
                IncidentStatus.Open)
            {
                throw new InvalidOperationException(
                    "The incident must have a hero "
                    + "before it can be resolved.");
            }

            // Prevent resolving the same incident twice.
            if (incident.Status ==
                IncidentStatus.Resolved)
            {
                throw new InvalidOperationException(
                    "This incident is already resolved.");
            }

            incident.Resolve();

            // Find the hero who was assigned to the incident.
            Hero hero = SearchHelper.FindFirst(
                _heroes,
                h => h.Name ==
                     incident.AssignedHeroName);

            if (hero != null)
            {
                // The hero becomes available again.
                hero.IsAvailable = true;

                // Give the hero some energy back.
                hero.RestoreEnergy(20);
            }

            // Execute the callback.
            onResolved(incident);
        }
    }
}

