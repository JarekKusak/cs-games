using Microsoft.VisualBasic.FileIO;
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
        private FileManager fileManager;
        private Player currentPlayer;
        private ConsoleKeyInfo c;
        private ConsoleColor outputHeadColor;
        private ConsoleColor outputBodyColor;
        private bool gameStillGoing;
        private int minPlayerNameLength = 3;
        private bool playMore;
        private bool end;
        private bool obstacles;
        private char move;
        private int delay;
        private char[] keys = { 'w', 's', 'a', 'd' };
        //private string[] colors = { "red", "blue", "green", "yellow" }; // cannot be used in switch case statements :-/

        public Game()
        {
            c = new ConsoleKeyInfo();
            playMore = true;
            end = false;
            SetupFileManager();
        }

        /// <summary>
        /// If file (path to file) does not exist, creates new (else uses original)
        /// </summary>
        void SetupFileManager()
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
            fileManager = new FileManager(System.IO.Path.Combine(path, "players.csv"));
        }

        /// <summary>
        /// Sets color of snake's head and tail (and sets it's output color in console)
        /// </summary>
        void SetupColorsOfSnake(string headColor, string bodyColor)
        {
            switch (headColor)
            {
                case "red":
                    outputHeadColor = ConsoleColor.Red;
                    break;
                case "blue":
                    outputHeadColor = ConsoleColor.Blue;
                    break;
                case "green":
                    outputHeadColor = ConsoleColor.Green;
                    break;
                case "yellow":
                    outputHeadColor = ConsoleColor.Yellow;
                    break;
            }
            switch (bodyColor)
            {
                case "red":
                    outputBodyColor = ConsoleColor.Red;
                    break;
                case "blue":
                    outputBodyColor = ConsoleColor.Blue;
                    break;
                case "green":
                    outputBodyColor = ConsoleColor.Green;
                    break;
                case "yellow":
                    outputBodyColor = ConsoleColor.Yellow;
                    break;
            }
        }

        /// <summary>
        /// Options for colors
        /// </summary>
        string ChooseYourColor()
        {
            bool validColor = false;
            Console.WriteLine("Na výběr máte ze čtyř barev: červená, modrá, zelená, žlutá.");
            Console.WriteLine("Zadejte prosím odpověď ve formátu [red/blue/green/yellow]");
            while (!validColor)
            {
                string color = Console.ReadLine().ToString().ToLower();
                switch (color)
                {
                    case "red":
                        return color;
                    case "blue":
                        return color;
                    case "green":
                        return color;
                    case "yellow":
                        return color;
                    default:
                        validColor = false;
                        Console.WriteLine("Neplatná volba, zadejte prosím jednu z možných barev [red/blue/green/yellow]");
                        break;
                }
            }
            return "green";
        }
    
        /// <summary>
        /// Menu after clicking second option in Startup menu, allows changing account or creates new
        /// </summary>
        void PlayerMenu()
        {
            Console.Clear();
            Console.WriteLine("Chcete změnit hráče nebo vytvořit jiného?");
            Console.WriteLine("Možnost zvolte ve formátu [1/2/3]: ");
            Console.WriteLine("1) ZMĚNIT HRÁČE");
            Console.WriteLine("2) VYTVOŘIT HRÁČE");
            Console.WriteLine("3) ZPĚT\n");
            bool validOption = false;
            while (!validOption)
            {
                int option;
                while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out option))
                    Console.WriteLine("\nNeplatné číslo, zadejte prosím znovu:");

                switch (option)
                {
                    case 1:
                        ChangePlayer();
                        validOption = true;
                        break;
                    case 2:
                        CreateNewPlayerAndSave();
                        validOption = true;
                        break;
                    case 3:
                        return;
                    default:
                        validOption = false;
                        Console.WriteLine("\nNeplatná volba, zadejte prosím [1/2/3]");
                        break;
                }
            }
        }

        /// <summary>
        /// Allows account switching
        /// </summary>
        void ChangePlayer()
        {
            Console.Clear();
            Console.WriteLine("\nZde je seznam již vytvořených hráčů: \n");
            fileManager.OutputPlayersWithTheirScore();
            Player changedPlayer = null;
            while (changedPlayer == null)
            {
                Console.WriteLine("Zadejte prosím index hráče na přepnutí účtu.");
                int playerIndex;
                while (!int.TryParse(Console.ReadLine(), out playerIndex))
                    Console.WriteLine("\nNeplatné číslo, zadejte prosím znovu:");
                changedPlayer = fileManager.ReturnPlayer(--playerIndex);
            }
            currentPlayer = changedPlayer;
        }

        /// <summary>
        /// Creates new player (sets name, skin of snake) and saves into the file
        /// </summary>
        void CreateNewPlayerAndSave()
        {
            Console.Clear();
            string name = "";
            Console.Write("\n");
            while (name.Length < minPlayerNameLength)
            {
                Console.WriteLine($"Zadejte jméno hráče alespoň o {minPlayerNameLength} znacích: ");
                name = Console.ReadLine();
            }
            char snakeHeadCharacter = ' ';
            char snakeBodyCharacter = ' ';
            Console.WriteLine("Zadejte znak, kterým se bude vypisovat hlava vašeho hada (doporučují se velké znaky): ");
            while (!char.TryParse(Console.ReadLine(), out snakeHeadCharacter) || snakeHeadCharacter == ' ')
                Console.WriteLine("Neplatný znak, zadejte prosím znovu:");
            Console.WriteLine("Zadejte barvu, kterou se bude vypisovat hlava vašeho hada.");
            string headColor = ChooseYourColor();
            Console.WriteLine("Zadejte znak, kterým se bude vypisovat tělo vašeho hada (doporučují se malé znaky): ");
            while (!char.TryParse(Console.ReadLine(), out snakeBodyCharacter) || snakeBodyCharacter == ' ')
                Console.WriteLine("Neplatný znak, zadejte prosím znovu:");
            Console.WriteLine("Zadejte barvu, kterou se bude vypisovat tělo vašeho hada.");
            string bodyColor = ChooseYourColor();
            fileManager.AddPlayer(name, snakeHeadCharacter, headColor, snakeBodyCharacter, bodyColor, 0);
            fileManager.Save();
            currentPlayer = fileManager.ReturnLastPlayer();
        }

        /// <summary>
        /// Main launching method
        /// </summary>
        public void StartupMenu()
        {
            try // load already existing file with players and load the last one created
            {
                fileManager.Load();       
                fileManager.OutputPlayersWithTheirScore();
            }
            catch // no list of players -> create player
            {
                CreateNewPlayerAndSave();
            }

            currentPlayer = fileManager.ReturnLastPlayer();

            while (!end)
            {
                Console.Clear();
                Console.WriteLine("Vítejte ve hře HADISKO!");
                Console.WriteLine("Zvolte možnost ve formátu [1/2/3]");
                Console.WriteLine("1) HRÁT");
                Console.WriteLine("2) ZMĚNIT/VYTVOŘIT HRÁČE");
                Console.WriteLine("3) UKONČIT HRU\n");
                Console.WriteLine("Právě hraje: " + currentPlayer.Name);
                SetupColorsOfSnake(currentPlayer.SnakeHeadColor, currentPlayer.SnakeBodyColor);
                Console.Write("Jeho/její had: ");
                Console.ForegroundColor = outputHeadColor;
                Console.Write(currentPlayer.SnakeHeadCharacter);
                Console.ForegroundColor = outputBodyColor;
                Console.Write($"{currentPlayer.SnakeBodyCharacter}{currentPlayer.SnakeBodyCharacter}\n");
                Console.ResetColor();
                Console.WriteLine("Jeho/její nejvyšší skóre: " + currentPlayer.MaxScore.ToString());

                bool validOption = false;

                while (!validOption)
                {
                    int option;
                    while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out option))
                        Console.WriteLine("\nNeplatné číslo, zadejte prosím znovu:");
                    
                    switch (option)
                    {
                        case 1:
                            PlayMenu();
                            validOption = true;
                            break;
                        case 2:
                            PlayerMenu();
                            validOption = true;
                            break;
                        case 3:
                            end = true;
                            validOption = true;
                            break;
                        default:
                            validOption = false;
                            Console.WriteLine("\nNeplatná volba, zadejte prosím [1/2/3]");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Menu after clicking first option in Startup menu, sets difficulty and obstacles
        /// </summary>
        void PlayMenu()
        {
            playMore = true;
            while (playMore)
            {
                Console.Clear();
                obstacles = false;
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
                bool validOption = false;
                Console.WriteLine("Přejete si přidat překážky ke zvýšení obtížnosti?");
                Console.WriteLine("Odpovězte ve formátu [ano/ne]: ");
                while (!validOption)
                {
                    switch (Console.ReadLine().ToString().ToLower())
                    {
                        case "ano":
                            obstacles = true;
                            validOption = true;
                            break;
                        case "ne":
                            obstacles = false;
                            validOption = true;
                            break;
                        default:
                            Console.WriteLine("Neplatná volba, zadejte prosím [ano/ne]");
                            break;
                    }
                }
                playMore = Play(obstacles);
                fileManager.CheckIfMaxScoreBeaten(apple.Points, currentPlayer);
            }
        }

        /// <summary>
        /// Method that setups whole new game (creates new objects)
        /// </summary>
        /// <param name="obstacles"> If obstacles are wanted </param>
        void SetupNewGame(bool obstacles)
        {
            Console.Clear();
            Console.CursorVisible = false;
            // new objects              
            table = new Table(25, obstacles);
            table.TableOutput();
            apple = new Apple(table, 'O');
            snake = new Snake(table, apple, keys, currentPlayer.SnakeHeadCharacter, currentPlayer.SnakeBodyCharacter, 
                outputHeadColor, outputBodyColor);
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
        public bool Play(bool obstacles)
        {
            SetupNewGame(obstacles);
            // main loop
            while (gameStillGoing)    
                Move();     
            Console.CursorVisible = true;
            playMore = Question();
            Console.Clear();
            return playMore;
        }

        /// <summary>
        /// Method is being called forever until keyboard interrupt appears (only for not forbiden keys)
        /// </summary>
        /// <param name="key"> Keyboard key that was pressed </param>
        void RepeatMovementUntilInterrupt(char key)
        {
            ConsoleKey allowedKey1;
            ConsoleKey allowedKey2;
            if (key == keys[0] || key == keys[1]) // if going up or down, only allowed movement is left or right
            {
                allowedKey1 = ConsoleKey.A;
                allowedKey2 = ConsoleKey.D;
            }
            else // if going left or right, only allowed movement is up or down
            {
                allowedKey1 = ConsoleKey.W;
                allowedKey2 = ConsoleKey.S;
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
            while (c.Key != allowedKey1 && c.Key != allowedKey2);
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
            gameStillGoing = !snake.CheckSelfCollision(); // assigns negation, collision == true -> game still going == false
            if (obstacles) // if obstacles are set
                gameStillGoing = !snake.CheckObstacleCollision();
            return gameStillGoing;
        }
    }
}