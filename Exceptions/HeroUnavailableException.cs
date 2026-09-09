using System;

namespace Heltevagten.Exceptions
{
    /// <summary>
    /// Exception thrown when a hero is unavailable.
    /// </summary>
    public class HeroUnavailableException : Exception
    {
        public HeroUnavailableException(string message)
            : base(message)
        {
        }
    }
}