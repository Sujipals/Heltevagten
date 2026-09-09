using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heltevagten.Exceptions;

/// <summary>
/// Exception thrown when no suitable hero can be found.
/// </summary>
public class NoSuitableHeroFoundException : Exception
{
    public NoSuitableHeroFoundException(string message)
        : base(message)
    {
    }
}
