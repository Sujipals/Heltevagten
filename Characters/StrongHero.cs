using System;
using Heltevagten.Interfaces;
using Heltevagten.Models;

namespace Heltevagten.Characters
{
    /// <summary>
    /// Gets the strength level.
    /// Represents a physically strong hero.
    /// StrongHero inherits from Hero and implements ISuperStrong.
    /// </summary>
    public class StrongHero : Hero, ISuperStrong
    {
        /// <summary> 
        /// Gets the strength level of the hero.
        /// private set means other classes can read it,
        /// but only this class can change it.
        /// This is encapsulation. 
        /// </summary>
        public int Strength { get; private set; }
        /// <summary> 
        /// Creates a new StrongHero. 
        /// </summary>
        public StrongHero(
            string name,
            int maxEnergy,
            int strength)
            // Calls the Hero constructor.
            // This hero gets the Strength specialty.
            : base(name, maxEnergy, HeroSpecialty.Strength)
        {
            // Check that the strength value is valid.
            if (strength <= 0)
            {
                // Stop the object from being created with 
                // an invalid strength value.
                throw new ArgumentException(
                    "Strength must be greater than zero.");
            }
            // Store the validated strength value.
            Strength = strength;
        }

        /// <summary>
        /// Uses the strong hero's signature move.
        /// </summary>
        public override string UseSignatureMove()
        {
            UseEnergy(25);// The signature move costs 25 energy.

            return Name// Return a description of the action.
                + " uses incredible strength to solve the problem.";
        }

        /// <summary>
        /// Lifts a heavy object.
        /// </summary>
        public void LiftHeavyObject()
        {
            UseEnergy(15);// Lifting a heavy object costs 15 energy.

            Console.WriteLine(// Display a message in the console.
                Name + " lifts a heavy object.");
        }
    }
}