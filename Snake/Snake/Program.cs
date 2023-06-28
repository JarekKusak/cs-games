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
            Game game = new Game(table);
    
            game.StartupMenu();

            Console.Clear();
            Console.WriteLine("Hru ukončíte stisknutím jakéhokoliv tlačítka...");
            Console.ReadLine();
        }
    }
}
