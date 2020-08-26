using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexyBoxTest.Utility.Extensions
{
    public static class CollectionExtensions
    {
        public delegate List<T> SearchDelegate<T>(List<T> inputs);
        
        //Task 3.2
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
        public static IEnumerable<T> FindElements<T>(this IEnumerable<T> inputs, params SearchDelegate<T>[] searchDelegate)
        {
            if (searchDelegate.Length < 1) throw new InvalidOperationException($"At least one {nameof(searchDelegate)} has to be supplied, otherwise no work will be done");

            var output = new List<T>(inputs);

            foreach (var @delegate in searchDelegate)
            {
                output = @delegate(output);
            }

            return output;
        }


        // Task 4.1
        public static IEnumerable<T> ReverseEnumerable<T>(this IEnumerable<T> input)
        {
            var output = new List<T>();

            var enumerable = input.ToList();
            for (var i = enumerable.Count()-1; i >= 0; i--)
            {
                output.Add(enumerable[i]);
            }

            return output;
        }

        // Task 4.3
        /// <summary>
        /// The input is ordered from smallest to highest before work begins.
        /// The same is also done to the output, before it is returned to the caller.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static IEnumerable<int> MissingElements(this IEnumerable<int> arr)
        {
            var output = new List<int>();

            var enumerable = arr.ToList().OrderBy(x => x).ToList();
            
            for (int i = 0; i < enumerable.Count; i++)
            {
                if (i + 1 == enumerable.Count) break;

                var num1 = enumerable[i];
                var num2 = enumerable[i+1];

                var difference = num2 - num1;

                while (difference > 1)
                {
                    var newNum = num1 + difference - 1;
                    output.Add(newNum);
                    difference--;
                }
            }

            output = output.OrderBy(x => x).ToList();

            return output;
        }
    }
}