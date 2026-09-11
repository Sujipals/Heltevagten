using Heltevagten.Interfaces;
using Heltevagten.Models;
using System;

namespace Heltevagten.Characters
{
    /// <summary>
    /// Represents a hero who can fly.
    /// FlyingHero inherits common properties and methods from Hero.
    /// It also implements the IFlyable interface.
    /// </summary>
    public class FlyingHero : Hero, IFlyable
    {
        /// <summary> 
        /// Creates a new FlyingHero. 
        /// </summary>
        public FlyingHero(string name, int maxEnergy)// Calls the constructor of the Hero base class. 
            : base(name, maxEnergy, HeroSpecialty.Rescue)// This also gives the hero the Rescue specialty.
        {
        }

        /// <summary>
        /// Uses the flying hero's signature move.
        /// </summary>
        public override string UseSignatureMove()
        {
            // The signature move costs 20 energy.
            UseEnergy(20);
            // Return a description of what the hero does.
            return Name + " flies rapidly to the incident.";
        }

        /// <summary>
        /// Makes the hero fly.
        /// </summary>
        public void Fly()
        {
            UseEnergy(10);// Flying costs 10 energy.

            Console.WriteLine(Name + " is flying.");// Display a message in the console.
        }
    }
}