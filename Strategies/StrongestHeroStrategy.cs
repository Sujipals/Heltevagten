using System.Collections.Generic;
using System.Linq;
using Heltevagten.Characters;
using Heltevagten.Exceptions;
using Heltevagten.Interfaces;
using Heltevagten.Models;

namespace Heltevagten.Strategies
{
    /// <summary>
    /// Selects the available hero with the highest energy.
    /// </summary>
    public class StrongestHeroStrategy : IDispatchStrategy
    {
        /// <summary>
        /// Selects the strongest available hero for an incident. 
        /// In this strategy, "strongest" means the hero 
        /// with the highest current energy. 
        /// </summary>
        public Hero SelectHero(
            Incident incident,
            IEnumerable<Hero> heroes)
        {

            // Start with all heroes in the collection.
            Hero hero = heroes

                // Keep only heroes who are currently available.
                .Where(h => h.IsAvailable)

                // Sort the available heroes by their energy. 
                // OrderByDescending means highest energy comes first.
                .OrderByDescending(h => h.Energy)

                // Select the first hero in the sorted collection.
                // If there are no heroes, FirstOrDefault returns null.
                .FirstOrDefault();


            // Check whether a suitable hero was found.
            if (hero == null)
            {

                // Throw a custom exception if there are no available heroes.
                throw new NoSuitableHeroFoundException(
                    "No suitable hero was found for: "
                    + incident.Description);
            }

            return hero;// Return the hero with the highest energy.
        }
    }
}