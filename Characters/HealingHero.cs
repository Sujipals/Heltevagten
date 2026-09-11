using System;
using Heltevagten.Models;

namespace Heltevagten.Characters
{
    /// <summary>
    /// Represents a hero who can heal people.
    /// HealingHero inherits common functionality from Hero.
    /// </summary>
    public class HealingHero : Hero
    {
        /// <summary> 
        /// Creates a new HealingHero.
        /// </summary>
        public HealingHero(string name, int maxEnergy)// Calls the Hero constructor.
            : base(name, maxEnergy, HeroSpecialty.Medical)// This hero has the Medical specialty.
        {
        }

        /// <summary>
        /// Uses the healing hero's signature move.
        /// </summary>
        public override string UseSignatureMove()
        {
            UseEnergy(15);// The signature move costs 15 energy.

            return Name// Return a description of the action.
                + " uses healing powers to help people.";
        }

        /// <summary>
        /// Heals an injured person.
        /// </summary>
        public void Heal()
        {
            UseEnergy(10);// Healing costs 10 energy.

            // Display a message showing that the hero healed someone.
            Console.WriteLine(
                Name + " heals an injured person.");
        }
    }
}