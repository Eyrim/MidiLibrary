using System;

namespace MidiLibrary.Util
{
    /// <summary>
    /// Static class to provide various utility methods for interacting with binary
    /// </summary>
    public static class BinaryTools
    {
        /// <summary>
        /// <para>Converts a long to a binary number, represented as an integer array where 1 represents a binary 1.</para>
        /// <para>Array is filled in from the end. Therefore the MSB is the last element.</para>
        /// <para>This method will use the fewest bytes possible to encode the given number</para>
        /// </summary>
        /// <param name="number">The number to convert</param>
        /// <returns>Integer array of 1/0's representing the binary equivalent of <code>number</code></returns>
        public static bool[] LongToBinary(long number)
        {
            long minimumBytes = CalculateMinimumBytes(number);
            // The number of bits in all of the bytes
            long bitCount = minimumBytes * 8;
            // Create the right amount of bytes as an array
            bool[] binary = new bool[bitCount];
            // Find the largest value 
            long binValue = (long) Math.Pow(2, bitCount) / 2;
            // Fill in from end of the array first (end = MSB)
            int offset = 1;

            while (number != 0)
            {
                if (number >= binValue)
                {
                    binary[^offset] = true;
                    number -= binValue;
                }

                // Move to next bit
                offset++;
                binValue /= 2;
            }

            return binary;
        }
        
        /// <summary>
        /// Converts a binary input to a long
        /// </summary>
        /// <param name="binary">The binary array to convert</param>
        /// <returns>The long representation of the array</returns>
        public static long BinaryToLong(bool[] binary)
        {
            long total = 0;
            long currentBitValue = 1;

            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i])
                {
                    total += currentBitValue;
                }

                currentBitValue *= 2;
            }

            return total;
        }

        /// <summary>
        /// Converts a given binary input to seven bit numbers
        /// </summary>
        /// <param name="binary">The binary to convert</param>
        /// <returns>An array of seven bit numbers</returns>
        public static bool[][] BinaryToSevenBitNumbers(bool[] binary)
        {
            long requiredBytes = CalculateNumberOfSevenBitGroups(binary.Length);
            bool[][] allNumbers = new bool[requiredBytes][];
            int counter = 0;

            for (int i = 0; i < requiredBytes * 7; i += 7)
            {
                if ((i + 7) > binary.Length - 1)
                {
                    allNumbers[counter] = PadBinary(binary[i..binary.Length], 7);
                }
                else
                {
                    allNumbers[counter] = binary[i..(i + 7)];
                }

                counter++;
            }

            Array.Reverse(allNumbers, 0, allNumbers.Length);

            return allNumbers;
        }

        /// <summary>
        /// Converts the input array to an array of len length
        /// </summary>
        /// <param name="binary">The binary to convert</param>
        /// <param name="len">The length of the new array</param>
        /// <returns>New array with right padding</returns>
        private static bool[] PadBinary(bool[] binary, int len)
        {
            bool[] newBinary = new bool[len];

            binary.CopyTo(newBinary, 0);

            return newBinary;
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
        // Credit: https://github.com/ily-cloudy
        private static long CalculateMinimumBytes(long number) => (long) Math.Ceiling(Math.Log2(number) / 8);

        /// <summary>
        /// <para>Calculates the number of seven bit groups required to store a specific number</para>
        /// <para>
        /// Equation used:
        /// <code>Math.Ceiling((8 * numberOfBytes) / 7)</code>
        /// </para>
        /// </summary>
        /// <param name="numberOfBits">The number of bits</param>
        /// <returns>The number of seven bit groups</returns>
        // Credit: https://github.com/ily-cloudy
        private static long CalculateNumberOfSevenBitGroups(long numberOfBits) => (long) Math.Ceiling((double) numberOfBits / 7);
    }
}