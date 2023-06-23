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
        private Manager manager;
        private ConsoleKeyInfo c;
        private bool gameStillGoing;
        private bool playMore;
        private char move;
        private int delay;
        private char[] keys = { 'w', 's', 'a', 'd' };

        public Game(Table tabulka)
        {
            c = new ConsoleKeyInfo();
            playMore = true;
            this.table = tabulka;
            SetupManager();
        }

        void SetupManager()
        {
            string path = "";
            try
            {
                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DatabazeHracu");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch
            {
                Console.WriteLine("Nepodařilo se vytvořit složku " + path + ", zkontrolujte prosím svá oprávnění.");
            }
            manager = new Manager(System.IO.Path.Combine(path, "players.csv"));
        }

        public void StartupMenu()
        {
            /*
            manager.AddPlayer("jirka", 'X', 'x');
            manager.AddPlayer("batman", 'O', 'x');
            manager.AddPlayer("doktor", 'X', 'p');
            */
            //
            try
            {
                manager.Load();
                //manager.Save();
            }
            catch
            {
                Console.WriteLine("Databázi se nepodařilo uložit, zkontrolujte přístupová práva k souboru.");
            }

            while (playMore)
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
                playMore = Play();
            }
        }

        void SetupNewGame()
        {
            Console.Clear();
            table.TableOutput();
            Console.CursorVisible = false;
            // new objects              
            apple = new Apple(table, 'O');
            snake = new Snake(table, apple, keys, 'X', 'x');
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
                Move();     
            Console.CursorVisible = true;
            playMore = Question();
            Console.Clear();
            return playMore;
        }

        void RepeatMovementUntilInterrupt(char key)
        {
            ConsoleKey forbidenKey1;
            ConsoleKey forbidenKey2;
            if (key == keys[0] || key == keys[1])
            {
                forbidenKey1 = ConsoleKey.A;
                forbidenKey2 = ConsoleKey.D;
            }
            else
            {
                forbidenKey1 = ConsoleKey.W;
                forbidenKey2 = ConsoleKey.S;
            }

            do
            {
                while (Console.KeyAvailable == false) // records user input undisturbed
                {
                    snake.GoSnake(key);
                    if (End() == false) // control for end of the game
                        break; // break for ending next move (end of cycle)
                    Thread.Sleep(delay);
                }
                if (End() == true)
                    c = Console.ReadKey(true);
                else break;
            }
            while (c.Key != forbidenKey1 && c.Key != forbidenKey2);
            move = c.KeyChar; // saves last entered input of player
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
                        RepeatMovementUntilInterrupt(keys[0]);
                        break;
                    case 's': // downwards direction
                        RepeatMovementUntilInterrupt(keys[1]);
                        break;
                    case 'a': // left direction
                        RepeatMovementUntilInterrupt(keys[2]);
                        break;
                    case 'd': // right direction
                        RepeatMovementUntilInterrupt(keys[3]);
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