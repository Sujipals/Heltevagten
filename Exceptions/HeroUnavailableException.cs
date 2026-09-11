using System;

namespace Heltevagten.Exceptions
{
    /// <summary> 
    /// Custom exception that is thrown when a hero
    /// cannot be used because the hero is unavailable. 
    /// </summary>
    public class HeroUnavailableException : Exception
    {
        /// <summary> 
        /// Creates a new HeroUnavailableException 
        /// with a specific error message.
        /// </summary>
        public HeroUnavailableException(string message)
            // Passes the error message to the parent Exception class.
            : base(message)
        {
        }
    }
}