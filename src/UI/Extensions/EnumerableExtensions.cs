using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITool.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Checks whether the given sequence is the empty set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> sequence)
        {
            return sequence == null || !sequence.Any();
        }
        public static bool IsNotEmpty<T>(this IEnumerable<T> sequence)
        {
            return sequence != null && sequence.Any();
        }
    }

}
