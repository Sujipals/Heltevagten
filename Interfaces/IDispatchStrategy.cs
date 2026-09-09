using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using Heltevagten.Characters;
using Heltevagten.Models;

namespace Heltevagten.Interfaces

/// <summary>
/// Defines how a hero is selected for an incident.
/// </summary>
public interface IDispatchStrategy
{
    /// <summary>
    /// Selects a suitable hero for an incident.
    /// </summary>
    /// <param name="incident">The incident requiring a hero.</param>
    /// <param name="heroes">The registered heroes.</param>
    /// <returns>The selected hero.</returns>
    Hero SelectHero(Incident incident, IEnumerable<Hero> heroes);
}