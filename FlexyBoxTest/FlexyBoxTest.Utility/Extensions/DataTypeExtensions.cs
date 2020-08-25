using System;
using System.Linq;

namespace FlexyBoxTest.Utility.Extensions
{
    public static class DataTypeExtensions
    {
        public static bool IsPalindrome(this string str)
        {
            var reverse = new string(str.ToList().ReverseList().ToArray());

            if (str == reverse) return true;
            return false;
        }
    }
}