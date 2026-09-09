using System;
using Heltevagten.Models;

namespace Heltevagten.Characters
{
    /// <summary>
    /// Represents a hero who can heal people.
    /// </summary>
    public class HealingHero : Hero
    {
        public HealingHero(string name, int maxEnergy)
            : base(name, maxEnergy, HeroSpecialty.Medical)
        {
        }

        /// <summary>
        /// Uses the healing hero's signature move.
        /// </summary>
        public override string UseSignatureMove()
        {
            UseEnergy(15);

            return Name
                + " uses healing powers to help people.";
        }

        /// <summary>
        /// Heals an injured person.
        /// </summary>
        public void Heal()
        {
            UseEnergy(10);

            Console.WriteLine(
                Name + " heals an injured person.");
        }
    }
}