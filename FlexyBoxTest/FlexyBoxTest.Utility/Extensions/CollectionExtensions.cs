using System;
using System.Collections.Generic;

namespace FlexyBoxTest.Utility.Extensions
{
    public static class CollectionExtensions
    {
        public delegate List<T> SearchDelegate<T>(List<T> inputs);
        
        /// <summary>
        /// The initial inputs parameter is passed to the searchDelegate, which then chains down through all the searchDelegates
        /// Using this, many different actions to sort away invalid or wrong elements can be used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputs"></param>
        /// <param name="searchDelegate"></param>
        /// <returns></returns>
        public static List<T> FindElements<T>(this List<T> inputs, params SearchDelegate<T>[] searchDelegate)
        {
            if (searchDelegate.Length < 1) throw new InvalidOperationException($"At least one {nameof(searchDelegate)} has to be supplied, otherwise no work will be done");

            var output = new List<T>(inputs);

            foreach (var @delegate in searchDelegate)
            {
                output = @delegate(output);
            }

            return output;
        }
    }
}