using System;
using System.Collections.Generic;

namespace Heltevagten.Services
{
    /// <summary>
    /// Provides reusable generic search functionality.
    /// </summary>
    public static class SearchHelper
    {
        /// <summary>
        /// Finds the first item matching a condition.
        /// </summary>
        public static T FindFirst<T>(
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

            return default(T);
        }
    }
}