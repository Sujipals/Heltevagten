using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heltevagten.Services;

/// <summary>
/// Provides reusable generic search functionality.
/// </summary>
public static class SearchHelper
{
    /// <summary>
    /// Finds the first item that matches the supplied condition.
    /// </summary>
    /// <typeparam name="T">The type of item being searched.</typeparam>
    /// <param name="items">The collection to search.</param>
    /// <param name="predicate">The condition that must be satisfied.</param>
    /// <returns>The first matching item, or null if no item matches.</returns>
    public static T? FindFirst<T>(
        IEnumerable<T> items,
        Func<T, bool> predicate)
    {
        foreach (T item in items)
        {
            if (predicate(item))
            {
                return item;
            }
        }

        return default;
    }
}
