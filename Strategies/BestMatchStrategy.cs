
using System.Collections.Generic;
using Heltevagten.Characters;
using Heltevagten.Exceptions;
using Heltevagten.Interfaces;
using Heltevagten.Models;
using Heltevagten.Services;

namespace Heltevagten.Strategies
{
    /// <summary>
    /// Selects an available hero whose specialty
    /// matches the incident.
    /// </summary>sav
    public class BestMatchStrategy : IDispatchStrategy
    {
        /// <summary> 
        /// Selects the best available hero for an incident. 
        /// </summary>
        /// 
        /// <param name="incident">
        /// The incident that needs a hero.
        /// </param>
        /// 
        /// <param name="heroes">
        /// The collection of heroes that can be searched.
        /// </param>
        /// 
        /// <returns>
        /// Returns the first available hero whose specialty
        /// matches the incident's required specialty.
        /// </returns>
        public Hero SelectHero(
            Incident incident,
            IEnumerable<Hero> heroes)
        {
            // Search through all heroes using the reusable
            // generic FindFirst method.
            Hero hero = SearchHelper.FindFirst( 
                heroes,

                // This is a lambda expression.
                // 'h' represents the current Hero being checked.
                // 
                // The condition must be true for a hero to be selected:
                // 1. The hero must be available.
                // 2. The hero's specialty must match the incident.
                h =>
                    h.IsAvailable &&
                    h.Specialty ==
                    incident.RequiredSpecialty);

            // If no hero matched the conditions,
            // FindFirst returns the default value for Hero,
            // which is null.
            if (hero == null)
            {
                // Throw a custom exception because there is
                // no suitable hero available for this incident.
                throw new NoSuitableHeroFoundException(
                    "No available hero matches the required specialty: "
                    + incident.RequiredSpecialty);
            }
            // Return the hero that was found.
            return hero;
        }
    }
}

