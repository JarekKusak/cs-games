using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Table
    {
        public int length; // délka strany čtvercové tabulky 
        public char[,] matrix;
        public char character;

        public Table(int length, char character)
        {
            this.character = character;
            this.length = length;
            matrix = new char[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    matrix[i, j] = character;
                }
            }
            // nastaví linky na své místa 
            for (int i = 0; i < length; i++)
                matrix[i, 0] = '-';
            for (int i = 0; i < length; i++)
                matrix[i, length - 1] = '-';
            for (int j = 0; j < length; j++)
                matrix[0, j] = '|';
            for (int j = 0; j < length; j++)
                matrix[length - 1, j] = '|';
            matrix[0, 0] = '+';
            matrix[length - 1, 0] = '+';
            matrix[0, length - 1] = '+';
            matrix[length - 1, length - 1] = '+';
        }
        public void TableOutput() // vypíše tabulku 
        {
            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < length; i++)
                {
                    if (i == length - 1)
                    {
                        Console.Write("{0}\n", matrix[i, j]);
                        break;
                    }
                    Console.Write("{0} ", matrix[i, j]);
                }
            }
        }
    }
}
