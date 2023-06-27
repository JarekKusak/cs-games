﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Snake
{
    internal class Manager
    {
        private string file;
        private ObservableCollection<Player> players;
        public Manager(string file) 
        {
            players = new ObservableCollection<Player>();
            this.file = file;
        }

        public Player ReturnLastPlayer()
        {
            return players.Last();
        }

        public void AddPlayer(string name, char snakeHeadCharacter, char snakeBodyCharacter, int maxScore)
        {
            Player player = new Player(name, snakeHeadCharacter, snakeBodyCharacter, maxScore);
            players.Add(player);
        }

        public void CheckIfHighScoreBeaten(int score, Player currentPlayer)
        {
            if (score > currentPlayer.MaxScore)
            {
                currentPlayer.MaxScore = score;
                Save();
            }
        }

        public void OutputPlayers()
        {
            if (players != null)
            {
                foreach (Player p in players)
                    Console.WriteLine(p.Name);
            }
        }

        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                foreach (Player p in players)
                {
                    // vytvoření pole hodnot
                    string[] values = { p.Name, p.SnakeHeadCharacter.ToString(), p.SnakeBodyCharacter.ToString(), p.MaxScore.ToString() };
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
                    char SnakeBodyCharacter = char.Parse(splittedString[2]);
                    int maxScore = int.Parse(splittedString[3]);

                    // přidá uživatele s danými hodnotami
                    AddPlayer(name, SnakeHeadCharacter, SnakeBodyCharacter, maxScore);
                }
            }
        }
    }
}
