using System;
using System.Collections.Generic;
using Heltevagten.Interfaces;
using Heltevagten.Models;

namespace Heltevagten.Characters;

/// <summary>
/// Represents the common base class for all heroes.
/// </summary>
public abstract class Hero
{
    /// <summary>
    /// Gets the name of the hero.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the maximum energy of the hero.
    /// </summary>
    public int MaxEnergy { get; }

    private int _energy;

    /// <summary>
    /// Gets the current energy of the hero.
    /// Energy cannot be changed directly from outside the class.
    /// </summary>
    public int Energy => _energy;

    /// <summary>
    /// Gets or sets whether the hero is available for an incident.
    /// </summary>
    public bool IsAvailable { get; internal set; }

    /// <summary>
    /// Gets the items or equipment carried by the hero.
    /// </summary>
    public List<string> Equipment { get; }

    /// <summary>
    /// Creates a new hero.
    /// </summary>
    /// <param name="name">The hero's name.</param>
    /// <param name="maxEnergy">The maximum energy.</param>
    protected Hero(string name, int maxEnergy)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Hero name cannot be empty.");
        }

        if (maxEnergy <= 0)
        {
            throw new ArgumentException("Maximum energy must be greater than zero.");
        }

        Name = name;
        MaxEnergy = maxEnergy;
        _energy = maxEnergy;
        IsAvailable = true;
        Equipment = new List<string>();
    }

    /// <summary>
    /// Uses the hero's unique signature move.
    /// Each concrete hero implements this differently.
    /// </summary>
    /// <returns>A description of the signature move.</returns>
    public abstract string UseSignatureMove();

    /// <summary>
    /// Uses energy.
    /// </summary>
    /// <param name="amount">The amount of energy to use.</param>
    public void UseEnergy(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Energy amount must be greater than zero.");
        }

        if (amount > _energy)
        {
            throw new InvalidOperationException($"{Name} does not have enough energy.");
        }

        _energy -= amount;
    }

    /// <summary>
    /// Restores energy to the hero.
    /// </summary>
    /// <param name="amount">The amount of energy to restore.</param>
    public void RestoreEnergy(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Energy amount must be greater than zero.");
        }

        _energy += amount;

        if (_energy > MaxEnergy)
        {
            _energy = MaxEnergy;
        }
    }

    /// <summary>
    /// Returns information about the hero.
    /// </summary>
    public override string ToString()
    {
        return $"{Name} - Energy: {Energy}/{MaxEnergy} - Available: {IsAvailable}";
    }
}