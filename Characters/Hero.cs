using System;
using System.Collections.Generic;
using Heltevagten.Models;

namespace Heltevagten.Characters
{
    /// <summary>
    /// Represents the common base class for all heroes.
    /// </summary>
    public abstract class Hero
    {
        /// <summary>
        /// Gets the name of the hero.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the maximum energy of the hero.
        /// </summary>
        public int MaxEnergy { get; private set; }

        public HeroSpecialty Specialty { get; private set; }
        private int _energy;

        /// <summary>
        /// Gets the current energy of the hero.
        /// Energy cannot be changed directly from outside the class.
        /// </summary>
        public int Energy
        {
            get { return _energy; }
        }

        /// <summary>
        /// Gets or sets whether the hero is available.
        /// </summary>
        public bool IsAvailable { get; internal set; }

        /// <summary>
        /// Gets the equipment carried by the hero.
        /// </summary>
        public List<string> Equipment { get; private set; }

        /// <summary>
        /// Creates a new hero.
        /// </summary>
        protected Hero(string name, int maxEnergy, HeroSpecialty specialty)
        {
            // Check that the hero has a valid name.'
            // IsNullOrWhiteSpace also detects an empty string or spaces.
            if (string.IsNullOrWhiteSpace(name))
            {
                // Throw an exception if the name is invalid.
                throw new ArgumentException("Hero name cannot be empty.");
            }
            // Check that maximum energy is greater than zero.
            if (maxEnergy <= 0)
            {
                // Throw an exception if the energy value is invalid.
                throw new ArgumentException(
                    "Maximum energy must be greater than zero.");
            }
            // Store the constructor values in the object's properties.
            Name = name;
            MaxEnergy = maxEnergy;
            Specialty = specialty;
            // When a hero is created, its current energy starts at maximum.
            _energy = maxEnergy;
            // A newly created hero is available by default.
            IsAvailable = true;
            // Create an empty equipment list for the hero.
            Equipment = new List<string>();
        }

        /// <summary>
        /// Defines the signature move that every type of hero must implement.
        /// abstract means the Hero class does not provide the implementation.
        /// Each child class must implement its own version. 
        /// This demonstrates polymorphism.
        /// </summary>
        public abstract string UseSignatureMove();

        /// <summary>
        /// Uses a specific amount of the hero's energy.
        /// </summary>
        public void UseEnergy(int amount)
        {
            // Energy amount must be greater than zero.
            if (amount <= 0)
            {
                throw new ArgumentException(
                    "Energy amount must be greater than zero.");
            }
            // Check whether the hero has enough energy.
            if (amount > _energy)
            {
                throw new InvalidOperationException(
                    Name + " does not have enough energy.");
            }

            _energy -= amount;
        }

        /// <summary>
        /// Restores energy to the hero.
        /// </summary>
        public void RestoreEnergy(int amount)
        {
            // The amount restored must be greater than zero.
            if (amount <= 0)
            {
                throw new ArgumentException(
                    "Energy amount must be greater than zero.");
            }
            // Add the restored energy to the current energy.
            _energy += amount;
            // The hero cannot have more energy than MaxEnergy.
            if (_energy > MaxEnergy)
            {
                // If the value becomes too high, set it back to maximum.
                _energy = MaxEnergy;
            }
        }

        /// <summary> 
        /// Converts the Hero object into a readable string.
        /// This method overrides the ToString() method from the Object class. 
        /// </summary>
        public override string ToString()
        {
            // Return important information about the hero.
            return Name
                + " - Energy: "
                + Energy
                + "/"
                + MaxEnergy
                + " - Available: "
                + IsAvailable;
        }
    }
}