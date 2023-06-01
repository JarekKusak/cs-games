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
        private ConsoleKeyInfo c;
        private bool gameStillGoing;
        private bool playMore;
        private char move;
        private int delay;

        public Game(Table tabulka, int doba)
        {
            c = new ConsoleKeyInfo();
            playMore = true;
            this.table = tabulka;
            this.delay = doba;
        }

        void SetupNewGame()
        {
            Console.Clear();
            table.TableOutput();
            Console.CursorVisible = false;
            // new objects              
            apple = new Apple(table, 'O');
            snake = new Snake(table, apple, 'X', 'x');
            // start of the game 
            gameStillGoing = true;
            snake.SnakeOutput(); // outputs snake's head on standard place
            Console.SetCursorPosition(0, table.Length);
            move = Console.ReadKey().KeyChar; // demand for entering the move
        }

        /// <summary>
        /// Main method for initializing objects and launching game
        /// </summary>
        /// <returns> Returns true if player wants to play more </returns>
        public bool Play() // main method
        {
            SetupNewGame();
            // main loop
            while (gameStillGoing)
            {
                Move();
            }
            Console.CursorVisible = true;
            playMore = Question();
            Console.Clear();
            return playMore;
        }

        /// <summary>
        /// Method that makes all the moves ;)
        /// </summary>
        public void Move()
        {
            while (gameStillGoing)
            {
                Console.SetCursorPosition(0, table.Length);
                switch (Char.ToLower(move))
                {
                    case 'w': // upwards direction                                               
                        do
                        {
                            while (Console.KeyAvailable == false) // records user input undisturbed
                            {
                                snake.GoUp();
                                if (End() == false) // control for end of the game
                                    break; // break for ending next move (end of cycle)
                                Thread.Sleep(delay);
                            }
                            if (End() == true)
                                c = Console.ReadKey(true);
                            else break;                             
                        }
                        while (c.Key != ConsoleKey.A && c.Key != ConsoleKey.D);
                        move = c.KeyChar; // saves last entered input of player
                        break;
                    case 's': // downwards direction
                        do
                        {
                            while (Console.KeyAvailable == false) // records user input undisturbed
                            {
                                snake.GoDown();
                                if (End() == false)
                                    break;
                                Thread.Sleep(delay);
                            }
                            if (End() == true)
                                c = Console.ReadKey(true);
                            else break;
                        }
                        while (c.Key != ConsoleKey.A && c.Key != ConsoleKey.D);
                        move = c.KeyChar;
                        break;
                    case 'a': // left direction
                        do
                        {
                            while (Console.KeyAvailable == false) // records user input undisturbed
                            {
                                snake.GoLeft();
                                if (End() == false)
                                    break;
                                Thread.Sleep(delay);
                            }
                            if (End() == true)
                                c = Console.ReadKey(true);
                            else break;
                        }
                        while (c.Key != ConsoleKey.W && c.Key != ConsoleKey.S);
                        move = c.KeyChar;
                        break;
                    case 'd': // right direction
                        do
                        {
                            while (Console.KeyAvailable == false) // records user input undisturbed
                            {
                                snake.GoRight();
                                if (End() == false)
                                    break;
                                Thread.Sleep(delay);
                            }
                            if (End() == true)
                                c = Console.ReadKey(true);
                            else break;
                        }
                        while (c.Key != ConsoleKey.W && c.Key != ConsoleKey.S);
                        move = c.KeyChar;
                        break;
                    default: // if player enters wrong key for the first time
                        Console.WriteLine("Neplatná volba, zadejte prosím [W/S/A/D]");
                        move = Console.ReadKey().KeyChar;
                        Console.SetCursorPosition(0, table.Length);
                        Console.Write(new string(' ', Console.WindowWidth)); // deletes message
                        break;
                }
            }
        }

        /// <summary>
        /// Asks player question if to continue or not
        /// </summary>
        /// <returns> Returns false if player doesn </returns>
        public bool Question()
        {
            Console.SetCursorPosition(0, table.Length);
            Console.WriteLine("Přejete si založit novou hru? [ano/ne]");
            bool validOption = false;
            bool goOn = true;
            while (!validOption)
            {
                switch (Console.ReadLine().ToString().ToLower())
                {
                    case "ano":
                        goOn = true;
                        validOption = true;
                        break;
                    case "ne":
                        goOn = false;
                        validOption = true;
                        break;
                    default:
                        Console.WriteLine("Neplatná volba, zadejte prosím [ano/ne]");
                        break;
                }
            }
            return goOn;
        }

        /// <summary>
        /// Checking collision of head
        /// </summary>
        /// <returns> False if collision happened </returns>
        public bool End() 
        {
            gameStillGoing = snake.CheckCollision(); ;
            return gameStillGoing;
        }
    }
}
