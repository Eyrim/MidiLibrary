using System;
using System.Collections.Generic;
using MidiLibrary.Structures;
using MidiLibrary.Util;

namespace MidiLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (bool b in BinaryTools.LongToBinary(106903))
            {
                if (b)
                {
                    Console.Write("1");
                }
                else
                {
                    Console.Write("0");
                }
            }
        }
    }
}
