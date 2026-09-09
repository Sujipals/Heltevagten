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
        public Hero SelectHero(
            Incident incident,
            IEnumerable<Hero> heroes)
        {
            Hero hero = heroes
                .Where(h => h.IsAvailable)
                .OrderByDescending(h => h.Energy)
                .FirstOrDefault();

            if (hero == null)
            {
                throw new NoSuitableHeroFoundException(
                    "No suitable hero was found for: "
                    + incident.Description);
            }

            return hero;
        }
    }
}