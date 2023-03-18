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
        private int points;

        public Apple(Table tabulka, char apple)
        {
            this.table = tabulka;
            this.apple = apple;
            random = new Random();
        }

        /// <summary>
        /// Random generator of apple coordinates
        /// </summary>
        public void WriteDownAppleCoordinates() // metoda vygeneruje náhodné souřadnice jablka v tabulce 
        {
            x = random.Next(1, table.Length - 1);
            y = random.Next(1, table.Length - 1);
        }
        /// <summary>
        /// If snakes head hit the apple, points are added
        /// </summary>
        /// <param name="m"> x coordinate of snakes head </param>
        /// <param name="n"> y coordinate of snakes head </param>
        /// <returns> did/did not </returns>
        public bool CheckStateOfApple(int m, int n) // kontroluje, zda nedošlo ke kolizi souřadnic hlavy hada a jablka 
        {
            if (x == n && y == m)
            {
                AddPoints();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Counts points and is outputing it on the screen
        /// </summary>
        public void AddPoints() // sčítá skóre
        {
            points = points + 10;
            Console.SetCursorPosition(2*table.Length + 10, table.CenterOfTable);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Skóre: {0}", points);
            Console.ResetColor();
        }
        /// <summary>
        /// Creates an apple 
        /// </summary>
        /// <param name="cx"> array of x coordinates of snake body parts </param>
        /// <param name="cy"> array of y coordinates of snake body parts </param>
        /// <param name="bodyParts"> number of body parts of snake </param>
        /// <param name="m"> x coordinate of snakes head </param>
        /// <param name="n"> y coordinate of snakes head </param>
        public void CreateApple(int[] cx, int[] cy, int bodyParts, int m, int n) // vypíše jablko na místo určené souřadnicemi x,y
        {
            WriteDownAppleCoordinates();
            if (x == n && y == m) // ošetřená výjímka při vytvoření jablka na místě hlavy hada 
            {
                WriteDownAppleCoordinates();
            }
            bool semaf = true;

            while (semaf)
            {
                for (int i = 0; i < bodyParts; i++)
                {
                    if (cx[i] == x && cy[i] == y)
                    {
                        WriteDownAppleCoordinates();
                    }
                }
                int counter = 0;
                for (int i = 0; i < bodyParts; i++)
                {
                    if (cx[i] == x && cy[i] == y)
                    {
                        semaf = true;
                    }
                    else counter++;
                }
                if (counter == bodyParts)
                    semaf = false;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(2 * x, y);
            Console.WriteLine(apple);
            Console.ResetColor();
        }
    }
}
