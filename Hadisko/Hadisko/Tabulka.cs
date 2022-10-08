using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadisko
{
    class Tabulka
    {
        public int delka; // délka strany čtvercové tabulky 
        public char[,] pole;
        public char znak;

        public Tabulka(int delka, char znak)
        {
            this.znak = znak;
            this.delka = delka;
            pole = new char[delka, delka];
            
            for (int i = 0; i< delka; i++)
            {
                for (int j = 0; j < delka; j++)
                {
                    pole[i, j] = znak;
                }
            }
            // nastaví linky na své místa 
            for (int i = 0; i < delka; i++)
                pole[i, 0] = '-';
            for (int i = 0; i < delka; i++)
                pole[i, delka-1] = '-';
            for (int j = 0; j < delka; j++)
                pole[0, j] = '|';
            for (int j = 0; j < delka; j++)
                pole[delka-1, j] = '|';
            pole[0, 0] = '+';
            pole[delka-1, 0] = '+';
            pole[0, delka-1] = '+';
            pole[delka-1, delka-1] = '+';
        }
        public void VypisTabulku() // vypíše tabulku 
        {
            for (int j = 0; j < delka; j++)
            {
                for (int i = 0; i < delka; i++)
                {
                    if (i == delka - 1)
                    {
                        Console.Write("{0}\n", pole[i,j]);
                        break;
                    }
                    Console.Write("{0} ", pole[i, j]);
                }
            }
        }
    }
}
