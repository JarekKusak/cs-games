using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Table
    {
        public int Length { get; set; } // délka strany čtvercové tabulky 
        public char Character { get; set; }
        public int CenterOfTable => centerOfTable;
        private char[,] matrix;
        private int centerOfTable;
        private bool wantObstacles;
        private int obstacleCoor = 4;
        private char obstacleChar = '#';

        public Table(int length, bool obstacles)
        {
            wantObstacles = obstacles; 
            Character = ' ';
            Length = length;
            matrix = new char[length, length];
            centerOfTable = (length + 1) / 2 - 1;
            for (int i = 0; i < length; i++)       
                for (int j = 0; j < length; j++)             
                    matrix[i, j] = Character;             
            // edges
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
            
            if (obstacles)
                SetObstacles();
        }

        public void SetObstacles()
        {
            // obstacles
            matrix[obstacleCoor, obstacleCoor] = obstacleChar;
            matrix[Length - obstacleCoor - 1, obstacleCoor] = obstacleChar;
            matrix[obstacleCoor, Length - obstacleCoor - 1] = obstacleChar;
            matrix[Length - obstacleCoor - 1, Length - obstacleCoor - 1] = obstacleChar;
        }

        /// <summary>
        /// Outputs table
        /// </summary>
        public void TableOutput()
        {
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < Length; j++)
            {
                for (int i = 0; i < Length; i++)
                {
                    if (i == Length - 1)
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
