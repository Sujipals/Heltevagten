using Heltevagten.Interfaces;
using Heltevagten.Models;
using System;

namespace Heltevagten.Characters
{
    /// <summary>
    /// Represents a hero who can fly.
    /// </summary>
    public class FlyingHero : Hero, IFlyable
    {
        public FlyingHero(string name, int maxEnergy)
            : base(name, maxEnergy, HeroSpecialty.Rescue)
        {
        }

        /// <summary>
        /// Uses the flying hero's signature move.
        /// </summary>
        public override string UseSignatureMove()
        {
            UseEnergy(20);

            return Name + " flies rapidly to the incident.";
        }

        /// <summary>
        /// Makes the hero fly.
        /// </summary>
        public void Fly()
        {
            UseEnergy(10);

            Console.WriteLine(Name + " is flying.");
        }
    }
}