using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Snake
{
    internal class FileManager
    {
        private string file;
        private ObservableCollection<Player> players;
        public FileManager(string file) 
        {
            players = new ObservableCollection<Player>();
            this.file = file;
        }

        public Player ReturnLastPlayer()
        {
            return players.Last();
        }

        public void AddPlayer(string name, char snakeHeadCharacter, string snakeHeadColor, char snakeBodyCharacter, string snakeBodyColor, int maxScore)
        {
            Player player = new Player(name, snakeHeadCharacter, snakeHeadColor, snakeBodyCharacter, snakeBodyColor, maxScore);
            players.Add(player);
        }

        public void CheckIfMaxScoreBeaten(int score, Player currentPlayer)
        {
            if (score > currentPlayer.MaxScore)
            {
                currentPlayer.MaxScore = score;
                Save();
            }
        }

        public Player ReturnPlayer(int index)
        {
            if (index < players.Count && index >= 0)
            {
                return players[index];
            }
            else return null;
        }

        public void OutputPlayersWithTheirScore()
        {
            if (players != null)
            {
                for (int i = 0; i < players.Count; i++) 
                {
                    Console.WriteLine($"{i+1}. {players[i].Name}");
                    Console.WriteLine($"Jeho/její nejvyšší skóre: {players[i].MaxScore}\n");
                }   
            }
        }

        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                foreach (Player p in players)
                {
                    // vytvoření pole hodnot
                    string[] values = { p.Name, p.SnakeHeadCharacter.ToString(), 
                        p.SnakeHeadColor, p.SnakeBodyCharacter.ToString(), p.SnakeBodyColor, p.MaxScore.ToString() };
                    // vytvoření řádku
                    string row = String.Join(";", values);
                    // zápis řádku
                    sw.WriteLine(row);
                }
                // vyprázdnění bufferu
                sw.Flush();
            }
        }

        public void Load()
        {
            players.Clear();
            // Otevře soubor pro čtení
            using (StreamReader sr = new StreamReader(file))
            {
                string s;
                // čte řádek po řádku
                while ((s = sr.ReadLine()) != null)
                {
                    // rozdělí string řádku podle středníků
                    string[] splittedString = s.Split(';');
                    string name = splittedString[0];
                    char SnakeHeadCharacter = char.Parse(splittedString[1]);
                    string SnakeHeadColor = splittedString[2];
                    char SnakeBodyCharacter = char.Parse(splittedString[3]);
                    string SnakeBodyColor = splittedString[4];
                    int maxScore = int.Parse(splittedString[5]);

                    // přidá uživatele s danými hodnotami
                    AddPlayer(name, SnakeHeadCharacter, SnakeHeadColor, SnakeBodyCharacter, SnakeBodyColor, maxScore);
                }
            }
        }
    }
}
