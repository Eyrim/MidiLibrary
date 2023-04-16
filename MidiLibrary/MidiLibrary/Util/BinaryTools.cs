using System;

namespace MidiLibrary.Util
{
    public static class BinaryTools
    {
        /// <summary>
        /// <para>Converts a long to a binary number, represented as an integer array where 1 represents a binary 1.</para>
        /// <para>Array is filled in from the end. Therefore the MSB is the last element.</para>
        /// <para>This method will use the fewest bytes possible to encode the given number</para>
        /// </summary>
        /// <param name="number">The number to convert</param>
        /// <returns>Integer array of 1/0's representing the binary equivalent of <code>number</code></returns>
        public static int[] LongToBinary(long number)
        {
            long minimumBytes = CalculateMinimumBytes(number);
            // The number of bits in all of the bytes
            long bitCount = minimumBytes * 8;
            // Create the right amount of bytes as an array
            int[] binary = new int[bitCount];
            // Find the largest value 
            long binValue = (long) Math.Pow(2, bitCount) / 2;
            // Fill in from end of the array first (end = MSB)
            int offset = 1;

            while (number != 0)
            {
                if (number >= binValue)
                {
                    binary[^offset] = 1;
                    number -= binValue;
                }

                // Move to next bit
                offset++;
                binValue /= 2;
            }

            return binary;
        }

        /// <summary>
        /// <para>Calculates the minimum number of bytes required to store a specified number</para>
        /// <para>
        /// Equation used:
        /// <code>Math.Ceiling(Math.Log2(number) / 8)</code>
        /// </para>
        /// </summary>
        /// <param name="number">The number to calculate the smallest number of bytes for</param>
        /// <returns>The smallest number of bytes to store a given number</returns>
        private static long CalculateMinimumBytes(long number) => (long) Math.Ceiling(Math.Log2(number) / 8);
    }
}