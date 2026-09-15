using System;
using System.Collections.Generic;
using System.Text;

namespace CasinoBingoCLI
{
    public static class Game
    {
        static private Random _random = new();
        static private readonly List<BingoCard> _bingoCardCollection = new List<BingoCard>();

        public static List<BingoCard> BingoCardCollection => _bingoCardCollection;

        static public void Initialize()
        {
            // int numCardsToMake = 3;
            for (int i = 0; i < 1; i++)
            {
                BingoCardCollection.Add(new BingoCard(_random));
            }
        }
        static public void PrintBingoCardToScreen()
        {
            while (true)
            {
                foreach (BingoCard card in BingoCardCollection)
                {
                    for (int i = 0; i < card.NumbersMatrix.GetLength(0); i++)
                    {
                        Console.Write("|");
                        for (int j = 0; j < card.NumbersMatrix.GetLength(1); j++)
                        {
                            if (i == 2 && j == 2)
                            {
                                Console.Write("FF|");
                            }
                            else
                            {
                                Console.Write($"{card.NumbersMatrix[i, j].ToString("D2")}|");
                            }
                        }
                        Console.WriteLine(Environment.NewLine);
                    }
                    Console.WriteLine(Environment.NewLine);
                }
                break;
            }
        }

        static public void Run()
        {
            Game.PrintBingoCardToScreen();
        }
    }
}
