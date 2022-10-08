using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadisko
{
    class Had
    {
        private Tabulka tabulka;
        private Jablko jablko; 
        private char hlava;
        public int n;
        public int m;
        private int[] sx; // pole ukládající souřadnice x 
        private int[] sy; // pole ukládající souřadnice y 
        private int o;
        private int cuntr; // counter všech článků těla (včetně hlavy) + zmizík na konci 
        private char telo;
        private bool kolize;

        public Had(Tabulka tabulka, Jablko jablko, char hlava, char telo)
        {
            sx = new int[(tabulka.delka - 2) * (tabulka.delka - 2)];
            sy = new int[(tabulka.delka-2)* (tabulka.delka - 2)];
            this.telo = telo; 
            this.jablko = jablko;
            this.tabulka = tabulka;
            n = (tabulka.delka + 1) / 2 - 1;
            m = n;
            this.hlava = hlava;
            sx[0] = n;
            sy[0] = m;
            sx[1] = n; // x souř. zmizíku
            sy[1] = m+1; // y souř. zmizíku
            cuntr = 2; // počet článků těla hada - hlava + "zmizík"
            kolize = true;
        }

        public void VypisHada() // vypíše hada na původní místo (do středu)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(2*n, m);
            Console.WriteLine(hlava);
            Console.ResetColor();
        }

        public void JdiNahoru()
        {
            m--; // souřadnice hlavy pro kontrolu  
            if (m == 0) // podmínka na srážku se zdí 
            {
                m = tabulka.delka - 2; // hlava hada se posune "přes zeď" (směrem nahoru)
                if (jablko.ZkontrolujStavJablka(sx, sy, cuntr, m, n) == false) // false -> had se posune 
                {
                    for (o = cuntr-1; o > 0; o--) // o = 1, protože nechceme posouvat hlavu  
                    {
                        sx[o] = sx[o - 1]; 
                        sy[o] = sy[o - 1]; // prohození souřadnic y 
                    }
                    sy[0] = tabulka.delka - 2; //posunutí souřadnice hlavy hada(směrem nahoru)
                    VypisTeloHada();
                }
                else // had sežere jablko a hadisku naroste tělo 
                {
                    for (o = cuntr-1; o > -1; o--)
                    {
                        sx[o + 1] = sx[o];
                        sy[o + 1] = sy[o];
                    }
                    sy[0] = tabulka.delka - 2; // posunutí souřadnice hlavy hada(směrem nahoru)
                    cuntr++; // +1 článek těla 
                    VypisTeloHada();
                    jablko.VytvorJablko(sx, sy, cuntr, m, n);
                }
            }
            else // podmínka mimo zeď 
            {                               
                if (jablko.ZkontrolujStavJablka(sx, sy, cuntr, m, n) == false) // false -> had se posune 
                {
                    for (o = cuntr-1; o > 0; o--) // o = 1, protože nechceme posouvat hlavu  
                    {
                        sx[o] = sx[o-1];
                        sy[o] = sy[o-1];
                    }
                    sy[0] = sy[0] - 1; // posunutí souřadnice hlavy hada(směrem nahoru)
                    VypisTeloHada();
                }
                else // had sežere jablko a hadisku naroste tělo 
                {
                    for (o = cuntr-1; o > -1; o--) 
                    {
                        sx[o + 1] = sx[o];
                        sy[o + 1] = sy[o];
                    }
                    sy[0] = sy[0] - 1; // posunutí souřadnice hlavy hada (směrem nahoru)
                    cuntr++; // +1 článek těla 
                    VypisTeloHada();
                    jablko.VytvorJablko(sx, sy, cuntr, m, n);
                }
            }
        }
        public void JdiDolu()
        {
            m++;
            if (m == tabulka.delka - 1) // podmínka na srážku se zdí 
            {
                m = 1; // hlava hada se posune "přes zeď" 
                if (jablko.ZkontrolujStavJablka(sx, sy, cuntr, m, n) == false) // false -> had se posune 
                {
                    for (o = cuntr - 1; o > 0; o--) // o = 1, protože nechceme posouvat hlavu  
                    {
                        sx[o] = sx[o - 1];
                        sy[o] = sy[o - 1]; // prohození souřadnic y 
                    }
                    sy[0] = 1; //posunutí souřadnice hlavy hada
                    VypisTeloHada();
                }
                else // had sežere jablko a hadisku naroste tělo 
                {
                    for (o = cuntr - 1; o > -1; o--)
                    {
                        sx[o + 1] = sx[o];
                        sy[o + 1] = sy[o];
                    }
                    sy[0] = 1; // posunutí souřadnice hlavy hada 
                    cuntr++; // +1 článek těla 
                    VypisTeloHada();
                    jablko.VytvorJablko(sx, sy, cuntr, m, n);
                }
            }
            else // podmínka mimo zeď 
            {
                if (jablko.ZkontrolujStavJablka(sx, sy, cuntr, m, n) == false) // false -> had se posune 
                {
                    for (o = cuntr - 1; o > 0; o--) // o = 1, protože nechceme posouvat hlavu  
                    {
                        sx[o] = sx[o - 1];
                        sy[o] = sy[o - 1];
                    }
                    sy[0] = sy[0] + 1; // posunutí souřadnice hlavy hada
                    VypisTeloHada();
                }
                else // had sežere jablko a hadisku naroste tělo 
                {
                    for (o = cuntr - 1; o > -1; o--)
                    {
                        sx[o + 1] = sx[o];
                        sy[o + 1] = sy[o];
                    }
                    sy[0] = sy[0] + 1; // posunutí souřadnice hlavy hada 
                    cuntr++; // +1 článek těla 
                    VypisTeloHada();
                    jablko.VytvorJablko(sx, sy, cuntr, m, n);
                }
            }
        }
        public void JdiDoleva()
        {
            n--;
            if (n == 0) // podmínka na srážku se zdí 
            {
                n = tabulka.delka - 2; // hlava hada se posune "přes zeď" (směrem doleva)
                if (jablko.ZkontrolujStavJablka(sx, sy, cuntr, m, n) == false) // false -> had se posune 
                {
                    for (o = cuntr - 1; o > 0; o--) // o = 1, protože nechceme posouvat hlavu  
                    {
                        sx[o] = sx[o - 1];
                        sy[o] = sy[o - 1]; // prohození souřadnic y 
                    }
                    sx[0] = tabulka.delka - 2; //posunutí souřadnice hlavy hada
                    VypisTeloHada();
                }
                else // had sežere jablko a hadisku naroste tělo 
                {
                    for (o = cuntr - 1; o > -1; o--)
                    {
                        sx[o + 1] = sx[o];
                        sy[o + 1] = sy[o];
                    }
                    sx[0] = tabulka.delka - 2; // posunutí souřadnice hlavy hada
                    cuntr++; // +1 článek těla 
                    VypisTeloHada();
                    jablko.VytvorJablko(sx, sy, cuntr, m, n);
                }
            }
            else // podmínka mimo zeď 
            {
                if (jablko.ZkontrolujStavJablka(sx, sy, cuntr, m, n) == false) // false => had se posune 
                {
                    for (o = cuntr - 1; o > 0; o--) // o = 1, protože nechceme posouvat hlavu  
                    {
                        sx[o] = sx[o - 1];
                        sy[o] = sy[o - 1];
                    }
                    sx[0] = sx[0] - 1; // posunutí souřadnice hlavy hada 
                    VypisTeloHada();
                }
                else // had sežere jablko a hadisku naroste tělo 
                {
                    for (o = cuntr - 1; o > -1; o--)
                    {
                        sx[o + 1] = sx[o];
                        sy[o + 1] = sy[o];
                    }
                    sx[0] = sx[0] - 1; // posunutí souřadnice hlavy hada
                    cuntr++; // +1 článek těla 
                    VypisTeloHada();
                    jablko.VytvorJablko(sx, sy, cuntr, m, n);
                }
            }
        }
        public void JdiDoprava()
        {
            n++;
            if (n == tabulka.delka - 1) // podmínka na srážku se zdí 
            {
                n = 1; // hlava hada se posune "přes zeď" 
                if (jablko.ZkontrolujStavJablka(sx, sy, cuntr, m, n) == false) // false -> had se posune 
                {
                    for (o = cuntr - 1; o > 0; o--) // o = 1, protože nechceme posouvat hlavu  
                    {
                        sx[o] = sx[o - 1];
                        sy[o] = sy[o - 1]; // prohození souřadnic y 
                    }
                    sx[0] = 1; //posunutí souřadnice hlavy hada
                    VypisTeloHada();

                }
                else // had sežere jablko a hadisku naroste tělo 
                {
                    for (o = cuntr - 1; o > -1; o--)
                    {
                        sx[o + 1] = sx[o];
                        sy[o + 1] = sy[o];
                    }
                    sx[0] = 1; // posunutí souřadnice hlavy hada 
                    cuntr++; // +1 článek těla 
                    VypisTeloHada();
                    jablko.VytvorJablko(sx, sy, cuntr, m, n);
                }
            }
            else // podmínka mimo zeď 
            {
                if (jablko.ZkontrolujStavJablka(sx, sy, cuntr, m, n) == false) // false -> had se posune 
                {
                    for (o = cuntr - 1; o > 0; o--) // o = 1, protože nechceme posouvat hlavu  
                    {
                        sx[o] = sx[o - 1];
                        sy[o] = sy[o - 1];
                    }
                    sx[0] = sx[0] + 1; // posunutí souřadnice hlavy hada
                    VypisTeloHada();
                }
                else // had sežere jablko a hadisku naroste tělo 
                {
                    for (o = cuntr - 1; o > -1; o--)
                    {
                        sx[o + 1] = sx[o];
                        sy[o + 1] = sy[o];
                    }
                    sx[0] = sx[0] + 1; // posunutí souřadnice hlavy hada 
                    cuntr++; // +1 článek těla 
                    VypisTeloHada();
                    jablko.VytvorJablko(sx, sy, cuntr, m, n);
                }
            }
        }

        public void VypisTeloHada() // vypíše celé tělo hada na již nově nastavené pozice
        {
            Console.SetCursorPosition(2 * sx[0], sy[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(hlava);
        
            for (o = 1; o < cuntr-1; o++)
            {
                Console.SetCursorPosition(2 * sx[o], sy[o]);
                Console.WriteLine(telo);
            }
            
            Console.SetCursorPosition(2 * sx[cuntr - 1], sy[cuntr - 1]);
            Console.WriteLine(tabulka.znak);
            Console.ResetColor();
        }

        public bool ZkontrolujKoliziHlavy() // kontroluje, zda nedošlo ke srážce hlavy s tělem
        {
            for (o = 1; o < cuntr-1; o++) // o = 1, protože hlavu do zbytku těla nepočítáme
            {
                if (n == sx[o] && m == sy[o])
                {
                    kolize = false;
                }
            }           
            return kolize;
        }
    }
}
