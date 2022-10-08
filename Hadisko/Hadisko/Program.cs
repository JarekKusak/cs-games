using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadisko
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // nastavení objektů 
            Tabulka tabulka = new Tabulka(23, ' ');
            Console.WriteLine("Vítejte ve hře HADISKO!\n");
            Console.WriteLine("Ovládat hada můžete pomocí tlačítek [W/S/A/D].");
            Console.WriteLine("Zadejte obtížnost ve formátu [easy/normal/hard/extreme]: ");
            bool platnaVolba = false;
            int doba = 0; 
            while (!platnaVolba) // konstrukce na nastavení časové odezvy (úrovně) 
            {
                switch (Console.ReadLine().ToString().ToLower())
                {
                    case "easy":
                        doba = 200;
                        platnaVolba = true;
                        break;
                    case "normal":
                        doba = 150;
                        platnaVolba = true;
                        break;
                    case "hard":
                        doba = 100;
                        platnaVolba = true;
                        break;
                    case "extreme":
                        doba = 50;
                        platnaVolba = true;
                        break;
                    default:
                        Console.WriteLine("Neplatná volba, zadejte prosím [easy/normal/hard/extreme]");
                        break;
                }
            }          
            Hra hra = new Hra(tabulka, doba);            
            // hra
            hra.Hrej();

            Console.WriteLine("Hru ukončíte stisknutím jakéhokoliv tlačítka...");
            Console.ReadLine();
        }
    }
}
