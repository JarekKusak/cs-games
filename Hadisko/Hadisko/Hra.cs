using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hadisko
{
    class Hra
    {
        private Tabulka tabulka;
        private Had had;
        private Jablko jablko;
        private ConsoleKeyInfo c; // třída s potřebnými metodami pro kontrolu vstupu uživatele
        private bool hraStaleBezi;
        private bool pokracovat;        
        private char tah;
        private int doba;

        public Hra(Tabulka tabulka, int doba)
        {
            c = new ConsoleKeyInfo();
            pokracovat = true;
            this.tabulka = tabulka;           
            this.doba = doba;
        }

        public void Hrej() // metoda spustí hru 
        {
            while (pokracovat)
            {
                Console.Clear();                               
                // založení nových objektů                
                jablko = new Jablko(tabulka, 'O');
                had = new Had(tabulka, jablko, 'X', 'x');
                // začátek hry 
                tabulka.VypisTabulku();
                hraStaleBezi = true;
                had.VypisHada(); // vypíše hlavu hada na původní místo
                jablko.VytvorPrvniJablko(had.n, had.m); // vytvoří prní jablko 
                Console.SetCursorPosition(1, tabulka.delka);
                tah = Console.ReadKey().KeyChar; // požadavek na zahájení tahu 
                while (hraStaleBezi)
                {
                    Tah();
                }
                Otazka();
                Console.Clear();
            }
        }
        public void Tah()
        {
            while (hraStaleBezi)
            {             
                Console.SetCursorPosition(1, tabulka.delka);
                switch (tah)
                {
                    case 'w': // směr nahoru                                               
                        do
                        {
                            while (Console.KeyAvailable == false) // nerušeně zaznamenává vstup uživatele
                            {                                
                                had.JdiNahoru();
                                if (Konec() == false) // kontrola konce hry (kolize hada s tělem)
                                    break; // break na ukončení dalšího tahu (cyklu)
                                Thread.Sleep(doba);
                                
                            }
                            if (Konec() == true)
                                c = Console.ReadKey(true); 
                            else break; // break na ukončení cyklu                               
                        }
                        while (c.Key != ConsoleKey.A && c.Key != ConsoleKey.D);
                        tah = c.KeyChar; // uloží poslední zaznamenaný vstup uživatele
                        break;
                    case 's': // směr dolů
                        do
                        {
                            while (Console.KeyAvailable == false) // nerušeně zaznamenává vstup uživatele
                            {
                                had.JdiDolu();
                                if (Konec() == false)
                                    break;
                                Thread.Sleep(doba); 
                              
                            }
                            if (Konec() == true)
                                c = Console.ReadKey(true);
                            else break;
                        }
                        while (c.Key != ConsoleKey.A && c.Key != ConsoleKey.D);
                        tah = c.KeyChar;
                        break;
                    case 'a': // směr doleva
                        do
                        {
                            while (Console.KeyAvailable == false) // nerušeně zaznamenává vstup uživatele
                            {
                                had.JdiDoleva();
                                if (Konec() == false)
                                    break;
                                Thread.Sleep(doba);
                               
                            }
                            if (Konec() == true)
                                c = Console.ReadKey(true);
                            else break;
                        }
                        while (c.Key != ConsoleKey.W && c.Key != ConsoleKey.S);
                        tah = c.KeyChar;
                        break;
                    case 'd': // směr doprava 
                        do
                        {
                            while (Console.KeyAvailable == false) // nerušeně zaznamenává vstup uživatele
                            {
                                had.JdiDoprava();
                                if (Konec() == false)
                                    break;
                                Thread.Sleep(doba);                                
                            }
                            if (Konec() == true)
                                c = Console.ReadKey(true);
                            else break;
                        }
                        while (c.Key != ConsoleKey.W && c.Key != ConsoleKey.S);
                        tah = c.KeyChar;
                        break;
                    default: // pro případ, že hráč při prvním tahu zadá špatnou klávesu
                        string s = "Neplatná volba, zadejte prosím [W/S/A/D]";
                        Console.WriteLine(s);
                        tah = Console.ReadKey().KeyChar;
                        Console.SetCursorPosition(1, tabulka.delka);
                        Console.Write(new string(' ', Console.WindowWidth)); // vymaže zprávu
                        break;
                }
            }
        }

        public void Otazka() // metoda se zeptá hráče na založení nové hry
        {
            Console.SetCursorPosition(1, tabulka.delka);
            Console.WriteLine("Přejete si založit novou hru? [ano/ne]");
            bool platnaVolba = false;
            while (!platnaVolba)
            {
                switch (Console.ReadLine().ToString().ToLower())
                {
                    case "ano":
                        pokracovat = true;
                        platnaVolba = true;
                        break;
                    case "ne":
                        pokracovat = false;
                        platnaVolba = true;
                        break;
                    default:
                        Console.WriteLine("Neplatná volba, zadejte prosím [ano/ne]");
                        break;
                }
            }        
        }
        public bool Konec() // metoda na ověřování eventuální kolize hada a těla => konce hry 
        {           
            hraStaleBezi = had.ZkontrolujKoliziHlavy(); ;
            return hraStaleBezi;
        }
    }
}
