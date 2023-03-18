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

        public void WriteDownAppleCoordinates() // metoda vygeneruje náhodné souřadnice jablka v tabulce 
        {
            x = random.Next(1, table.Length - 1);
            y = random.Next(1, table.Length - 1);
        }
        public bool CheckStateOfApple(int m, int n) // kontroluje, zda nedošlo ke kolizi souřadnic hlavy hada a jablka 
        {
            if (x == n && y == m)
            {
                AddPoints();
                return true;
            }
            return false;
        }

        public void AddPoints() // sčítá skóre
        {
            points = points + 10;
            Console.SetCursorPosition(2*table.Length + 10, table.CenterOfTable);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Skóre: {0}", points);
            Console.ResetColor();
        }

        public void CreateApple(int[] cx, int[] cy, int bodySegments, int m, int n) // vypíše jablko na místo určené souřadnicemi x,y
        {
            WriteDownAppleCoordinates();
            if (x == n && y == m) // ošetřená výjímka při vytvoření jablka na místě hlavy hada 
            {
                WriteDownAppleCoordinates();
            }
            bool semaf = true;

            while (semaf)
            {
                for (int i = 0; i < bodySegments; i++)
                {
                    if (cx[i] == x && cy[i] == y)
                    {
                        WriteDownAppleCoordinates();
                    }
                }
                int counter = 0;
                for (int i = 0; i < bodySegments; i++)
                {
                    if (cx[i] == x && cy[i] == y)
                    {
                        semaf = true;
                    }
                    else counter++;
                }
                if (counter == bodySegments)
                    semaf = false;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(2 * x, y);
            Console.WriteLine(apple);
            Console.ResetColor();
        }
    }
}
