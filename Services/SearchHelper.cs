using System;
using System.Collections.Generic;

namespace Heltevagten.Services
{
    /// <summary>
    /// Provides reusable generic search functionality.
    /// This class can search through different types of collections.
    /// </summary>
    public static class SearchHelper
    {
        /// <summary>
        /// Finds the first item matching a condition.
        /// </summary>
        /// 
        /// <typeparam name="T">
        /// T is a generic type.
        /// It means this method can work with Hero, Incident,
        /// string, int, or another type.
        /// </typeparam>
        /// 
        /// <param name="items">
        /// The collection that we want to search.
        /// </param>
        /// 
        /// <param name="predicate">
        /// A condition that decides whether an item matches.
        /// Func<T, bool> means:
        /// - takes a T object as input
        /// - returns true or false
        /// /// </param>
        public static T FindFirst<T>(
            IEnumerable<T> items,
            Func<T, bool> predicate)
        {
            foreach (T item in items)// Go through every item in the collection one by one.
            {
                if (predicate(item))// Send the current item to the predicate.
                {// If the condition returns true, we found a match.
                    return item;// Return the first matching item.
                }
            }
            // If no item matches the condition, 
            // return the default value for type T.
            //
            // For a class/reference type, this is normally null. 
            // For int, it would be 0. 
            // For bool, it would be false.
            return default(T);
        }
    }
}