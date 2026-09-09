using System;
using System.Collections.Generic;

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
        protected Hero(string name, int maxEnergy)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Hero name cannot be empty.");
            }

            if (maxEnergy <= 0)
            {
                throw new ArgumentException(
                    "Maximum energy must be greater than zero.");
            }

            Name = name;
            MaxEnergy = maxEnergy;
            _energy = maxEnergy;
            IsAvailable = true;
            Equipment = new List<string>();
        }

        /// <summary>
        /// Uses the hero's unique signature move.
        /// </summary>
        public abstract string UseSignatureMove();

        /// <summary>
        /// Uses energy.
        /// </summary>
        public void UseEnergy(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(
                    "Energy amount must be greater than zero.");
            }

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
            if (amount <= 0)
            {
                throw new ArgumentException(
                    "Energy amount must be greater than zero.");
            }

            _energy += amount;

            if (_energy > MaxEnergy)
            {
                _energy = MaxEnergy;
            }
        }

        public override string ToString()
        {
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