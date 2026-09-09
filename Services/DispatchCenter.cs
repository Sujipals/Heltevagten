using System;
using System.Collections.Generic;
using Heltevagten.Characters;
using Heltevagten.Exceptions;
using Heltevagten.Interfaces;
using Heltevagten.Models;

namespace Heltevagten.Services
{
    /// <summary>
    /// Represents the central system that manages heroes and incidents.
    /// </summary>
    public class DispatchCenter
    {
        private readonly List<Hero> _heroes;
        private readonly List<Incident> _incidents;
        private readonly IDispatchStrategy _dispatchStrategy;

        /// <summary>
        /// Creates a dispatch center.
        /// </summary>
        public DispatchCenter(
            IDispatchStrategy dispatchStrategy)
        {
            _dispatchStrategy = dispatchStrategy;
            _heroes = new List<Hero>();
            _incidents = new List<Incident>();
        }

        /// <summary>
        /// Gets all registered heroes.
        /// </summary>
        public IReadOnlyList<Hero> Heroes
        {
            get { return _heroes; }
        }

        /// <summary>
        /// Gets all registered incidents.
        /// </summary>
        public IReadOnlyList<Incident> Incidents
        {
            get { return _incidents; }
        }

        /// <summary>
        /// Registers a hero.
        /// </summary>
        public void RegisterHero(Hero hero)
        {
            if (hero == null)
            {
                throw new ArgumentNullException("hero");
            }

            _heroes.Add(hero);

            Console.WriteLine(
                "Hero registered: " + hero.Name);
        }

        /// <summary>
        /// Reports an incident.
        /// </summary>
        public void ReportIncident(Incident incident)
        {
            if (incident == null)
            {
                throw new ArgumentNullException("incident");
            }

            _incidents.Add(incident);

            Console.WriteLine(
                "Incident reported: "
                + incident.Description);
        }

        /// <summary>
        /// Dispatches a suitable hero.
        /// </summary>
        public Hero DispatchHero(Incident incident)
        {
            if (incident == null)
            {
                throw new ArgumentNullException("incident");
            }

            Hero hero = _dispatchStrategy.SelectHero(
                incident,
                _heroes);

            if (!hero.IsAvailable)
            {
                throw new HeroUnavailableException(
                    hero.Name + " is already busy.");
            }

            hero.IsAvailable = false;

            incident.AssignHero(hero.Name);

            Console.WriteLine(
                hero.Name
                + " has been dispatched to: "
                + incident.Description);

            Console.WriteLine(
                hero.UseSignatureMove());

            return hero;
        }

        /// <summary>
        /// Resolves an incident and executes a callback.
        /// </summary>
        public void ResolveIncident(
            Incident incident,
            Action<Incident> onResolved)
        {
            if (incident == null)
            {
                throw new ArgumentNullException("incident");
            }

            if (onResolved == null)
            {
                throw new ArgumentNullException("onResolved");
            }

            incident.Resolve();

            if (incident.AssignedHeroName != null)
            {
                Hero hero = SearchHelper.FindFirst(
                    _heroes,
                    h => h.Name == incident.AssignedHeroName);

                if (hero != null)
                {
                    hero.IsAvailable = true;
                    hero.RestoreEnergy(20);
                }
            }

            onResolved(incident);
        }
    }
}