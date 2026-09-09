using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heltevagten.Models;

/// <summary>
/// Represents an incident reported to Heltevagten.
/// </summary>
public class Incident
{
    /// <summary>
    /// Gets the description of the incident.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the location of the incident.
    /// </summary>
    public string Location { get; }

    /// <summary>
    /// Gets the severity of the incident.
    /// </summary>
    public Severity Severity { get; }

    /// <summary>
    /// Gets whether the incident has been resolved.
    /// </summary>
    public bool IsResolved { get; private set; }

    /// <summary>
    /// Gets the hero assigned to the incident.
    /// </summary>
    public string? AssignedHeroName { get; private set; }

    /// <summary>
    /// Creates a new incident.
    /// </summary>
    public Incident(
        string description,
        string location,
        Severity severity)
    {
        Description = description;
        Location = location;
        Severity = severity;
        IsResolved = false;
    }

    /// <summary>
    /// Assigns a hero to the incident.
    /// </summary>
    public void AssignHero(string heroName)
    {
        AssignedHeroName = heroName;
    }

    /// <summary>
    /// Marks the incident as resolved.
    /// </summary>
    public void Resolve()
    {
        IsResolved = true;
    }

    public override string ToString()
    {
        return $"{Description} | Location: {Location} | Severity: {Severity} | Resolved: {IsResolved}";
    }
}
