using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CasinoBingoCLI
{
    public class BingoBall
    {
        public BingoBall(Random random)
        {
            Number = random.Next(1, 76);
        }

        public int Number { get; init; }

        /// <summary>
        /// Prefix bingo ball letter to input integer.
        /// </summary>
        /// <param name="ballValue">The value of ball between 1 and 75</param>
        /// <returns>Interpolated value preceded by a letter from Bingo</returns>
        public string BallNumberToString(int ballValue)
        {
            string letter = ""; 
            if (ballValue > 0 && ballValue < 16)
            {
                letter = "B";
            } else if (ballValue < 31)
            {
                letter = "I";
            } else if (ballValue < 46)
            {
                letter = "N";
            } else if (ballValue < 61)
            {
                letter = "G";
            } else if (ballValue < 76)
            {
                letter = "O";
            } else
            {
                throw new ArgumentException($"Input integer {ballValue} out of valid range: 0-75");
            }
            return $"{letter}{ballValue:D2}"; // string interpolation :D2 is a format specifier
        }

        public void PrintBingoBallToScreen()
        {
            Console.WriteLine($"Next number: {BallNumberToString(Number)}");
        }
    }
}
