using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadisko
{
    class Jablko
    {
        private Tabulka tabulka;
        private Random random;
        private char jablicko;
        private int x;
        private int y;
        private int body; 

        public Jablko(Tabulka tabulka, char jablicko)
        {
            this.tabulka = tabulka;
            this.jablicko = jablicko;
            random = new Random();
        }

        public void ZapisSouradniceJablka() // metoda vygeneruje náhodné souřadnice jablka v tabulce 
        {
            x = random.Next(1, tabulka.delka - 1);
            y = random.Next(1, tabulka.delka - 1);
        }
        public bool ZkontrolujStavJablka(int[] sx, int[] sy, int cuntr,int m, int n) // kontroluje, zda nedošlo ke kolizi souřadnic hlavy hada a jablka 
        {
            if (x == n && y == m)
            {               
                PrictiBody();
                return true; 
            }
            return false;
                
        }

        public void PrictiBody() // sčítá skóre
        {
            body = body + 10;
            Console.SetCursorPosition(58, 11);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Skóre: {0}", body);
            Console.ResetColor();
        }

        public void VytvorJablko(int[] sx, int[] sy, int cuntr, int m, int n) // vypíše jablko na místo určené souřadnicemi x,y
        {
            ZapisSouradniceJablka();
            if (x == n && y == m) // ošetřená výjímka při vytvoření jablka na místě hlavy hada 
            {
                ZapisSouradniceJablka();
            }
            bool semaf = true;
            
            while (semaf) 
            {
                for (int i = 0; i < cuntr; i++)
                {
                    if (sx[i] == x && sy[i] == y)
                    {
                        ZapisSouradniceJablka();
                    }
                }
                int pocitac = 0; 
                for (int i = 0; i < cuntr; i++)
                {
                    if (sx[i] == x && sy[i] == y)
                    {
                        semaf = true; 
                    }
                    else pocitac++;
                }
                if (pocitac == cuntr)
                    semaf = false; 
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(2*x, y);
            Console.WriteLine(jablicko);
            Console.ResetColor();
        }

        public void VytvorPrvniJablko(int n, int m) // vypíše první jablko
        {
            ZapisSouradniceJablka();
            if (x == n && y == m)
            {
                ZapisSouradniceJablka();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(2 * x, y);
            Console.WriteLine(jablicko);
            Console.ResetColor();
        }

    }
}
