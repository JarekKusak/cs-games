using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Table
    {
        public int Length { get; private set; } // délka strany čtvercové tabulky 
        public char Character { get; private set; }
        public bool Obstacles { get; private set; }
        public Tuple<int, int>[] ObstaclesCoordinates = new Tuple<int, int>[12];
        public int CenterOfTable => centerOfTable;
        private char[,] matrix;
        private int centerOfTable;
        private int obstacleCoor = 4;
        private char obstacleChar = '#';

        public Table(int length, bool obstacles)
        {
            Obstacles = obstacles; 
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
            CreateObstacles();
            
            if (Obstacles)
                SetObstacles();
        }

        void CreateObstacles()
        {
            // upper-left corner
            ObstaclesCoordinates[0] = Tuple.Create(obstacleCoor, obstacleCoor);
            ObstaclesCoordinates[1] = Tuple.Create(obstacleCoor + 1, obstacleCoor);
            ObstaclesCoordinates[2] = Tuple.Create(obstacleCoor, obstacleCoor + 1);
            // upper-right corner
            ObstaclesCoordinates[3] = Tuple.Create(Length - obstacleCoor - 1, obstacleCoor);
            ObstaclesCoordinates[4] = Tuple.Create(Length - obstacleCoor - 2, obstacleCoor);
            ObstaclesCoordinates[5] = Tuple.Create(Length - obstacleCoor - 1, obstacleCoor + 1);
            // down-left corner
            ObstaclesCoordinates[6] = Tuple.Create(obstacleCoor, Length - obstacleCoor - 1);
            ObstaclesCoordinates[7] = Tuple.Create(obstacleCoor + 1, Length - obstacleCoor - 1);
            ObstaclesCoordinates[8] = Tuple.Create(obstacleCoor, Length - obstacleCoor - 2);
            // down-right corner
            ObstaclesCoordinates[9] = Tuple.Create(Length - obstacleCoor - 1, Length - obstacleCoor - 1);
            ObstaclesCoordinates[10] = Tuple.Create(Length - obstacleCoor - 2, Length - obstacleCoor - 1);
            ObstaclesCoordinates[11] = Tuple.Create(Length - obstacleCoor - 1, Length - obstacleCoor - 2);
        }

        void SetObstacles()
        {
            foreach (var obstacle in ObstaclesCoordinates)
                matrix[obstacle.Item1, obstacle.Item2] = obstacleChar;
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
