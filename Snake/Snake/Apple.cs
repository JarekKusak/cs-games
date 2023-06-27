using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Apple
    {
        private Table table;
        private Random random;
        private char apple;
        private int x;
        private int y;
        public int Points { get; private set; }

        public Apple(Table tabulka, char apple)
        {
            this.table = tabulka;
            this.apple = apple;
            random = new Random();
        }

        /// <summary>
        /// Random generator of apple coordinates
        /// </summary>
        public void WriteDownAppleCoordinates()
        {
            x = random.Next(1, table.Length - 1);
            y = random.Next(1, table.Length - 1);
        }
        /// <summary>
        /// If snakes head hit the apple, points are added
        /// </summary>
        /// <param name="headX"> x coordinate of snakes head </param>
        /// <param name="headY"> y coordinate of snakes head </param>
        /// <returns> did/did not </returns>
        public bool CheckStateOfApple(int headY, int headX) 
        {
            if (x == headX && y == headY)
            {
                AddPoints();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Counts points and is outputing it on the screen
        /// </summary>
        public void AddPoints()
        {
            Points = Points + 10;
            OutputScore();
        }
        /// <summary>
        /// Creates an apple 
        /// </summary>
        /// <param name="coordinatesX"> array of x coordinates of snake body parts </param>
        /// <param name="coordinatesY"> array of y coordinates of snake body parts </param>
        /// <param name="bodyParts"> number of body parts of snake </param>
        /// <param name="headY"> x coordinate of snakes head </param>
        /// <param name="headX"> y coordinate of snakes head </param>
        public void CreateApple(int[] coordinatesX, int[] coordinatesY, int bodyParts, int headY, int headX)
        {
            WriteDownAppleCoordinates();
            if (x == headX && y == headY) // exception for creating an apple in place of snake's head
                WriteDownAppleCoordinates();
            bool needToGenerateNewCoordinates = true;

            while (needToGenerateNewCoordinates)
            {
                for (int i = 0; i < bodyParts; i++)
                    if (coordinatesX[i] == x && coordinatesY[i] == y)
                        WriteDownAppleCoordinates();      
                int counter = 0;
                for (int i = 0; i < bodyParts; i++)
                {
                    if (coordinatesX[i] == x && coordinatesY[i] == y)
                        needToGenerateNewCoordinates = true; 
                    else counter++;
                }
                if (counter == bodyParts)
                    needToGenerateNewCoordinates = false;
            }
            OutputApple(x,y);
            
        }

        void OutputApple(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(2 * x, y);
            Console.WriteLine(apple);
            Console.ResetColor();
        }

        void OutputScore()
        {
            Console.SetCursorPosition(2 * table.Length + 10, table.CenterOfTable);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Skóre: {0}", Points);
            Console.ResetColor();
        }
    }
}