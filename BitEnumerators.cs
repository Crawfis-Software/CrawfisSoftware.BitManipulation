using System.Collections.Generic;
using System.Numerics;

namespace BitManipulation
{

    public static class BitEnumerators
    {
        
        // Tables store values with an odd or even number of bits. Entry zero is ignored.
        private static readonly BigInteger[][] OddNumberOfBitsTable = new BigInteger[][] { null, new BigInteger[] { 1 },
            new BigInteger[] { 1, 2 }
        };
        private static readonly BigInteger[][] EvenNumberOfBitsTable = new BigInteger[][] { null,
            new BigInteger[] { 0 }, new BigInteger[] { 0 , 3} };
        private const int TableSize = 3;

        /// <summary>
        /// Algorithm to iterate over all bit patterns up to the specified width that have an
        /// even number of 1's.
        /// </summary>
        /// <param name="width">The number of bits in the patterns. Must be less than 31.</param>
        /// <returns>An enumerated stream of ints, where each value has an even number of 1's.</returns>
        /// <remarks>This method is NP in runtime, as is the number of return values!</remarks>
        public static IEnumerable<BigInteger> AllEven(int width)
        {
            if (width < TableSize)
            {
                if (EvenNumberOfBitsTable[width] != null)
                {
                    foreach (int pattern in EvenNumberOfBitsTable[width])
                    {
                        yield return pattern;
                    }
                }

                // else
                yield break;
            }

            // else
            int leadingOne = 1 << width - 1;
            foreach (BigInteger pattern in AllEven(width - 1))
            {
                yield return pattern;
            }

            foreach (BigInteger pattern in AllOdd(width - 1))
            {
                yield return leadingOne + pattern;
            }
        }

        /// <summary>
        /// Algorithm to iterate over all bit patterns up to the specified width that have an
        /// odd number of 1's.
        /// </summary>
        /// <param name="width">The number of bits in the patterns. Must be less than 31.</param>
        /// <returns>An enumerated stream of ints, where each value has an odd number of 1's.</returns>
        /// <remarks>This method is NP in runtime, as is the number of return values!</remarks>
        public static IEnumerable<BigInteger> AllOdd(int width)
        {
            if (width < TableSize)
            {
                if (OddNumberOfBitsTable[width] != null)
                {
                    foreach (int pattern in OddNumberOfBitsTable[width])
                    {
                        yield return pattern;
                    }
                }

                // else
                yield break;
            }

            // else - Uses Recursion until reaches the table size.
            int leadingOne = 1 << width - 1;
            foreach (BigInteger pattern in AllOdd(width - 1))
            {
                yield return pattern;
            }

            foreach (BigInteger pattern in AllEven(width - 1))
            {
                yield return leadingOne + pattern;
            }
        }
    }
}