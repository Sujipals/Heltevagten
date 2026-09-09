using System.Collections.Generic;
using Heltevagten.Characters;
using Heltevagten.Exceptions;
using Heltevagten.Interfaces;
using Heltevagten.Models;
using Heltevagten.Services;

namespace Heltevagten.Strategies
{
    /// <summary>
    /// Selects the first available hero.
    /// </summary>
    public class FirstAvailableStrategy : IDispatchStrategy
    {
        public Hero SelectHero(
            Incident incident,
            IEnumerable<Hero> heroes)
        {
            Hero hero = SearchHelper.FindFirst(
                heroes,
                h => h.IsAvailable);

            if (hero == null)
            {
                throw new NoSuitableHeroFoundException(
                    "No available hero was found for: "
                    + incident.Description);
            }

            return hero;
        }
    }
}