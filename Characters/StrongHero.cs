using Heltevagten.Interfaces;

namespace Heltevagten.Characters;

/// <summary>
/// Represents a physically strong hero.
/// </summary>
public class StrongHero : Hero, ISuperStrong
{
    /// <summary>
    /// Gets the strength level of the hero.
    /// </summary>
    public int Strength { get; }

    public StrongHero(string name, int maxEnergy, int strength)
        : base(name, maxEnergy)
    {
        if (strength <= 0)
        {
            throw new System.ArgumentException("Strength must be greater than zero.");
        }

        Strength = strength;
    }

    /// <summary>
    /// Uses the strong hero's signature move.
    /// </summary>
    public override string UseSignatureMove()
    {
        UseEnergy(25);
        return $"{Name} uses incredible strength to solve the problem.";
    }

    /// <summary>
    /// Lifts a heavy object.
    /// </summary>
    public void LiftHeavyObject()
    {
        UseEnergy(15);
        System.Console.WriteLine($"{Name} lifts a heavy object.");
    }
}