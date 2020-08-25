using System;
using System.Collections.Generic;

namespace FlexyBoxTest.Utility.Extensions
{
    public static class CollectionExtensions
    {
        public delegate List<T> SearchDelegate<T>(List<T> inputs);
        
        /// <summary>
        /// Allows the use of multiple search or other methods of specifying the target.
        ///
        /// The order of which properties are used to search with, should in theory never alter the end result.
        ///
        /// An example of how this could be used is as follows:
        ///
        /// The List that has to be searched through is followed by a dot,
        /// after which the name of the targeted class is defined in a short expression to the delegate.
        /// From here the user can stop giving expressions that would limit the result, and get it,
        /// or he could ask that the result is only the ones where MaxSpeed is 100 or higher.
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

        public static List<T> ReverseList<T>(this List<T> input)
        {
            var output = new List<T>();
            
            for (var i = input.Count-1; i >= 0; i--)
            {
                output.Add(input[i]);
            }

            return output;
        }
    }
}