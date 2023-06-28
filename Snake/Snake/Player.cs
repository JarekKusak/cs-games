using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Player
    {
        public string Name { get; set; }
        public char SnakeHeadCharacter { get; set; }
        public string SnakeHeadColor { get; set; }
        public char SnakeBodyCharacter { get; set; }
        public string SnakeBodyColor { get; set; }
        public int MaxScore { get; set; }

        public Player(string name, char snakeHeadCharacter, string snakeHeadColor, char snakeBodyCharacter, string snakeBodyColor, int maxScore) 
        {
            this.Name = name;
            SnakeHeadCharacter = snakeHeadCharacter;
            SnakeHeadColor = snakeHeadColor;
            SnakeBodyCharacter = snakeBodyCharacter;
            SnakeBodyColor = snakeBodyColor;
            MaxScore = maxScore;
        }
    }
}
