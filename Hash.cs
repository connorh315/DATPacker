using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATPacker
{
    internal static class Hash
    {
        public static long Calculate(string toHash)
        {
            long hash = -3750763034362895579;
            foreach (char character in toHash)
            {
                hash ^= character;
                hash *= 1099511628211;
            }
            return hash;
        }

        public static long Extend(long hash, string segment)
        {
            hash ^= '\\';
            hash *= 1099511628211;
            foreach (char character in segment)
            {
                hash ^= character;
                hash *= 1099511628211;
            }

            return hash;
        }
    }
}
