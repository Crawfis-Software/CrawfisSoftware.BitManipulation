using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BitManipulation
{
    /// <summary>
    ///     A static utility class for bit manipulation
    /// </summary>
    public static class BitUtility
    {
        /// <summary>
        ///     Check if the posth bit in inflow is 1
        /// </summary>
        /// <param name="inflow">bit to check</param>
        /// <param name="pos">position to check</param>
        /// <param name="width">the width of a bit pattern</param>
        /// <returns></returns>
        public static bool IsBitSet(BigInteger inflow, int pos, int width)
        {
            return OpeningsFromBits(inflow, width).Contains(pos);
        }


        /// <summary>
        ///     Set the posth bit in bit to 0
        /// </summary>
        /// <param name="bit">bits to manipulate</param>
        /// <param name="pos">position to block</param>
        /// <returns>a bit with posth bit set to 0</returns>
        public static BigInteger BlockBit(BigInteger bit, int pos)
        {
            BigInteger one = new BigInteger(1);
            return bit &= ~(one << pos);
        }

        /// <summary>
        ///     Set the posth bit in bit to 1
        /// </summary>
        /// <param name="startingPos">starting position to open</param>
        /// <param name="endingPos">ending position to open</param>
        /// <returns>a bit with posth bit set to 1</returns>
        public static BigInteger OpenBits(int startingPos, int endingPos)
        {
            BigInteger one = new BigInteger(1);
            return ((one << (endingPos - startingPos)) - one) << startingPos;
        }

        /// <summary>
        ///     Set the posth bit in bit to 1
        /// </summary>
        /// <param name="bit">bits to manipulate</param>
        /// <param name="pos">position to open</param>
        /// <returns>a bit with posth bit set to 1</returns>
        public static BigInteger OpenBit(BigInteger bit, int pos)
        {
            return bit |= BigInteger.One << pos;
        }


        /// <summary>
        ///     Print the given bit pattern with given width
        /// </summary>
        /// <param name="flow">bit pattern to print</param>
        /// <param name="width">the width of the bit pattern</param>
        public static string PrintBits(BigInteger flow, int width)
        {
            List<int> bits = new List<int>();
            BigInteger mask = new BigInteger(1);
            for (int i = 0; i < width; i++)
            {
                //mask = 1 << (width - i - 1);
                if ((flow & mask) == mask)
                    bits.Add(1);
                else
                    bits.Add(0);

                mask <<= 1;
            }

            Console.WriteLine(string.Join("", bits));
            return string.Join("", bits);
        }

        public static string VisualizeVerticalBits(BigInteger flow, int width)
        {
            StringBuilder builder = new StringBuilder();
            BigInteger mask = BigInteger.One;
            for (int i = 0; i < width; i++)
            {
                Console.Write("+");
                builder.Append("+");
                //mask = 1 << (width - i - 1);
                if ((flow & mask) == mask)
                {
                    Console.Write("   ");
                    builder.Append("   ");
                }
                else
                {
                    Console.Write("...");
                    builder.Append("...");
                }

                mask <<= 1;
            }

            Console.Write("+");
            builder.Append("+");
            Console.WriteLine();
            builder.Append("\n");
            return builder.ToString();
        }

        public static string VisualizeHorizontalBits(BigInteger flow, int width)
        {
            StringBuilder builder = new StringBuilder();
            Console.Write("|");
            builder.Append("|");
            BigInteger mask = new BigInteger(1);
            for (int i = 0; i < width; i++)
            {
                Console.Write("   ");
                builder.Append("   ");
                if ((flow & mask) == mask)
                {
                    Console.Write(" ");
                    builder.Append(" ");
                }
                else
                {
                    Console.Write("|");
                    builder.Append("|");
                }

                mask <<= 1;
            }

            Console.WriteLine();
            builder.Append("\n");
            return builder.ToString();
        }

        /// <summary>
        ///     Set the highest set bet of a bit pattern to 0
        /// </summary>
        /// <param name="bit">the bit to change</param>
        /// <returns>changed bit pattern</returns>
        public static BigInteger BlockHighestSetBit(BigInteger bit)
        {
            BigInteger bitCopy = bit;
            int highestSetBit = 0; // assume that to begin with, x is all zeroes
            while (bitCopy != 0)
            {
                ++highestSetBit;
                bitCopy >>= 1;
            }

            BigInteger blockedBit = BlockBit(bit, highestSetBit - 1);
            return blockedBit;
        }

        /// <summary>
        ///     Set the lowest set bet of a bit pattern to 0
        /// </summary>
        /// <param name="bit">the bit to change</param>
        /// <returns>changed bit pattern</returns>
        public static BigInteger BlockLowestSetBit(BigInteger bit)
        {
            BigInteger blockedBit = bit & (bit - BigInteger.One);
            return blockedBit;
        }

        /// <summary>
        ///     Merge multiple bits of width into a single bit
        /// </summary>
        /// <param name="bits">bits to merge</param>
        /// <param name="width">width of the bit pattern</param>
        /// <returns>merged bits</returns>
        public static BigInteger ConcatenateMultipleBits(IList<int> bits, int width)
        {
            BigInteger mergedBit = bits[0];
            for (int i = 1; i < bits.Count; i++) mergedBit = (mergedBit << width) | bits[i];
            return mergedBit;
        }

        /// <summary>
        ///     Returns the column indices of the inflows of a row
        /// </summary>
        /// <param name="width">The width of the row</param>
        /// <param name="row">The inflow bit pattern</param>
        /// <returns></returns>
        public static List<int> OpeningsFromBits(BigInteger row, int width)
        {
            List<int> inFlows = new List<int>();
            BigInteger mask = new BigInteger(1);
            for (int i = 0; i < width; i++)
            {
                //mask = 1 << (width - i - 1);
                if ((row & mask) == mask) inFlows.Add(i);
                mask <<= 1;
            }

            return inFlows;
        }

        /// <summary>
        ///     Count the number of set bits in the bit pattern
        /// </summary>
        /// <param name="n">Base 10 representation of bits</param>
        /// <returns></returns>
        public static int CountSetBits(BigInteger n)
        {
            int count = 0;
            while (n > 0)
            {
                n &= n - BigInteger.One;
                count++;
            }

            return count;
        }

        /// <summary>
        ///     Returns a random BigInteger with a bit count of <paramref name="bitLength" />(inclusive).
        /// </summary>
        /// <param name="random">Random number generator</param>
        /// <param name="bitLength">The length of the random BigInteger returned.</param>
        /// <param name="allowZero">Flag to permit if zero is allowed.</param>
        public static BigInteger RandomOddBitPattern(int bitLength, Random random)
        {
            if (bitLength < 0) throw new ArgumentOutOfRangeException();
            int bits = bitLength;
            if (bits == 0) return BigInteger.One;
            byte[] bytes = new byte[(bits + 7) / 8];
            random.NextBytes(bytes);
            // For the top byte, place a leading 1-bit then downshift to achieve desired length.
            bytes[^1] = (byte)((0x80 | bytes[^1]) >> (7 - (bits - 1) % 8));
            BigInteger result = new BigInteger(bytes, true);
            if (CountSetBits(result) % 2 == 0)
            {
                result = BlockLowestSetBit(result);
                if (result == BigInteger.Zero) result = BigInteger.One;
            }

            return result;
        }


        /// <summary>
        ///     Returns a random BigInteger with a bit count of <paramref name="bitLength" />(inclusive).
        /// </summary>
        /// <param name="random">Random number generator</param>
        /// <param name="bitLength">The length of the random BigInteger returned.</param>
        /// <param name="allowZero">Flag to permit if zero is allowed.</param>
        public static BigInteger RandomEvenBitPattern(int bitLength, Random random, bool allowZero = false)
        {
            if (bitLength < 0) throw new ArgumentOutOfRangeException();
            int bits = bitLength;
            if (bits == 0)
            {
                if (!allowZero) return BigInteger.Zero;
                return new BigInteger(3);
            }

            byte[] bytes = new byte[(bits + 7) / 8];
            random.NextBytes(bytes);
            // For the top byte, place a leading 1-bit then downshift to achieve desired length.
            bytes[^1] = (byte)((0x80 | bytes[^1]) >> (7 - (bits - 1) % 8));
            BigInteger result = new BigInteger(bytes, true);
            if (CountSetBits(result) % 2 != 0)
            {
                result = BlockLowestSetBit(result);
                if (!allowZero && result == BigInteger.Zero) result = new BigInteger(3);
            }

            return result;
        }
    }
}