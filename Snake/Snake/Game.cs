using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Snake
{
    internal class Game
    {
        private Table table;
        private Snake snake;
        private Apple apple;
        private Manager manager;
        private Player currentPlayer;
        private ConsoleKeyInfo c;
        private bool gameStillGoing;
        private bool playMore;
        private bool end;
        private char move;
        private int delay;
        private char[] keys = { 'w', 's', 'a', 'd' };

        public Game(Table tabulka)
        {
            c = new ConsoleKeyInfo();
            playMore = true;
            end = false;
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

        void CreateNewPlayer()
        {
            manager.AddPlayer("sobotka", 'c', 'x', 0);
            manager.Save();
            /*
            bool correct = false;
            
            Console.WriteLine("Jak se bude váš hráč jmenovat? (jméno musí obsahovat alespoň tři znaky");
            string name = Console.ReadLine();
            if (name.Length < 3)
                Console.WriteLine("Jméno je příliš krátké");
            char snakeBodyCharacter = ' ';
            char snakeHeadCharacter = ' ';
            if (snakeBodyCharacter == ' ' || snakeHeadCharacter == ' ')
                Console.WriteLine("Neplatný znak");
            */
        }

        public void StartupMenu()
        {
            Console.WriteLine("Vítejte ve hře HADISKO!");

            try
            {
                manager.Load();
                currentPlayer = manager.ReturnLastPlayer();
                manager.OutputPlayers();
            }
            catch
            {
                
            }

            while (!end)
            {
                Console.WriteLine("Zvolte možnost ve formátu [1/2/3]");
                Console.WriteLine("1) HRÁT");
                Console.WriteLine("2) ZMĚNIT/VYTVOŘIT HRÁČE");
                Console.WriteLine("3) UKONČIT HRU\n");
                Console.WriteLine("Právě hraje: " + currentPlayer.Name);
                Console.WriteLine("Jeho/její had: " + currentPlayer.SnakeHeadCharacter + currentPlayer.SnakeBodyCharacter + 
                    currentPlayer.SnakeBodyCharacter);
                Console.WriteLine("Jeho/její nejvyšší skóre: " + currentPlayer.MaxScore.ToString());

                bool validOption = false;

                while (!validOption)
                {
                    switch (int.Parse(Console.ReadKey().KeyChar.ToString()))
                    {
                        case 1:
                            PlayMenu();
                            validOption = true;
                            break;
                        case 2:
                            CreateNewPlayer();
                            validOption = true;
                            break;
                        case 3:
                            end = true;
                            validOption = true;
                            break;
                        default:
                            validOption = false;
                            Console.WriteLine("Neplatná volba, zadejte prosím [1/2/3]");
                            break;
                    }
                }
            }
        }

        void PlayMenu()
        {
            playMore = true;
            while (playMore)
            {
                Console.Clear();
                bool validDifficulty = false;
                Console.WriteLine("Ovládat hada můžete pomocí tlačítek [W/S/A/D].");
                Console.WriteLine("Zadejte obtížnost ve formátu [easy/normal/hard/extreme]: ");

                while (!validDifficulty)
                {
                    switch (Console.ReadLine().ToString().ToLower())
                    {
                        case "easy":
                            delay = 100;
                            validDifficulty = true;
                            break;
                        case "normal":
                            delay = 75;
                            validDifficulty = true;
                            break;
                        case "hard":
                            delay = 50;
                            validDifficulty = true;
                            break;
                        case "extreme":
                            delay = 25;
                            validDifficulty = true;
                            break;
                        default:
                            validDifficulty = false;
                            Console.WriteLine("Neplatná volba, zadejte prosím [easy/normal/hard/extreme]");
                            break;
                    }
                }
                playMore = Play();
                manager.CheckIfHighScoreBeaten(apple.Points, currentPlayer);
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
        public bool Play()
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