
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heltevagten.Interfaces;

/// <summary>
/// Represents something that can fly.
/// </summary>
public interface IFlyable
{
    /// <summary>
    /// Makes the object fly.
    /// </summary>
    void Fly();
}