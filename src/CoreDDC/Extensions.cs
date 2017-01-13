using System;
using System.Collections.Generic;

namespace CoreDDC
{
    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new Random();
            for (var i = list.Count - 1; i > 0; i--)
            {
                var swapIndex = rng.Next(i + 1);
                var swap = list[swapIndex];
                list[swapIndex] = list[i];
                list[i] = swap;
            }
        }
    }
}
