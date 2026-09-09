using System.Collections.Generic;
using Heltevagten.Characters;
using Heltevagten.Models;

namespace Heltevagten.Interfaces
{
    /// <summary>
    /// Defines how a hero is selected for an incident.
    /// </summary>
    public interface IDispatchStrategy
    {
        /// <summary>
        /// Selects a suitable hero.
        /// </summary>
        Hero SelectHero(
            Incident incident,
            IEnumerable<Hero> heroes);
    }
}