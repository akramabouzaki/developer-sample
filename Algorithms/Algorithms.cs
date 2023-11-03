using System;
using System.Linq;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int n) {
            if (n <= 1)
                return 1;
            else
                return (n * GetFactorial(n - 1));
        }

        public static string FormatSeparators(params string[] items)
        {
            if (items.Length == 1)
                return items[0];
            else
            {
                string[] temp = new string[items.Length - 1];
                Array.Copy(items, temp, items.Length - 1);
                string seperatedByComma = String.Join(", ", temp);
                string result = seperatedByComma + " and " + items[items.Length - 1];
                return result;
            }
        }
    }
}
