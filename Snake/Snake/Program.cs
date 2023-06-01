using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            // initalization of table
            Table table = new Table(25, ' ');
            bool wantToPlay = true;
            int delay = 0;
            Console.WriteLine("Vítejte ve hře HADISKO!\n");
            while (wantToPlay)
            {
                bool validOption = false;
                Console.WriteLine("Ovládat hada můžete pomocí tlačítek [W/S/A/D].");
                Console.WriteLine("Zadejte obtížnost ve formátu [easy/normal/hard/extreme]: ");

                while (!validOption)
                {
                    switch (Console.ReadLine().ToString().ToLower())
                    {
                        case "easy":
                            delay = 100;
                            validOption = true;
                            break;
                        case "normal":
                            delay = 75;
                            validOption = true;
                            break;
                        case "hard":
                            delay = 50;
                            validOption = true;
                            break;
                        case "extreme":
                            delay = 25;
                            validOption = true;
                            break;
                        default:
                            validOption = false;
                            Console.WriteLine("Neplatná volba, zadejte prosím [easy/normal/hard/extreme]");
                            break;
                    }
                }
                Game game = new Game(table, delay);
                wantToPlay = game.Play();
            }

            Console.WriteLine("Hru ukončíte stisknutím jakéhokoliv tlačítka...");
            Console.ReadLine();
        }
    }
}
