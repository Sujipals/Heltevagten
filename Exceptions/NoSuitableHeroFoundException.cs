using System;

namespace Heltevagten.Exceptions
{
    /// <summary>
    /// Exception thrown when no suitable hero is found.
    /// </summary>
    public class NoSuitableHeroFoundException : Exception
    {
        public NoSuitableHeroFoundException(string message)
            : base(message)
        {
        }
    }
}