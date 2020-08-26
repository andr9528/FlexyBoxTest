using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexyBoxTest.Utility.Extensions
{
    public static class DataTypeExtensions
    {
        // Task 4.2
        public static bool IsPalindrome(this string str)
        {
            var reverse = new string(str.ToList().ReverseEnumerable().ToArray());

            if (str == reverse) return true;
            return false;
        }

        // Task 4.1
        public static string GetString(this IEnumerable<char> chars)
        {
            return new string(chars.ToArray());
        }
    }
}