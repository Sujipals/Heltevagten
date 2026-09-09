using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heltevagten.Exceptions;

/// <summary>
/// Exception thrown when a hero is not available.
/// </summary>
public class HeroUnavailableException : Exception
{
    public HeroUnavailableException(string message)
        : base(message)
    {
    }
}
