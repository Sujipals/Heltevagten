
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
    /// </summary>
    public class BestMatchStrategy : IDispatchStrategy
    {
        public Hero SelectHero(
            Incident incident,
            IEnumerable<Hero> heroes)
        {
            Hero hero = SearchHelper.FindFirst(
                heroes,
                h =>
                    h.IsAvailable &&
                    h.Specialty ==
                    incident.RequiredSpecialty);

            if (hero == null)
            {
                throw new NoSuitableHeroFoundException(
                    "No available hero matches the required specialty: "
                    + incident.RequiredSpecialty);
            }

            return hero;
        }
    }
}

