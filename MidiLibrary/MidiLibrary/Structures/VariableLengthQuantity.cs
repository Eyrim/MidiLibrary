using System;
using MidiLibrary.Util;

namespace MidiLibrary.Structures
{
    /// <summary>
    /// Represents a variable length quantity
    /// </summary>
    public readonly struct VariableLengthQuantity
    //TODO: Override ToString
    {
        /// <summary>
        /// Binary representation of the input number up to 64 bit signed limits
        /// </summary>
        private readonly bool[][] Number { get; init; }

        /// <summary>
        /// Constructs a new variable length quantity variable
        /// </summary>
        /// <param name="number">The signed 64 bit integer to represent</param>
        /// <exception cref="NotImplementedException"></exception>
        private VariableLengthQuantity(long number)
        {
            this.Number = FromLong(number);
        }
        
        /// <summary>
        /// Explicitly converts a long to VariableLengthQuantity
        /// </summary>
        /// <param name="number">The signed 64 bit integer to convert</param>
        /// <returns>VariableLengthQuantity</returns>
        public static explicit operator VariableLengthQuantity(long number) => new VariableLengthQuantity(number);

        public static explicit operator long(VariableLengthQuantity vlq) => ToLong(vlq);

        /// <summary>
        /// Converts the input long to a variable length quantity
        /// </summary>
        /// <param name="number">The long to convert</param>
        /// <returns>Variable Length Quantity</returns>
        /// <exception cref="NotImplementedException"></exception>
        private static bool[][] FromLong(long number)
        {
            bool[] binaryRep = BinaryTools.LongToBinary(number);
            bool[][] sevenBitNumbers = BinaryTools.BinaryToSevenBitNumbers(binaryRep);
            // Reverse the array
            Array.Reverse(sevenBitNumbers, 0, sevenBitNumbers.Length);
            
            for (int i = 0; i < sevenBitNumbers.Length; i++)
            {
                sevenBitNumbers[i] = BinaryTools.PadBinary(sevenBitNumbers[i], 8);
                // everything that isn't next, has a 1 (true) as MSB
                sevenBitNumbers[i][^1] = true;
            }

            sevenBitNumbers[^1][^1] = false;

            return sevenBitNumbers;
        }

        /// <summary>
        /// Converts the input variable length quantity to long
        /// </summary>
        /// <param name="array">The variable length quantity to convert</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static long ToLong(VariableLengthQuantity vlq)
        {
            // NOTE: This method is very similar to BinaryTools.BinaryToLong()
            // TODO: Consider merging them
            long total = 0;
            long currentBitValue = 1;

            // Reverse the array
            Array.Reverse(vlq.Number, 0, vlq.Number.Length);
            
            for (int i = 0; i < vlq.Number.Length; i++)
            {
                // Don't evaluate the last bit as it holds no value
                    // Used only as a "null" terminator
                for (int j = 0; j < vlq.Number[i].Length - 1; j++)
                {
                    if (vlq.Number[i][j])
                    {
                        total += currentBitValue;
                    }

                    currentBitValue *= 2;
                }
            }

            return total;
        }
    }
}