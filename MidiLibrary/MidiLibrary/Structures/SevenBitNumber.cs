using System;
using System.Runtime.CompilerServices;

namespace MidiLibrary.Structures
{
    /// <summary>
    /// Represents a seven bit number, MSB is reserved and therefore the number must be lower than 127
    /// </summary>
    public readonly struct SevenBitNumber
    {
        /// <summary>
        /// 7 bit representation of the input byte
        /// </summary>
        private readonly byte Number { get; init; }

        /// <summary>
        /// Constructs a new seven bit number 
        /// </summary>
        /// <param name="number">The number to encode</param>
        /// <exception cref="ArgumentException">Thrown if number is greater than 127</exception>
        public SevenBitNumber(byte number)
        {
            // If number is greater than 127
            if (!ValidateByte(number))
                throw new ArgumentException($"{number} is greater than 127");

            this.Number = number;
        }

        /// <summary>
        /// Explicitly converts a byte to a seven bit number
        /// </summary>
        /// <param name="number">The byte to convert</param>
        /// <returns>SevenBitNumber</returns>
        public static explicit operator SevenBitNumber(byte number) => new SevenBitNumber(number);

        public override string ToString() => $"{Number.ToString()}";

        /// <summary>
        /// Validates input byte
        /// </summary>
        /// <param name="toValidate">The byte to validate</param>
        /// <returns>
        /// <para>True - If byte is less than 128</para>
        /// <para>False - If byte is greater than 127</para>
        /// </returns>
        //TODO: rename
        private static bool ValidateByte(byte toValidate)
        {
            return toValidate < 127;
        }
    }
}