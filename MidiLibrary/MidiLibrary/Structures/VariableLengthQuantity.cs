using System;
using System.Collections.Generic;
using System.Text;
using MidiLibrary.Util;

namespace MidiLibrary.Structures
{
    /// <summary>
    /// Represents a variable length quantity
    /// </summary>
    public readonly struct VariableLengthQuantity
    {
        /// <summary>
        /// Binary representation of the input number up to 64 bit signed limits
        /// </summary>
        public readonly byte[] Number { get; init; }

        /// <summary>
        /// Constructs a new variable length quantity variable
        /// </summary>
        /// <param name="number">The signed 64 bit integer to represent</param>
        /// <exception cref="NotImplementedException"></exception>
        public VariableLengthQuantity(long number)
        {
            bool[] binaryRep = BinaryTools.LongToBinary(number);
            bool[][] sevenBitNumbers = BinaryTools.BinaryToSevenBitNumbers(binaryRep);

            throw new Exception();
        }
        
        /// <summary>
        /// Explicitly converts a long to VariableLengthQuantity
        /// </summary>
        /// <param name="number">The signed 64 bit integer to convert</param>
        /// <returns>VariableLengthQuantity</returns>
        public static explicit operator VariableLengthQuantity(long number) => new VariableLengthQuantity(number);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (byte b in this.Number)
            {
                sb.Append(b);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts the input long to a variable length quantity
        /// </summary>
        /// <param name="number">The long to convert</param>
        /// <returns>Variable Length Quantity</returns>
        /// <exception cref="NotImplementedException"></exception>
        private static byte[] FromLong(long number)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the input variable length quantity to long
        /// </summary>
        /// <param name="array">The variable length quantity to convert</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static long ToLong(byte[] array)
        {
            throw new NotImplementedException();
        }
    }
}