using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Game
    {
        private Table table;
        private Snake snake;
        private Apple apple;
        private ConsoleKeyInfo c; // třída s potřebnými metodami pro kontrolu vstupu uživatele
        private bool gameStillGoing;
        private bool playMore;
        private char move;
        private int doba;

        public Game(Table tabulka, int doba)
        {
            c = new ConsoleKeyInfo();
            playMore = true;
            this.table = tabulka;
            this.doba = doba;
        }

        public bool Play() // metoda spustí hru 
        {
            Console.Clear();
            Console.CursorVisible = false;
            // založení nových objektů                
            apple = new Apple(table, 'O');
            snake = new Snake(table, apple, 'X', 'x');
            // začátek hry 
            table.TableOutput();
            gameStillGoing = true;
             
            Console.SetCursorPosition(1, table.Length);
            move = Console.ReadKey().KeyChar; // požadavek na zahájení tahu 
            while (gameStillGoing)
            {
                //Tah();
            }
            playMore = Question();
            Console.Clear();
            return playMore;
        }
        public bool Question() // metoda se zeptá hráče na založení nové hry
        {
            Console.SetCursorPosition(1, table.Length);
            Console.WriteLine("Přejete si založit novou hru? [ano/ne]");
            bool validOption = false;
            bool pokracovat = true;
            while (!validOption)
            {
                switch (Console.ReadLine().ToString().ToLower())
                {
                    case "ano":
                        pokracovat = true;
                        validOption = true;
                        break;
                    case "ne":
                        pokracovat = false;
                        validOption = true;
                        break;
                    default:
                        Console.WriteLine("Neplatná volba, zadejte prosím [ano/ne]");
                        break;
                }
            }
            return pokracovat;
        }
        public bool End() // metoda na ověřování eventuální kolize hada a těla => konce hry 
        {
            //gameStillGoing = snake.CheckCollision(); ;
            return gameStillGoing;
        }
    }
}
