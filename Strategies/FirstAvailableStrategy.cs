using System.Collections.Generic;
using Heltevagten.Characters;
using Heltevagten.Exceptions;
using Heltevagten.Interfaces;
using Heltevagten.Models;
using Heltevagten.Services;

namespace Heltevagten.Strategies
{
    /// <summary>
    /// Selects the first available hero from the collection.
    /// This strategy does not check the hero's specialty.
    /// </summary>
    public class FirstAvailableStrategy : IDispatchStrategy
    {
        /// <summary>
        /// Selects a hero to handle an incident.
        /// </summary>
        /// 
        /// <param name="incident">
        /// The incident that needs a hero.
        /// 
        /// <param name="heroes">
        /// The collection of heroes that will be searched.
        /// </param>
        /// 
        /// <returns>
        /// Returns the first available hero.
        /// </returns>
        public Hero SelectHero(
            Incident incident,
            IEnumerable<Hero> heroes)
        {
            // Search through the heroes using the reusable
            // generic FindFirst method.
            Hero hero = SearchHelper.FindFirst(
                heroes,

                // Lambda expression used as the search condition.
                // 'h' represents the current hero.
                //
                // The condition is simple:
                // Is the hero available?
                h => h.IsAvailable);
            // If FindFirst did not find an available hero,
            // it returns null because Hero is a reference type.
            if (hero == null)
            {
                // Throw a custom exception to tell the program
                // that no available hero could be found.
                throw new NoSuitableHeroFoundException(
                    "No available hero was found for: "
                    + incident.Description);
            }
            // Return the first available hero.
            return hero;
        }
    }
}