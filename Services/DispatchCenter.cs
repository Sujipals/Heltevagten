using System;
using System.Collections.Generic;
using Heltevagten.Characters;
using Heltevagten.Exceptions;
using Heltevagten.Interfaces;
using Heltevagten.Models;

namespace Heltevagten.Services;

/// <summary>
/// Represents the central system that manages heroes and incidents.
/// </summary>
public class DispatchCenter
{
    private readonly List<Hero> _heroes;
    private readonly List<Incident> _incidents;

    private readonly IDispatchStrategy _dispatchStrategy;

    /// <summary>
    /// Creates a dispatch center using the supplied dispatch strategy.
    /// </summary>
    /// <param name="dispatchStrategy">
    /// The strategy used to select heroes.
    /// </param>
    public DispatchCenter(IDispatchStrategy dispatchStrategy)
    {
        _dispatchStrategy = dispatchStrategy;
        _heroes = new List<Hero>();
        _incidents = new List<Incident>();
    }

    /// <summary>
    /// Gets all registered heroes.
    /// </summary>
    public IReadOnlyList<Hero> Heroes => _heroes;

    /// <summary>
    /// Gets all registered incidents.
    /// </summary>
    public IReadOnlyList<Incident> Incidents => _incidents;

    /// <summary>
    /// Registers a hero in the dispatch center.
    /// </summary>
    public void RegisterHero(Hero hero)
    {
        if (hero == null)
        {
            throw new ArgumentNullException(nameof(hero));
        }

        _heroes.Add(hero);

        Console.WriteLine($"Hero registered: {hero.Name}");
    }

    /// <summary>
    /// Reports a new incident.
    /// </summary>
    public void ReportIncident(Incident incident)
    {
        if (incident == null)
        {
            throw new ArgumentNullException(nameof(incident));
        }

        _incidents.Add(incident);

        Console.WriteLine($"Incident reported: {incident.Description}");
    }

    /// <summary>
    /// Dispatches a suitable hero to an incident.
    /// </summary>
    public Hero DispatchHero(Incident incident)
    {
        if (incident == null)
        {
            throw new ArgumentNullException(nameof(incident));
        }

        Hero hero = _dispatchStrategy.SelectHero(
            incident,
            _heroes);

        if (!hero.IsAvailable)
        {
            throw new HeroUnavailableException(
                $"{hero.Name} is already busy.");
        }

        hero.IsAvailable = false;
        incident.AssignHero(hero.Name);

        Console.WriteLine(
            $"{hero.Name} has been dispatched to: {incident.Description}");

        Console.WriteLine(
            hero.UseSignatureMove());

        return hero;
    }

    /// <summary>
    /// Marks an incident as resolved and executes a callback.
    /// </summary>
    /// <param name="incident">The incident to resolve.</param>
    /// <param name="onResolved">The callback executed after resolution.</param>
    public void ResolveIncident(
        Incident incident,
        Action<Incident> onResolved)
    {
        if (incident == null)
        {
            throw new ArgumentNullException(nameof(incident));
        }

        if (onResolved == null)
        {
            throw new ArgumentNullException(nameof(onResolved));
        }

        incident.Resolve();

        if (incident.AssignedHeroName != null)
        {
            Hero? hero = SearchHelper.FindFirst(
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