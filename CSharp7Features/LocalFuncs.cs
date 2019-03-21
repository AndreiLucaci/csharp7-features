using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp7Features
{
    public static class LocalFuncs
    {
        public static IEnumerable<(T, int index)> FindAll<T>(this IEnumerable<T> inputSource, Func<T, bool> what)
        {
            return Find();

            IEnumerable<(T, int index)> Find()
            {
                var counter = -1;
                foreach (var item in inputSource)
                {
                    counter++;
                    if (what(item)) yield return (item, counter);
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> input, Action<T> action)
        {
            foreach (var item in input)
            {
                action(item);
            }
        }
    }
}
