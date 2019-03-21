using System;
using System.Collections.Generic;

namespace CSharp7Features
{
    public class Ref
    {
        public void TestRef()
        {
            void PrintItems (IEnumerable<int> nrs) => Console.WriteLine(string.Join(", ", nrs));

            var items = new[] {1, 2, 3, 4};

            PrintItems(items); // 1, 2, 3, 4

            ref var item = ref GetRefItem(items, 1);
            item = int.MaxValue;

            PrintItems(items); // 1, 2147483647. 3, 4
        }

        private ref int GetRefItem(int[] nrs, int index)
        {
            if (index < 0 || index > nrs.Length) throw new ArgumentOutOfRangeException();

            return ref nrs[index];
        }
    }
}
