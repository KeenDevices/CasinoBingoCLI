using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace CasinoBingoCLI
{
    public class BingoCard
    {
        private readonly int _serialNumber;
        private readonly int[,] _numbersMatrix = new int[5, 5];
        private List<int> _usedNumbers = new List<int>();

        public BingoCard(Random random)
        {
            _serialNumber = random.Next(100000, 1000000);
            int least = 1;
            int most = 16;
            for (int j = 0; j < _numbersMatrix.GetLength(1); j++)
            {

                for (int i = 0; i < _numbersMatrix.GetLength(0); i++)
                {
                    do
                    {
                        int num = random.Next(least, most);
                        if (i == 2 && j == 2) // [2,2] is the free space in the middle of a [5,5] grid
                        {
                            _numbersMatrix[i, j] = 0; // assign it 0
                            break; 
                        }
                        else
                        {
                            if (!_usedNumbers.Contains(num))
                            {
                                _numbersMatrix[i, j] = num;
                                _usedNumbers.Add(num);
                                break;
                            }
                        }
                    }
                    while (true);
                }
                least += 15;
                most += 15;
                _usedNumbers.Clear();
            }
        }

        // This is read-only getter using the lambda operator
        public int[,] NumbersMatrix => _numbersMatrix;
        public int SerialNumber => _serialNumber;

        public void PrintToCLI()
        {
            for (int i = 0; i < NumbersMatrix.GetLength(0) ; i++)
            {
                for(int j = 0; j < NumbersMatrix.GetLength(1); j++)
                {
                    Console.Write($"{NumbersMatrix[i, j]},");
                    if (j == 4)
                    {
                        Console.WriteLine($"{Environment.NewLine}");
                    }
                }
            }
            
        }
    }
}
