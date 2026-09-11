using System;

namespace Heltevagten.Exceptions
{
    /// <summary>
    /// Custom exception that is thrown when the system
    /// cannot find a hero who is suitable for an incident.
    /// </summary>
    public class NoSuitableHeroFoundException : Exception
    {
        /// <summary> 
        /// Creates a new NoSuitableHeroFoundException 
        /// with a specific error message.
        /// </summary>
        public NoSuitableHeroFoundException(string message)
            // Passes the error message to the parent Exception class.
            : base(message)
        {
        }
    }
}