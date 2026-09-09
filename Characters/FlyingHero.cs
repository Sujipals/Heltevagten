using Heltevagten.Interfaces;

namespace Heltevagten.Characters;

/// <summary>
/// Represents a hero who can fly.
/// </summary>
public class FlyingHero : Hero, IFlyable
{
    public FlyingHero(string name, int maxEnergy)
        : base(name, maxEnergy)
    {
    }

    /// <summary>
    /// Uses the flying hero's signature move.
    /// </summary>
    public override string UseSignatureMove()
    {
        UseEnergy(20);
        return $"{Name} flies rapidly to the incident.";
    }

    /// <summary>
    /// Makes the hero fly.
    /// </summary>
    public void Fly()
    {
        UseEnergy(10);
        System.Console.WriteLine($"{Name} is flying.");
    }
}