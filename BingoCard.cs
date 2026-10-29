using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Numerics;
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
            for (int i = 0; i < UndabbedCard.GetLength(0) ; i++)
            {
                for(int j = 0; j < UndabbedCard.GetLength(1); j++)
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
            for(int i = 0; i < _undabbedCard.GetLength(0); i++)
            {
                for(int j = 0; j <_undabbedCard.GetLength(1); j++)
                {
                    //Once in Array checking if the space is a free space if so then we change the dabbed card at that location to an X
                    if(_undabbedCard[i,j] == 0)
                    {
                        _dabbedCard[i,j] = " X";

                    }
                    //Putting values from undabbed card into dabbed card as a string instead an Int
                    else
                    {
                        _dabbedCard[i,j] = _undabbedCard[i,j].ToString("D2");
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
            for(int i = 0; i < _dabbedCard.GetLength(0); i++)
            {
                Console.Write("|");
                for(int j = 0; j <_dabbedCard.GetLength(1); j++)
                {
                    Console.Write($"{_dabbedCard[i,j]}|");
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
    }
}
