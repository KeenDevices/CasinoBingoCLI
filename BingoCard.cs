using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace CasinoBingoCLI
{
    public class BingoCard
    {
        private readonly int _serialNumber;
        /// <summary>
        /// _undabbedCard is for archiving and is not in play after creation
        /// </summary>
        private readonly int[,] _undabbedCard = new int[5, 5];
        /// <summary>
        /// _dabbedCard is for tracking matching bingo balls as they're drawn, pattern recogniztion
        /// and the presentation to the user
        /// </summary>
        private string[,] _dabbedCard = new string[5, 5];
        private List<int> _usedNumbers = [];

        public BingoCard()
        {
            _serialNumber = Random.Shared.Next(100000, 1000000);

            MakeUnDabbedCard();
            MakeDabbedCard();
        }

        // This is read-only getter using the lambda operator
        public int[,] UndabbedCard => _undabbedCard;
        public string[,] DabbedCard => _dabbedCard;
        public int SerialNumber => _serialNumber;

        private List<int> usedNumbers => _usedNumbers;

        public void PrintToCLI()
        {
            for (int i = 0; i < UndabbedCard.GetLength(0); i++)
            {
                for (int j = 0; j < UndabbedCard.GetLength(1); j++)
                {
                    Console.Write($"{UndabbedCard[i, j]},");
                    if (j == 4)
                    {
                        Console.WriteLine($"{Environment.NewLine}");
                    }
                }
            }

        }

        private void MakeDabbedCard()
        {
            //Going through the undabbed array 
            for (int i = 0; i < UndabbedCard.GetLength(0); i++)
            {
                for (int j = 0; j < UndabbedCard.GetLength(1); j++)
                {
                    //Once in Array checking if the space is a free space if so then we change the dabbed card at that location to an X
                    if (UndabbedCard[i, j] == 0)
                    {
                        DabbedCard[i, j] = " X";

                    }
                    //Putting values from undabbed card into dabbed card as a string instead an Int
                    else
                    {
                        DabbedCard[i, j] = UndabbedCard[i, j].ToString("D2");
                    }
                }
            }
        }

        private void MakeUnDabbedCard()
        {
            int least = 1;
            int most = 16;
            for (int j = 0; j < UndabbedCard.GetLength(1); j++)
            {
                // B = 0, I = 1, N = 2, G = 3, O = 4
                for (int i = 0; i < UndabbedCard.GetLength(0); i++)
                {
                    do
                    {
                        int num = Random.Shared.Next(least, most);
                        if (i == 2 && j == 2) // [2,2] is the free space in the middle of a [5,5] grid
                        {
                            UndabbedCard[i, j] = 0; // assign it 0
                            break;
                        }
                        else
                        {
                            if (!usedNumbers.Contains(num))
                            {
                                UndabbedCard[i, j] = num;
                                usedNumbers.Add(num);
                                break;
                            }
                        }
                    }
                    while (true);
                }
                least += 15;
                most += 15;
                usedNumbers.Clear();
            }
        }

        public void PrintDabbedCardToCLI()
        {
            PrintDabbedCardHeader();

            //Printing out the dabbed card 
            for (int i = 0; i < DabbedCard.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < DabbedCard.GetLength(1); j++)
                {
                    Console.Write($"{DabbedCard[i, j]}|");
                }
                Console.Write(Environment.NewLine);
            }
        }

        // This could return a string
        private void PrintDabbedCardHeader()
        {
            string header = " B  I  N  G  O";
            Console.WriteLine(header);
        }

        public int?[] FindNumber(BingoBall ball)
        {

            // Your solution here.
            int? row = null;
            int column;

            if (ball.Number > 0 && ball.Number < 16)
            {
                column = 0;
            }
            else if (ball.Number < 31)
            {
                column = 1;
            }
            else if (ball.Number < 46)
            {
                column = 2;
            }
            else if (ball.Number < 61)
            {
                column = 3;
            }
            else if (ball.Number < 71)
            {
                column = 4;
            }
            else
            {
                throw new ArgumentException("Error, invalid value for the bingo card.");
            }
            for (int i = 0; i < 5; i++)
            {
                if (UndabbedCard[i, column] == ball.Number)
                {
                    row = i;

                }
                break;

            }
            int?[] arrHolder = [row, column];
            return arrHolder;
        }

        private void MarkXDabbedCard(BingoBall ball)
        {
            int?[] location = FindNumber(ball);
            if (location[0] == null)
            {
                return;
            }
            else
            {
                DabbedCard[location[0].GetValueOrDefault(), location[1].GetValueOrDefault()] = "X";
            }

        }
        private void PatternRecognizer(BingoBall ball)
        {
            //Go through undabbed card to recognize patterns. 
            List<int[]> possibleBingo = new List<int[]>();

            int?[] location = FindNumber(ball);
            if (location[0] == null)
            {

                return;
            }

            else {
                int row = location[0].GetValueOrDefault();
                int column = location[1].GetValueOrDefault();
                for (int i = 0; i < 5; i++)
                {
                    possibleBingo.Add(new int[] { row, i });
                    possibleBingo.Add(new int[] { i, column });

                }
            
            }
        }
    }
}

